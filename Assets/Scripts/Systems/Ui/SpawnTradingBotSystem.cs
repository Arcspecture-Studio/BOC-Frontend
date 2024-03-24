using UnityEngine;
using UnityEngine.Events;

public class SpawnTradingBotSystem : MonoBehaviour
{
    SpawnTradingBotComponent spawnTradingBotComponent;

    void Start()
    {
        spawnTradingBotComponent = GlobalComponent.instance.spawnTradingBotComponent;

        spawnTradingBotComponent.onChange_botToSpawn.AddListener(SpawnTradingBot);
    }

    void SpawnTradingBot(General.WebsocketGetTradingBotResponse response)
    {

    }
}