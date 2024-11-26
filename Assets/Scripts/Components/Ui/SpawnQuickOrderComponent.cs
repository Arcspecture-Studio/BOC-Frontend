using UnityEngine;
using UnityEngine.Events;

public class SpawnQuickOrderComponent : MonoBehaviour
{
    public General.WebsocketGetQuickOrderDataResponse quickOrderToSpawn
    {
        set { onChange_quickOrderToSpawn.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetQuickOrderDataResponse> onChange_quickOrderToSpawn = new();
}