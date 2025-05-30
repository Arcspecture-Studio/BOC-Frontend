using UnityEngine;
using UnityEngine.Events;

public class SpawnBotComponent : MonoBehaviour
{
    public General.WebsocketGetTradingBotDataResponse botToSpawn
    {
        set { onChange_botToSpawn.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetTradingBotDataResponse> onChange_botToSpawn = new();
}