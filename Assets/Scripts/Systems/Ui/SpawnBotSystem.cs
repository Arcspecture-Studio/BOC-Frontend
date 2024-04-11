using UnityEngine;

public class SpawnBotSystem : MonoBehaviour
{
    SpawnBotComponent spawnTradingBotComponent;
    BotTabComponent botTabComponent;

    void Start()
    {
        spawnTradingBotComponent = GlobalComponent.instance.spawnTradingBotComponent;
        botTabComponent = GlobalComponent.instance.botTabComponent;

        spawnTradingBotComponent.onChange_botToSpawn.AddListener(SpawnTradingBot);
    }

    void SpawnTradingBot(General.WebsocketGetTradingBotResponse response)
    {
        if (botTabComponent.spawnedBotDataObjects.ContainsKey(response.id)) return;

        GameObject botDataRowObject = Instantiate(botTabComponent.botDataRowPrefab);
        botDataRowObject.transform.SetParent(botTabComponent.botDataRowParent, false);
        botTabComponent.spawnedBotDataObjects.TryAdd(response.id, botDataRowObject);

        BotDataRowComponent botDataRowComponent = botDataRowObject.GetComponent<BotDataRowComponent>();
        botDataRowComponent.botId = response.id;
        botDataRowComponent.botIdText.text = response.id;
        botDataRowComponent.botTypeText.text = response.botSetting.botType.ToString();
        botDataRowComponent.symbolText.text = response.quickOrderSetting.symbol.ToUpper();
    }
}