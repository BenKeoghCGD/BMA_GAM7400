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
    public TextMeshProUGUI livesText;
    public int playerLives = 3;
    private float timer;
    private float multiplier = 1.0f;
    private bool gameIsOver = false;

    public Color gizmoColor = Color.yellow;
    void Start()
    {
        timer = gameDuration;
        UpdateLivesUI();
        UpdateMultiplierUI();

    }

    void Update()
    {

        if (gameIsOver) return;

        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            gameIsOver = true;
        }

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

    Vector3 GetTouchWorldPosition()
    {
        Vector3 screenPos = Input.GetTouch(0).position;
        return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(transform.position.z - Camera.main.transform.position.z)));
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
                selectedLitter.gameObject.SetActive(false);
                multiplier += 0.1f;
                UpdateMultiplierUI();
            }
            else
            {
                Debug.Log("Wrong bin! Try again.");
                multiplier = 1.0f;
                UpdateMultiplierUI();
                LoseLife();
            }
        }

    }

    void LoseLife()
    {
        playerLives--;
        UpdateLivesUI();
        if (playerLives <= 0)
        {
            gameIsOver = true;
        }
    }

    void UpdateMultiplierUI()
    {
        multiplierText.text = "Multiplier: " + multiplier.ToString("F1");
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + playerLives.ToString();
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
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(selectedGarbage.transform.position, detectionRadius);
        }
    }
}
