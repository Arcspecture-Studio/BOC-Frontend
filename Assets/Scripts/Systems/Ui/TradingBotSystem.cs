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
    MiniPromptComponent miniPromptComponent;

    void Start()
    {
        tradingBotComponent = GlobalComponent.instance.tradingBotComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        preferenceComponent = GlobalComponent.instance.preferenceComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;

        tradingBotComponent.onChange_sendSignalToServer.AddListener(() => SendSignalToServer((BotTypeEnum)tradingBotComponent.tradingBotDropdown.value));
        tradingBotComponent.onChange_getTradingBots.AddListener(() => SendRetrieveTradingBotsSignal());
    }
    void Update()
    {
        RetrieveTradingBotsFromServer();
        RetrieveSaveTradinBotNotificationFromServer();
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
    void RetrieveSaveTradinBotNotificationFromServer() // PENDING: when there is multiple bots, all use RETRIEVE_TRADING_BOTS since we will retrieve all bots info in one go, RETRIEVE_TRADING_BOTS no longer only sent to frontend when frontend ask for it
    {
        string retrieveNotificationString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SAVE_TRADING_BOT.ToString());
        if (retrieveNotificationString.IsNullOrEmpty()) return;
        General.WebsocketSaveTradingBotsResponse response = JsonConvert.DeserializeObject<General.WebsocketSaveTradingBotsResponse>(retrieveNotificationString, JsonSerializerConfig.settings);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SAVE_TRADING_BOT.ToString());
        if (response.tradingBotSpawned) miniPromptComponent.message = "Bot Spawned";
        else miniPromptComponent.message = "Bot Removed";
    }
}