using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public InputAction click;
    public InputAction hold;
    public InputAction drag;
    public InputAction back;

    // Debug
    public InputAction screenPos;
    public InputAction submit;
    public InputAction space;

    void Awake()
    {
        click.Enable();
        hold.Enable();
        drag.Enable();
        back.Enable();
    }
    void OnDestroy()
    {
        click.Disable();
        hold.Disable();
        drag.Disable();
        back.Disable();
    }
}