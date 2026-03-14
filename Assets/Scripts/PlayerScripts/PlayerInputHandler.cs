using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool InteractPressed { get; private set; }

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx =>
            MoveInput = ctx.ReadValue<Vector2>();

        inputActions.Player.Move.canceled += ctx =>
            MoveInput = Vector2.zero;

        inputActions.Player.Jump.started += ctx =>
            JumpPressed = true;

        inputActions.Player.Interact.started += ctx =>
            InteractPressed = true;
        
    
        
    }

    private void LateUpdate()
    {
        JumpPressed = false;
        InteractPressed = false;

    }

    public bool IsToggleInventoryPressed()
    {
        return inputActions.Player.ToggleInventory.WasPressedThisFrame();
    }

    public bool IsJumpHeld()
    {
        return inputActions.Player.Jump.IsPressed();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        InteractPressed = context.started;
    }
}
