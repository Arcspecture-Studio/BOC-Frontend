using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class TradingBotSystem : MonoBehaviour
{
    TradingBotComponent tradingBotComponent;
    WebsocketComponent websocketComponent;
    PlatformComponent platformComponent;
    PreferenceComponent preferenceComponent;
    QuickTabComponent quickTabComponent;

    void Start()
    {
        tradingBotComponent = GlobalComponent.instance.tradingBotComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        preferenceComponent = GlobalComponent.instance.preferenceComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;

        tradingBotComponent.onChange_sendSignalToServer.AddListener(() => SendSignalToServer((BotTypeEnum)tradingBotComponent.tradingBotDropdown.value));
        tradingBotComponent.onChange_getTradingBots.AddListener(() => SendRetrieveTradingBotsSignal());
    }
    void Update()
    {
        RetrieveTradingBotsFromServer();
    }

    void SendSignalToServer(BotTypeEnum value)
    {
        switch (value)
        {
            case BotTypeEnum.NONE:
                websocketComponent.generalRequests.Add(new General.WebsocketSaveTradingBotRequest(platformComponent.tradingPlatform, platformComponent.apiKey));
                break;
            case BotTypeEnum.PREMIUM_INDEX:
                double normalizedMarginWeightDistributionValue = preferenceComponent.marginWeightDistributionValue * OrderConfig.MARGIN_WEIGHT_DISTRIBUTION_RANGE;
                websocketComponent.generalRequests.Add(new General.WebsocketSaveTradingBotRequest(
                    platformComponent.tradingPlatform,
                    platformComponent.apiKey,
                    preferenceComponent.symbol,
                    preferenceComponent.lossPercentage,
                    preferenceComponent.lossAmount,
                    preferenceComponent.marginDistributionMode == MarginDistributionModeEnum.WEIGHTED,
                    normalizedMarginWeightDistributionValue,
                    preferenceComponent.takeProfitType,
                    preferenceComponent.riskRewardRatio,
                    preferenceComponent.takeProfitTrailingCallbackPercentage,
                    int.Parse(quickTabComponent.entryTimesInput.text),
                    WebsocketIntervalEnum.array[quickTabComponent.atrTimeframeDropdown.value],
                    int.Parse(quickTabComponent.atrLengthInput.text),
                    double.Parse(quickTabComponent.atrMultiplierInput.text)
                ));
                break;
        }
    }
    void SendRetrieveTradingBotsSignal()
    {
        General.WebsocketGeneralRequest request = new General.WebsocketGeneralRequest(WebsocketEventTypeEnum.RETRIEVE_TRADING_BOTS, platformComponent.tradingPlatform);
        websocketComponent.generalRequests.Add(request);
    }
    void RetrieveTradingBotsFromServer()
    {
        string retrieveTradingBotsString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_TRADING_BOTS.ToString());
        if (retrieveTradingBotsString.IsNullOrEmpty()) return;
        General.WebsocketRetrieveTradingBotsResponse response = JsonConvert.DeserializeObject<General.WebsocketRetrieveTradingBotsResponse>(retrieveTradingBotsString, JsonSerializerConfig.settings);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_TRADING_BOTS.ToString());
        General.WebsocketRetrieveTradingBotsResponseData data;
        if (response.tradingBots.TryGetValue(platformComponent.apiKey, out data))
        {
            tradingBotComponent.tradingBotDropdown.value = (int)data.botType;
        }
        else
        {
            tradingBotComponent.tradingBotDropdown.value = 0;
        }
    }
}