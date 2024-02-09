using UnityEngine;
using UnityEngine.Events;

public class HideAllPanelComponent : MonoBehaviour
{
    public string hideNow
    {
        set { onChange_hideNow.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<string> onChange_hideNow = new();
}