using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public InputAction click;
    public InputAction hold; // Unused for now
    public bool holding;
    public InputAction drag;
    public InputAction back;
    public InputAction screenPos;

    // Debug
    public InputAction submit;
    public InputAction space;

    void Awake()
    {
        click.Enable();
        hold.Enable();
        drag.Enable();
        back.Enable();
        screenPos.Enable();

        hold.performed += IsHolding;
        hold.canceled += HoldingReleased;
    }
    void OnDestroy()
    {
        hold.performed -= IsHolding;
        hold.canceled -= HoldingReleased;

        click.Disable();
        hold.Disable();
        drag.Disable();
        back.Disable();
        screenPos.Disable();
    }

    void IsHolding(InputAction.CallbackContext context)
    {
        holding = true;
    }
    void HoldingReleased(InputAction.CallbackContext context)
    {
        holding = false;
    }
}