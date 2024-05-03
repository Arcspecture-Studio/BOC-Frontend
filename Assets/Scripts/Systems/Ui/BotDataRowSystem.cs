using TMPro;
using UnityEngine;

public class BotDataRowSystem : MonoBehaviour
{
    BotDataRowComponent botDataRowComponent;
    BotTabComponent botTabComponent;

    void Start()
    {
        botDataRowComponent = GetComponent<BotDataRowComponent>();
        botTabComponent = GlobalComponent.instance.botTabComponent;

        botDataRowComponent.infoButton.onClick.AddListener(OnClick_InfoButton);
        botDataRowComponent.closeButton.onClick.AddListener(OnClick_CloseButton);

        botDataRowComponent.infoPanel.SetActive(false);
    }

    void OnClick_CloseButton()
    {
        botTabComponent.deleteFromServer = botDataRowComponent.botId;
        botDataRowComponent.closeButton.interactable = false;
    }
    void OnClick_InfoButton()
    {
        bool isActive = !botDataRowComponent.infoPanel.activeSelf;
        botDataRowComponent.infoPanel.SetActive(isActive);

        if (isActive && !botDataRowComponent.infoPanelInstantiated)
        {
            botDataRowComponent.infoPanelInstantiated = true;
            InstantiateInfoPanelData();
        }
    }
    void DestroyInfoPanelData()
    {
        if (botDataRowComponent.infoPanelContent.childCount == 0) return;
        for (int i = 0; i < botDataRowComponent.infoPanelContent.childCount; i++)
        {
            Destroy(botDataRowComponent.infoPanelContent.GetChild(i).gameObject);
        }
    }
    void InstantiateInfoPanelData()
    {
        DestroyInfoPanelData();

        #region Id
        GameObject id = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text idText = id.GetComponent<TMP_Text>();
        idText.text = "Bot Id: " + botDataRowComponent.botId;
        #endregion

        #region Order
        Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);

        GameObject orderTitle = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text orderTitleText = orderTitle.GetComponent<TMP_Text>();
        orderTitleText.text = "Order";
        orderTitleText.fontStyle = FontStyles.Bold;

        GameObject symbol = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text symbolText = symbol.GetComponent<TMP_Text>();
        symbolText.text = "Symbol: " + botDataRowComponent.setting.order.symbol;

        GameObject lossPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text lossPercentageText = lossPercentage.GetComponent<TMP_Text>();
        lossPercentageText.text = "Max Loss Percentage: " + botDataRowComponent.setting.order.lossPercentage.ToString();

        GameObject lossAmount = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text lossAmountText = lossAmount.GetComponent<TMP_Text>();
        lossAmountText.text = "Max Loss Amount: " + botDataRowComponent.setting.order.lossAmount.ToString();

        GameObject marginDistributionMode = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text marginDistributionModeText = marginDistributionMode.GetComponent<TMP_Text>();
        marginDistributionModeText.text = "Margin Distribution Mode: " + botDataRowComponent.setting.order.marginDistributionMode.ToString();

        GameObject marginWeightDistributionValue = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text marginWeightDistributionValueText = marginWeightDistributionValue.GetComponent<TMP_Text>();
        marginWeightDistributionValueText.text = "Margin Weight Distribution Value: " + botDataRowComponent.setting.order.marginWeightDistributionValue.ToString();

        GameObject takeProfitType = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text takeProfitTypeText = takeProfitType.GetComponent<TMP_Text>();
        takeProfitTypeText.text = "Take Profit Type: " + botDataRowComponent.setting.order.takeProfitType.ToString();

        GameObject riskRewardRatio = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text riskRewardRatioText = riskRewardRatio.GetComponent<TMP_Text>();
        riskRewardRatioText.text = "Risk Reward Ratio: " + botDataRowComponent.setting.order.riskRewardRatio.ToString();

        GameObject takeProfitTrailingCallbackPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text takeProfitTrailingCallbackPercentageText = takeProfitTrailingCallbackPercentage.GetComponent<TMP_Text>();
        takeProfitTrailingCallbackPercentageText.text = "Take Profit Trailing Callback %: " + botDataRowComponent.setting.order.takeProfitTrailingCallbackPercentage.ToString();

        GameObject orderType = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text orderTypeText = orderType.GetComponent<TMP_Text>();
        orderTypeText.text = "Order Type: " + botDataRowComponent.setting.order.orderType.ToString();
        #endregion

        #region Quick Order
        Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);

        GameObject quickOrderTitle = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text quickOrderTitleText = quickOrderTitle.GetComponent<TMP_Text>();
        quickOrderTitleText.text = "Quick Order";
        quickOrderTitleText.fontStyle = FontStyles.Bold;

        GameObject quickEntryTimes = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text quickEntryTimesText = quickEntryTimes.GetComponent<TMP_Text>();
        quickEntryTimesText.text = "Entry Times: " + botDataRowComponent.setting.quickOrder.quickEntryTimes.ToString();

        GameObject atrTimeframe = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text atrTimeframeText = atrTimeframe.GetComponent<TMP_Text>();
        atrTimeframeText.text = "ATR Timeframe: " + OrderConfig.TIMEFRAME_ARRAY[(int)botDataRowComponent.setting.quickOrder.atrTimeframe];

        GameObject atrLength = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text atrLengthText = atrLength.GetComponent<TMP_Text>();
        atrLengthText.text = "ATR Length: " + botDataRowComponent.setting.quickOrder.atrLength.ToString();

        GameObject atrMultiplier = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text atrMultiplierText = atrMultiplier.GetComponent<TMP_Text>();
        atrMultiplierText.text = "ATR Multiplier: " + botDataRowComponent.setting.quickOrder.atrMultiplier.ToString();
        #endregion

        #region Bot
        Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);

        GameObject botTitle = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text botTitleText = botTitle.GetComponent<TMP_Text>();
        botTitleText.text = "Bot";
        botTitleText.fontStyle = FontStyles.Bold;

        GameObject botType = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text botTypeText = botType.GetComponent<TMP_Text>();
        botTypeText.text = "Bot Type: " + botDataRowComponent.setting.bot.botType.ToString();

        GameObject longOrderLimit = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text longOrderLimitText = longOrderLimit.GetComponent<TMP_Text>();
        longOrderLimitText.text = "Long Order Limit: " + botDataRowComponent.setting.bot.longOrderLimit.ToString();

        GameObject shortOrderLimit = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text shortOrderLimitText = shortOrderLimit.GetComponent<TMP_Text>();
        shortOrderLimitText.text = "Short Order Limit: " + botDataRowComponent.setting.bot.shortOrderLimit.ToString();

        GameObject autoDestroyOrder = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text autoDestroyOrderText = autoDestroyOrder.GetComponent<TMP_Text>();
        autoDestroyOrderText.text = "Auto Destroy Order: " + botDataRowComponent.setting.bot.autoDestroyOrder.ToString();

        switch (botDataRowComponent.setting.bot.botType)
        {
            case BotTypeEnum.PREMIUM_INDEX:
                GameObject longThresholdPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text longThresholdPercentageText = longThresholdPercentage.GetComponent<TMP_Text>();
                longThresholdPercentageText.text = "Long Threshold %: " + botDataRowComponent.setting.bot.premiumIndex.longThresholdPercentage.ToString();

                GameObject shortThresholdPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text shortThresholdPercentageText = shortThresholdPercentage.GetComponent<TMP_Text>();
                shortThresholdPercentageText.text = "Short Threshold %: " + botDataRowComponent.setting.bot.premiumIndex.shortThresholdPercentage.ToString();

                GameObject candleLength = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text candleLengthText = candleLength.GetComponent<TMP_Text>();
                candleLengthText.text = "Candle Length: " + botDataRowComponent.setting.bot.premiumIndex.candleLength.ToString();
                break;
        }
        #endregion
    }
}
