using UnityEngine;
using UnityEngine.Events;

public class GetInitialDataComponent : MonoBehaviour
{
    public bool getInitialData
    {
        set { onChange_getInitialData.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getInitialData = new();
}