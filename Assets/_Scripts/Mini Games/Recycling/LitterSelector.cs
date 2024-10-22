using PrimeTween;
using PrimeTweenDemo;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LitterSelector : MonoBehaviour
{
    private GameObject selectedGarbage;
    private Litter selectedLitter;
    private Bin nearestBin;
    private GameObject highlightedBin;

    public float detectionRadius = 2f;
    public float gameDuration = 20f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI multiplierText;
    private float timer;
    private float multiplier = 1.0f;
    private bool GameIsOver = false;

    [SerializeField] GameObject[] litter;
    [SerializeField] Vector3 litterSpawnPos;
    [SerializeField] Vector3 litterScreenPos;

    Vector3 litterOriginalPos;
    GameObject currentLitter;

    [SerializeField] PlayerLife playerLife;
    [SerializeField] Camera cam;
    PlayerScript playerScript;




    bool CanDragLitter;
    bool StartTimer;



    void Update()
    {

        if (GameIsOver) return;

        if (!StartTimer) return;

        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            OnMiniGameEnd();
        }


        if (!CanDragLitter) return;

        DragLitterWithTouch();
    }
    private void ChangeCameraPosition()
    {
        Tween.LocalPosition(cam.transform, new Vector3(-5.48999977f, 3.20000005f, 48.4099998f), 0.5f);
        Tween.LocalEulerAngles(cam.transform, cam.transform.localEulerAngles, new Vector3(39.8089981f, 0, 0), 0.5f).OnComplete(target: this, target => target.SpawnNewLitter());
    }
    private void SpawnNewLitter()
    {
        CanDragLitter = false;
        int randomLitterIndex = UnityEngine.Random.Range(0, litter.Length);
        currentLitter = Instantiate(litter[randomLitterIndex], new Vector3(-5.38000011f, 1.58299994f, 47.8199997f), Quaternion.identity);
        Tween.Position(currentLitter.transform, new Vector3(-5.38000011f, 1.58299994f, 49.7200012f), 0.5f).OnComplete(target: this, target =>
        {
            litterOriginalPos = currentLitter.transform.position;
            CanDragLitter = true;
        });
    }
    private void MoveLitterToOriginalPos()
    {
        CanDragLitter = false;
        Tween.Position(currentLitter.transform, litterOriginalPos, 0.5f).OnComplete(target: this, target => CanDragLitter = true);

    }

    private void DragLitterWithTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = GetTouchWorldPosition();
            touchPosition.z = selectedGarbage != null ? selectedGarbage.transform.position.z : transform.position.z;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TrySelectGarbage();
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (selectedGarbage != null)
                    {
                        MoveSelectedGarbage(touchPosition);
                        CheckForNearbyBin();
                    }
                    break;

                case TouchPhase.Ended:
                    if (selectedGarbage != null)
                    {
                        CheckIfCorrectBin();
                        DeselectGarbage();
                    }
                    break;
            }
        }
    }
    public void OnMiniGameStart(PlayerScript player)
    {
        playerScript = player;

        playerScript.SetMovementStatus(false);
        playerScript.GetComponent<MeshRenderer>().enabled = false;
        cam.GetComponent<CameraController>().FollowPlayer = false;

        UIManager uIManager = UIManager.instance;
        uIManager.joystickCanvas.SetActive(true);
        uIManager.recyclingCanvas.SetActive(false);
        uIManager.triggerUICanvas.SetActive(false);

        timer = gameDuration;
        UpdateMultiplierUI();
        ChangeCameraPosition();
        StartTimer = true;

    }
    public void OnMiniGameEnd()
    {
        if (playerScript != null)
        {
            GameIsOver = true;
            playerScript.SetMovementStatus(true);
            playerScript.GetComponent<MeshRenderer>().enabled = true;
            cam.GetComponent<CameraController>().ResetCamera();

            UIManager uIManager = UIManager.instance;
            uIManager.joystickCanvas.SetActive(true);
            uIManager.recyclingCanvas.SetActive(false);
            uIManager.triggerUICanvas.SetActive(true);
            Destroy(currentLitter);
            currentLitter = null;
            playerLife.ResetHealth();
            GameIsOver = false;
            //triggerUICanvas.SetActive(false);
        }
    }

    Vector3 GetTouchWorldPosition()
    {
        Vector3 screenPos = Input.GetTouch(0).position;
        return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(transform.position.z - cam.transform.position.z)));
    }

    void TrySelectGarbage()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Garbage"))
        {
            selectedGarbage = hit.collider.gameObject;
            selectedLitter = selectedGarbage.GetComponent<Litter>();

            HighlightObject(selectedGarbage, true);
        }
    }

    void MoveSelectedGarbage(Vector3 position)
    {
        selectedGarbage.transform.position = Vector3.Lerp(selectedGarbage.transform.position, position, Time.deltaTime * 10f);
    }

    void CheckForNearbyBin()
    {
        Collider[] colliders = Physics.OverlapSphere(selectedGarbage.transform.position, detectionRadius);
        GameObject nearest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            Bin bin = collider.GetComponent<Bin>();
            if (bin != null)
            {
                float distance = Vector3.Distance(selectedGarbage.transform.position, bin.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = bin.gameObject;
                    nearestBin = bin;
                }
            }
        }

        UpdateBinHighlight(nearest);
    }

    void UpdateBinHighlight(GameObject nearest)
    {
        if (highlightedBin != nearest)
        {
            HighlightObject(highlightedBin, false);
            HighlightObject(nearest, true);
            highlightedBin = nearest;
        }
    }

    void CheckIfCorrectBin()
    {
        if (nearestBin != null && selectedLitter != null)
        {
            if (nearestBin.litterType == selectedLitter.litterType)
            {
                Debug.Log("Correct bin!");
                SpawnNewLitter();
                selectedLitter.gameObject.SetActive(false);
                multiplier += 0.1f;
                UpdateMultiplierUI();
            }
            else
            {
                Debug.Log("Wrong bin! Try again.");
                
                multiplier = 1.0f;
                UpdateMultiplierUI();
                OnWrongBinSelection();
            }
        }

    }

    void OnWrongBinSelection()
    {
        playerLife.DecreasePlayerHealth();
        if (playerLife.Health <= 0)
        {
            OnMiniGameEnd();
        }
        else
        {
            MoveLitterToOriginalPos();
        }
    }

    void UpdateMultiplierUI()
    {
        multiplierText.text = "Multiplier: " + multiplier.ToString("F1");
    }

    void DeselectGarbage()
    {
        HighlightObject(selectedGarbage, false);
        HighlightObject(highlightedBin, false);
        selectedGarbage = null;
        selectedLitter = null;
        nearestBin = null;
        highlightedBin = null;
    }

    void HighlightObject(GameObject obj, bool highlight)
    {
        if (obj != null)
        {
            obj.GetComponent<Outline>().enabled = highlight;
        }
    }

    //======Debug=============
    void OnDrawGizmos()
    {
        if (selectedGarbage != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(selectedGarbage.transform.position, detectionRadius);
        }
    }
}
