using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public InputAction click;
    public InputAction hold;
    public InputAction drag;
    public InputAction back;
    public InputAction submit;

    // Debug
    public InputAction screenPos;
    public InputAction space;

    void Awake()
    {
        click.Enable();
        hold.Enable();
        drag.Enable();
        back.Enable();
        submit.Enable();
    }
    void OnDestroy()
    {
        click.Disable();
        hold.Disable();
        drag.Disable();
        back.Disable();
        submit.Disable();
    }
}