using UnityEngine;
using UnityEngine.Events;

public class GetInitialDataComponent : MonoBehaviour
{
    public bool getNow
    {
        set { onChange_getNow.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getNow = new();
}