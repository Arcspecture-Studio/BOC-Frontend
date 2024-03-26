using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class TradingBotSystem : MonoBehaviour
{
    TradingBotComponent tradingBotComponent;
    WebsocketComponent websocketComponent;
    QuickTabComponent quickTabComponent;
    MiniPromptComponent miniPromptComponent;
    ProfileComponent profileComponent;

    void Start()
    {
        tradingBotComponent = GlobalComponent.instance.tradingBotComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;
        profileComponent = GlobalComponent.instance.profileComponent;

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
        ProfilePerference perference = profileComponent.activeProfile.preference;
        switch (value)
        {
            case BotTypeEnum.NONE:
                websocketComponent.generalRequests.Add(new General.WebsocketSaveTradingBotRequest(""));
                break;
            case BotTypeEnum.PREMIUM_INDEX:
                websocketComponent.generalRequests.Add(new General.WebsocketSaveTradingBotRequest("",
                    perference.symbol,
                    perference.lossPercentage,
                    perference.lossAmount,
                    perference.marginDistributionMode == MarginDistributionModeEnum.WEIGHTED,
                    perference.marginWeightDistributionValue,
                    perference.takeProfitType,
                    perference.riskRewardRatio,
                    perference.takeProfitTrailingCallbackPercentage,
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
        General.WebsocketGeneralRequest request = new General.WebsocketGeneralRequest(WebsocketEventTypeEnum.RETRIEVE_TRADING_BOTS);
        websocketComponent.generalRequests.Add(request);
    }
    void RetrieveTradingBotsFromServer()
    {
        string retrieveTradingBotsString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_TRADING_BOTS);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_TRADING_BOTS);
        if (retrieveTradingBotsString.IsNullOrEmpty()) return;

        General.WebsocketRetrieveTradingBotsResponse response = JsonConvert.DeserializeObject<General.WebsocketRetrieveTradingBotsResponse>(retrieveTradingBotsString, JsonSerializerConfig.settings);
        General.WebsocketRetrieveTradingBotsResponseData data;
        if (response.tradingBots.TryGetValue(""/*platformComponent.apiKey*/, out data))
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
        string retrieveNotificationString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SAVE_TRADING_BOT);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SAVE_TRADING_BOT);
        if (retrieveNotificationString.IsNullOrEmpty()) return;

        General.WebsocketSaveTradingBotsResponse response = JsonConvert.DeserializeObject<General.WebsocketSaveTradingBotsResponse>(retrieveNotificationString, JsonSerializerConfig.settings);
        if (response.tradingBotSpawned) miniPromptComponent.message = "Bot Spawned";
        else miniPromptComponent.message = "Bot Removed";
    }
}