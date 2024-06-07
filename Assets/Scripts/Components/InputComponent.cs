using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public InputAction click;
    public InputAction hold;
    public InputAction screenPos;
    public InputAction drag;
    public InputAction escape;
    public InputAction back;
    public InputAction space;

    void Awake()
    {
        click.Enable();
        hold.Enable();
        screenPos.Enable();
        drag.Enable();
        // escape.Enable();
        back.Enable();
        // space.Enable(); // DEBUG
    }
}