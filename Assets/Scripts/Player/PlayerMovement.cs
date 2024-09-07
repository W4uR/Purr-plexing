using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : DisableOnPause
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float turnSpeed = 180f;
    [SerializeField]
    AudioSource feetAudioSource;

    private bool isMoving;
    private bool isTurning;

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private Quaternion targetRotation;
    private Quaternion startRotation;

    private Bindings inputActions;
    private Vector2 inputVector;

    private void Awake()
    {
        inputActions = new Bindings();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        //inputActions.Player.Move.performed += OnMovementInput;
    }


    private void OnDisable()
    {
        //inputActions.Player.Move.performed -= OnMovementInput;
        inputActions.Disable();
    }

    // Use this for supporting NOT holding input keys.
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        if (isMoving || isTurning) return;
        
        inputVector = context.ReadValue<Vector2>();
        
        if (inputVector.y > 0f)
        {
            isMoving = true;
            targetPosition = (transform.position + transform.forward).RoundXZ();
            Debug.Log("Target Position: " +targetPosition.ToString("F10"));
            startPosition = transform.position;
        }
        else if (inputVector.y < 0f)
        {
            isMoving = true;
            targetPosition = (transform.position - transform.forward).RoundXZ();
            startPosition = transform.position;
        }
        else if (inputVector.x > 0f)
        {
            isTurning = true;
            targetRotation = Quaternion.Euler(0f, RelativeDirection.RIGHT.toRotationAngle(), 0f) * transform.rotation;
            startRotation = transform.rotation;
        }
        else if (inputVector.x < 0f)
        {
            isTurning = true;
            targetRotation = Quaternion.Euler(0f, RelativeDirection.LEFT.toRotationAngle(), 0f) * transform.rotation;
            startRotation = transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
    }

    // Use this for supporting holding input keys.
    private void HandleInput()
    {
        if (isMoving || isTurning) return;

        inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        if (inputVector.y > 0f)
        {
            isMoving = true;
            targetPosition = (transform.position + transform.forward).RoundXZ();
            Debug.Log("Target Position: " + targetPosition.ToString("F10"));
            startPosition = transform.position;
        }
        else if (inputVector.y < 0f)
        {
            isMoving = true;
            targetPosition = (transform.position - transform.forward).RoundXZ();
            startPosition = transform.position;
        }
        else if (inputVector.x > 0f)
        {
            isTurning = true;
            targetRotation = Quaternion.Euler(0f, RelativeDirection.RIGHT.toRotationAngle(), 0f) * transform.rotation;
            startRotation = transform.rotation;
        }
        else if (inputVector.x < 0f)
        {
            isTurning = true;
            targetRotation = Quaternion.Euler(0f, RelativeDirection.LEFT.toRotationAngle(), 0f) * transform.rotation;
            startRotation = transform.rotation;
        }
    }

    private void HandleMovement()
    {
        if (isMoving)
        {
            if (Vector3.Distance(startPosition, transform.position) > 1f)
            {
                transform.position = targetPosition;
                isMoving = false;
                return;
            }
            transform.position += (targetPosition - startPosition) * speed * Time.deltaTime;
            return;
        }

        if (isTurning)
        {
            if (Quaternion.Angle(startRotation, transform.rotation) >= 90f)
            {
                transform.rotation = targetRotation;
                isTurning = false;
                return;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            return;
        }
    }
}
