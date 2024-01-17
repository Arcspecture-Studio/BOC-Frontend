using UnityEngine;

public class TradingBotSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    WebsocketComponent websocketComponent;
    PlatformComponent platformComponent;
    PreferenceComponent preferenceComponent;
    QuickTabComponent quickTabComponent;

    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        preferenceComponent = GlobalComponent.instance.preferenceComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;

        settingPageComponent.tradingBotDropdown.onValueChanged.AddListener(value =>
        {
            switch ((BotTypeEnum)value)
            {
                case BotTypeEnum.NONE:
                    websocketComponent.generalRequests.Add(new General.WebsocketSaveTradingBotRequest(platformComponent.tradingPlatform));
                    break;
                case BotTypeEnum.PREMIUM_INDEX:
                    double normalizedMarginWeightDistributionValue = preferenceComponent.marginWeightDistributionValue * OrderConfig.MARGIN_WEIGHT_DISTRIBUTION_RANGE;
                    websocketComponent.generalRequests.Add(new General.WebsocketSaveTradingBotRequest(
                        platformComponent.tradingPlatform,
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
        });
    }
}