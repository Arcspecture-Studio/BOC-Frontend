using UnityEngine;
using UnityEngine.Events;

public class SpawnTradingBotComponent : MonoBehaviour
{
    public General.WebsocketGetTradingBotResponse botToSpawn
    {
        set { onChange_botToSpawn.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetTradingBotResponse> onChange_botToSpawn = new();
}