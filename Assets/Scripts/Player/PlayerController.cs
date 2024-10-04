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
    private int maxLitter;

    [SerializeField]
    private LayerMask interactLayer;

    private int heldLitter = 0;
    public int HeldLitter
    {
        get { return heldLitter; }
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = baseSpeed;
        maxLitter = baseMaxLitter;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OnInteractCollision();
    }

    public void AdjustLitter(int amount)
    {
        heldLitter += amount;
    }
    public void SetLitter(int amount)
    {
        heldLitter = amount;
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

            if (hit.GetComponent<Litter>() != null && heldLitter >= maxLitter)
            {
                continue;
            }

            target.OnInteract(this);
        }
    }
}
