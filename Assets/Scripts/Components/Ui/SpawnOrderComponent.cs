using UnityEngine;
using UnityEngine.Events;

public class SpawnOrderComponent : MonoBehaviour
{
    public General.WebsocketGetOrderDataResponse orderToSpawn
    {
        set { onChange_orderToSpawn.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetOrderDataResponse> onChange_orderToSpawn = new();
}