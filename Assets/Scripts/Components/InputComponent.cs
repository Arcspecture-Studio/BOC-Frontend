using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public InputAction click;
    public InputAction hold;
    public InputAction screenPos;
    public InputAction drag;

    void Awake()
    {
        click.Enable();
        hold.Enable();
        screenPos.Enable();
        drag.Enable();
    }
}