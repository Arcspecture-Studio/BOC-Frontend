using UnityEngine;
using UnityEngine.Events;

public class ExitComponent : MonoBehaviour
{
    public bool exit
    {
        set { onChange_exit.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_exit = new();
}