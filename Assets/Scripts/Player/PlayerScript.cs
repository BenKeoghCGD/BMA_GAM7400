using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerScript : MonoBehaviour
{
    //Lucian's version of the player script :3

    //Input System stuff
    private InputActionAsset playerInputActions;
    private InputActionMap playerInputMap;
    private InputAction moveAction;
    private InputAction interactAction;
    private CharacterController characterController;

    //Player movement variables
    public float moveSpeed = 5.0f;


    //Camera variables
    [SerializeField] Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movement has to be updated every frame
        Move();
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
        interactAction = playerInputMap.FindAction("Interact");
        Debug.Log("PlayerScript Awake");
    }

    private void OnEnable()
    {
        //enables the move and interact actions
        moveAction.Enable();
        interactAction.Enable();
    }




    void Move()
    {
        //gets the input from the move action and moves the player
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = move * moveSpeed * Time.deltaTime;
        move = transform.TransformDirection(move);
        characterController.Move(move);
    }

    void Interact()
    {
        //WIP for now just logs interact
        if (interactAction.triggered)
        {
            Debug.Log("Interact");
        }
    }

}
