/*
 * Branch: Lucian (Matan, Lucian)
 * Commit: 99357b9b8e792fe89c60c5b01b057b3cc84a0648, f269abddc18aec6cbe37a18775f730aaa1ea1e32
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{
    //Lucian's version of the player script :3

    //Input System stuff
    private InputActionAsset playerInputActions;
    private InputActionMap playerInputMap;
    private InputAction moveAction;
    private CharacterController characterController;
    [SerializeField] private FixedJoystick joystick;

    //Player movement variables
    [SerializeField]
    private float baseSpeed;
    public float moveSpeed = 5.0f;

    //player life variables (HS)
    public int playerLife = 0;

    // tool functionality enum (BS)
    public ToolType equippedTool;

    //the amount of litter that player has in inventory (HS)
    public int litterCollectedAmount;

    //Camera variables
    [SerializeField] Camera playerCamera;

    //Litter Related variables
    // Litter needs to be managed by size and weight rather than flat value, can probably use ScriptableObject for LitterData (ask Ben Stott if you move on to this) (BH)
    // MaxSize variable
    // MaxWeight variable
    [SerializeField] 
    private int heldBlackLitter = 0;
    [SerializeField] 
    private int heldRedLitter = 0;
    [SerializeField] 
    private int heldBeigeLitter = 0;

    [SerializeField] private LayerMask interactLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //movement has to be updated every frame
        Move();
        OnInteractCollision();
    }

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
        characterController = GetComponent<CharacterController>();
        //initializes the input system
        playerInputActions = this.GetComponent<PlayerInput>().actions;
        //gets the player controls action map and the actions
        playerInputMap = playerInputActions.FindActionMap("PlayerControls");
        moveAction = playerInputMap.FindAction("Move");
    }

    private void OnEnable()
    {
        //enables the move and interact actions
        moveAction.Enable();
        playerInputMap.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        playerInputMap.Disable();
    }


    void Move()
    {
        Debug.Log("Move2");
        //gets the input from the move action and moves the player
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = move * moveSpeed * Time.deltaTime;
        move = transform.TransformDirection(move);
        characterController.Move(move);

        //Alternatively, we could use the joystick input for movement
        move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        move = move * moveSpeed * Time.deltaTime;
        move = transform.TransformDirection(move);
        characterController.Move(move);

    }

    //Following code has been copied from Ben Higham's PlayerController
    public int HeldBlackLitter
    {
        get { return heldBlackLitter; }
    }
    
    //In this part, we were only able to read the values,
    //but it’s necessary to reset them to zero after colliding with the trash bins and using certain power-ups.
    //So, I added these setters to handle that.(HS)
    public void HeldBlackLitter_Setter(int value)
    {
          heldBlackLitter = value;
    }
    public int HeldRedLitter
    {
        get { return heldRedLitter; }
    }
    public void HeldRedLitter_Setter(int value)
    {
        heldRedLitter = value;
    }
    public int HeldBeigeLitter
    {
        get { return heldBeigeLitter; }
    }
    public void HeldBeigeLitter_Setter(int value)
    {
        heldBeigeLitter = value;
    }

    public void AdjustLitter(LitterType type)
    {
        switch (type)
        {
            case LitterType.Beige:
                heldBeigeLitter += Configuration.LitterValue;
                break;
            case LitterType.Black:
                heldBlackLitter += Configuration.LitterValue;
                break;
            case LitterType.Red:
                heldRedLitter += Configuration.LitterValue;
                break;
        }
    }

    public void SetLitter(LitterType type, int amount)
    {
        switch (type)
        {
            case LitterType.Beige:
                heldBeigeLitter = amount;
                break;
            case LitterType.Black:
                heldBlackLitter = amount;
                break;
            case LitterType.Red:
                heldRedLitter = amount;
                break;
        }
    }

    public void ClearLitter(LitterType type)
    {
        SetLitter(type, 0);
    }
    public void MultiplyBaseSpeed(float multiplier)
    {
        moveSpeed = baseSpeed * multiplier;
    }
    public void ResetMovementSpeed()
    {
        moveSpeed = baseSpeed;
    }

    
    public void CalculatePlayerLife()
    {
        playerLife += 1;
        
        //This part has been implemented temporarily to display the player’s current lives on the screen.(HS)
        UIManager.instance.lifeAmountText.text = playerLife.ToString();
    }

    //This function is responsible for calculating the amount of collected litter under different conditions.(HS)
    //It takes two inputs: a boolean indicating whether the item encountered is a power-up,
    //and a second input that specifies the amount we want to award to the player upon interaction.(HS)
    public void CalculateCollectedLitter(bool isPowerUp , int amount)
    {
        if (!isPowerUp)
        {
            litterCollectedAmount += amount;
        }
        else
        {
            litterCollectedAmount += amount;
        }
        //This part has been implemented temporarily to display the player’s current litter collected on the screen.(HS)
        UIManager.instance.LitterAmountText.text = litterCollectedAmount.ToString();
    }

    // Checks for collision with IInteractable object. If the object is litter, checks if the player can carry more
    private void OnInteractCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, interactLayer);

        foreach (Collider hit in hits)
        {
            IInteractable target = hit.gameObject.GetComponent<IInteractable>();

            if (target == null)
            {
                continue;
            }

            target.OnInteract(this);
        }
    }

    public bool hasRequiredTool(ToolType requiredTool)
    {
        return equippedTool == requiredTool;
    }
}
