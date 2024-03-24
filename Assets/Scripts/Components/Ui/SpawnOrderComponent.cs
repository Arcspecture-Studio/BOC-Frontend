using UnityEngine;
using UnityEngine.Events;

public class SpawnOrderComponent : MonoBehaviour
{
    public General.WebsocketGetOrderResponse orderToSpawn
    {
        set { onChange_orderToSpawn.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetOrderResponse> onChange_orderToSpawn = new();
}