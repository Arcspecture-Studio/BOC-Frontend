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

        if (botDataRowComponent.setting.order.lossAmount <= 0)
        {
            GameObject lossPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
            TMP_Text lossPercentageText = lossPercentage.GetComponent<TMP_Text>();
            lossPercentageText.text = "Max Loss Percentage: " + botDataRowComponent.setting.order.lossPercentage.ToString();
        }
        else
        {
            GameObject lossAmount = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
            TMP_Text lossAmountText = lossAmount.GetComponent<TMP_Text>();
            lossAmountText.text = "Max Loss Amount: " + botDataRowComponent.setting.order.lossAmount.ToString();
        }


        GameObject marginDistributionMode = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text marginDistributionModeText = marginDistributionMode.GetComponent<TMP_Text>();
        marginDistributionModeText.text = "Margin Distribution Mode: " + botDataRowComponent.setting.order.marginDistributionMode.ToString();

        if (botDataRowComponent.setting.order.marginDistributionMode == MarginDistributionModeEnum.WEIGHTED)
        {
            GameObject marginWeightDistributionValue = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
            TMP_Text marginWeightDistributionValueText = marginWeightDistributionValue.GetComponent<TMP_Text>();
            marginWeightDistributionValueText.text = "Margin Weight Distribution Value: " + botDataRowComponent.setting.order.marginWeightDistributionValue.ToString();
        }

        GameObject takeProfitType = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text takeProfitTypeText = takeProfitType.GetComponent<TMP_Text>();
        takeProfitTypeText.text = "Take Profit Type: " + botDataRowComponent.setting.order.takeProfitType.ToString();

        if (botDataRowComponent.setting.order.takeProfitType > TakeProfitTypeEnum.NONE)
        {
            GameObject riskRewardRatio = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
            TMP_Text riskRewardRatioText = riskRewardRatio.GetComponent<TMP_Text>();
            riskRewardRatioText.text = "Risk Reward Ratio: " + botDataRowComponent.setting.order.riskRewardRatio.ToString();

            GameObject takeProfitQuantityPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
            TMP_Text takeProfitQuantityPercentageText = takeProfitQuantityPercentage.GetComponent<TMP_Text>();
            takeProfitQuantityPercentageText.text = "Take Profit Quantity %: " + botDataRowComponent.setting.order.takeProfitQuantityPercentage.ToString();

            if (botDataRowComponent.setting.order.takeProfitType == TakeProfitTypeEnum.TRAILING)
            {
                GameObject takeProfitTrailingCallbackPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text takeProfitTrailingCallbackPercentageText = takeProfitTrailingCallbackPercentage.GetComponent<TMP_Text>();
                takeProfitTrailingCallbackPercentageText.text = "Take Profit Trailing Callback %: " + botDataRowComponent.setting.order.takeProfitTrailingCallbackPercentage.ToString();
            }
        }

        GameObject orderType = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text orderTypeText = orderType.GetComponent<TMP_Text>();
        orderTypeText.text = "Order Type: " + botDataRowComponent.setting.order.orderType.ToString();

        GameObject fundingFeeHandler = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
        TMP_Text fundingFeeHandlerText = fundingFeeHandler.GetComponent<TMP_Text>();
        fundingFeeHandlerText.text = "Order Type: " + botDataRowComponent.setting.order.fundingFeeHandler.ToString();
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
        atrTimeframeText.text = "ATR Timeframe: " + TimeframeArray.TIMEFRAME_ARRAY[(int)botDataRowComponent.setting.quickOrder.atrTimeframe];

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
                GameObject premiumIndex_longThresholdPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text premiumIndex_longThresholdPercentageText = premiumIndex_longThresholdPercentage.GetComponent<TMP_Text>();
                premiumIndex_longThresholdPercentageText.text = "Long Threshold %: " + botDataRowComponent.setting.bot.premiumIndex.longThresholdPercentage.ToString();

                GameObject premiumIndex_shortThresholdPercentage = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text premiumIndex_shortThresholdPercentageText = premiumIndex_shortThresholdPercentage.GetComponent<TMP_Text>();
                premiumIndex_shortThresholdPercentageText.text = "Short Threshold %: " + botDataRowComponent.setting.bot.premiumIndex.shortThresholdPercentage.ToString();

                GameObject premiumIndex_averageCandleLength = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text premiumIndex_averageCandleLengthText = premiumIndex_averageCandleLength.GetComponent<TMP_Text>();
                premiumIndex_averageCandleLengthText.text = "Average Candle Length: " + botDataRowComponent.setting.bot.premiumIndex.averageCandleLength.ToString();

                GameObject premiumIndex_reverseCandleBuffer = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text premiumIndex_reverseCandleBufferText = premiumIndex_reverseCandleBuffer.GetComponent<TMP_Text>();
                premiumIndex_reverseCandleBufferText.text = "Reverse Candle Buffer: " + botDataRowComponent.setting.bot.premiumIndex.reverseCandleBuffer.ToString();

                GameObject premiumIndex_reverseCandleConfirmation = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text premiumIndex_reverseCandleConfirmationText = premiumIndex_reverseCandleConfirmation.GetComponent<TMP_Text>();
                premiumIndex_reverseCandleConfirmationText.text = "Reverse Candle Confirmation: " + botDataRowComponent.setting.bot.premiumIndex.reverseCandleConfirmation.ToString();
                break;
            case BotTypeEnum.MCDX:
                GameObject mcdx_timeframe = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text mcdx_timeframeText = mcdx_timeframe.GetComponent<TMP_Text>();
                mcdx_timeframeText.text = "Timeframe: " + TimeframeArray.TIMEFRAME_ARRAY[(int)botDataRowComponent.setting.bot.mcdx.timeframe];

                GameObject mcdx_averageCandleLength = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text mcdx_averageCandleLengthText = mcdx_averageCandleLength.GetComponent<TMP_Text>();
                mcdx_averageCandleLengthText.text = "Average Candle Length: " + botDataRowComponent.setting.bot.mcdx.averageCandleLength.ToString();

                GameObject mcdx_fomoCandleConfirmation = Instantiate(botDataRowComponent.infoPanelData, botDataRowComponent.infoPanelContent);
                TMP_Text mcdx_fomoCandleConfirmationText = mcdx_fomoCandleConfirmation.GetComponent<TMP_Text>();
                mcdx_fomoCandleConfirmationText.text = "Fomo Candle Confirmation: " + botDataRowComponent.setting.bot.mcdx.fomoCandleConfirmation.ToString();
                break;
        }
        #endregion
    }
}
