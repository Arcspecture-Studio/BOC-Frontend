using UnityEngine;
using UnityEngine.Events;

public class SpawnQuickOrderComponent : MonoBehaviour
{
    public General.WebsocketGetQuickOrderResponse quickOrderToSpawn
    {
        set { onChange_quickOrderToSpawn.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetQuickOrderResponse> onChange_quickOrderToSpawn = new();
}