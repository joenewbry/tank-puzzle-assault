// This file was auto-migrated from legacy Input API to new Input System for compatibility.

using UnityEngine;
using UnityEngine.InputSystem;

public class DemoInputRouter : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float TurnSpeed = 100f;

    private PlayerInput playerInput;
    private Vector2 moveInput;
    private Vector2 rotateInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Rotate"].performed += OnRotate;
        playerInput.actions["Rotate"].canceled += OnRotate;
    }

    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Move"].canceled -= OnMove;
        playerInput.actions["Rotate"].performed -= OnRotate;
        playerInput.actions["Rotate"].canceled -= OnRotate;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up, rotateInput.x * TurnSpeed * Time.deltaTime);
    }
}
