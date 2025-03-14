using UnityEngine;
using UnityEngine.Events;

public class GetRuntimeDataComponent : MonoBehaviour
{
    /* Current usages
     - user press refresh button
     - on add new profile
    */
    public bool getRuntimeData
    {
        set { onChange_getRuntimeData.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getRuntimeData = new();
    public General.WebsocketGetRuntimeDataResponse processRuntimeData
    {
        set { onChange_processRuntimeData.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetRuntimeDataResponse> onChange_processRuntimeData = new();
}