using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed;
    private float movementSpeed;

    [SerializeField]
    private int baseMaxLitter;
    private int maxBlackLitter;
    private int maxRedLitter;
    private int maxBeigeLitter;

    [SerializeField]
    private LayerMask interactLayer;

    private int heldBlackLitter = 0;
    private int heldRedLitter = 0;
    private int heldBeigeLitter = 0;
    public int HeldBlackLitter
    {
        get { return heldBlackLitter; }
    }
    public int HeldRedLitter
    {
        get { return heldRedLitter; }
    }
    public int HeldBeigeLitter
    {
        get { return heldBeigeLitter; }
    }
    void Start()
    {
        movementSpeed = baseSpeed;
        maxBlackLitter = baseMaxLitter;
        maxRedLitter = baseMaxLitter;
        maxBeigeLitter = baseMaxLitter;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OnInteractCollision();
    }

    public void AdjustBlackLitter(int amount)
    {
        heldBlackLitter += amount;
    }
    public void AdjustRedLitter(int amount)
    {
        heldRedLitter += amount;
    }
    public void AdjustBeigeLitter(int amount)
    {
        heldBeigeLitter += amount;
    }
    public void SetBlackLitter(int amount)
    {
        heldBlackLitter = amount;

    }
    public void SetRedLitter(int amount)
    {
        heldRedLitter = amount;
    }
    public void SetBeigeLitter(int amount)
    {
        heldBeigeLitter = amount;
    }
    public void MultiplyBaseSpeed(float multiplier)
    {
        movementSpeed = baseSpeed * multiplier;
    }
    public void ResetMovementSpeed()
    {
        movementSpeed = baseSpeed;
    }
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        move.Normalize();

        transform.position += move * movementSpeed * Time.deltaTime;
    }

    // Checks for collision with IInteractable object. If the object is litter, checks if the player can carry more
    private void OnInteractCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, interactLayer);

        foreach (Collider hit in hits)
        {
            IInteractable target = hit.gameObject.GetComponent<IInteractable>();

            if(target == null)
            {
                continue;
            }

            if (hit.GetComponent<BlackBinLitter>() != null && heldBlackLitter >= maxBlackLitter)
            {
                continue;
            }

            target.OnInteract(this);
        }
    }
}
