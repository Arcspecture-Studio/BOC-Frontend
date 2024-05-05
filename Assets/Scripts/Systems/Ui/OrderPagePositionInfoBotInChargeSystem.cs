using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class OrderPagePositionInfoBotInChargeSystem : MonoBehaviour
{
    OrderPageComponent orderPageComponent;
    BotTabComponent botTabComponent;

    int spawnedTradingBotObjectCount = -1;
    List<string> mapper;

    void Start()
    {
        orderPageComponent = GetComponent<OrderPageComponent>();
        botTabComponent = GlobalComponent.instance.botTabComponent;

        orderPageComponent.positionInfoBotInChargeDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
    void Update()
    {
        ShowAndHideBotInChargeObject();
    }

    void ShowAndHideBotInChargeObject()
    {
        if (spawnedTradingBotObjectCount == botTabComponent.spawnedBotDataObjects.Count) return;
        spawnedTradingBotObjectCount = botTabComponent.spawnedBotDataObjects.Count;
        bool show = spawnedTradingBotObjectCount > 0;
        orderPageComponent.positionInfoBotInChargeObject.SetActive(show);
        if (show) InitializeAndSetDropdownOption();
    }
    void InitializeAndSetDropdownOption()
    {
        List<TMP_Dropdown.OptionData> optionDataList = new(){
            new TMP_Dropdown.OptionData("NONE")
        };
        mapper = new() { "" };
        foreach (KeyValuePair<string, GameObject> obj in botTabComponent.spawnedBotDataObjects)
        {
            BotDataRowComponent botDataRowComponent = obj.Value.GetComponent<BotDataRowComponent>();
            string optionName = botDataRowComponent.botId + " (" + botDataRowComponent.botTypeText.text + ")";
            optionDataList.Add(new TMP_Dropdown.OptionData(optionName));
            mapper.Add(botDataRowComponent.botId);
        }
        orderPageComponent.positionInfoBotInChargeDropdown.options = optionDataList;
        if (!orderPageComponent.tradingBotId.IsNullOrEmpty())
        {
            orderPageComponent.positionInfoBotInChargeDropdown.value = mapper.IndexOf(orderPageComponent.tradingBotId);
        }
    }
    void OnDropdownValueChanged(int value)
    {
        string botId = mapper[value];
        if (orderPageComponent.tradingBotId == botId) return;
        orderPageComponent.tradingBotId = botId;
        orderPageComponent.updateToServer = true;
    }
}