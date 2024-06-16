using TMPro;
using UnityEngine;

public class QuickOrderDataRowSystem : MonoBehaviour
{
    QuickOrderDataRowComponent quickOrderDataRowComponent;
    QuickTabComponent quickTabComponent;

    void Start()
    {
        quickOrderDataRowComponent = GetComponent<QuickOrderDataRowComponent>();
        quickTabComponent = GlobalComponent.instance.quickTabComponent;

        quickOrderDataRowComponent.infoButton.onClick.AddListener(OnClick_InfoButton);
        quickOrderDataRowComponent.closeButton.onClick.AddListener(OnClick_CloseButton);

        quickOrderDataRowComponent.infoPanel.SetActive(false);
    }

    void OnClick_CloseButton()
    {
        quickTabComponent.deleteFromServer = quickOrderDataRowComponent.orderId;
        quickOrderDataRowComponent.closeButton.interactable = false;
    }
    void OnClick_InfoButton()
    {
        bool isActive = !quickOrderDataRowComponent.infoPanel.activeSelf;
        quickOrderDataRowComponent.infoPanel.SetActive(isActive);

        if (isActive && !quickOrderDataRowComponent.infoPanelInstantiated)
        {
            quickOrderDataRowComponent.infoPanelInstantiated = true;
            InstantiateInfoPanelData();
        }
    }
    void DestroyInfoPanelData()
    {
        if (quickOrderDataRowComponent.infoPanelContent.childCount == 0) return;
        for (int i = 0; i < quickOrderDataRowComponent.infoPanelContent.childCount; i++)
        {
            Destroy(quickOrderDataRowComponent.infoPanelContent.GetChild(i).gameObject);
        }
    }
    void InstantiateInfoPanelData()
    {
        DestroyInfoPanelData();

        #region Id
        GameObject id = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text idText = id.GetComponent<TMP_Text>();
        idText.text = "Order Id: " + quickOrderDataRowComponent.orderId;
        #endregion

        #region Order
        Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);

        GameObject orderTitle = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text orderTitleText = orderTitle.GetComponent<TMP_Text>();
        orderTitleText.text = "Order";
        orderTitleText.fontStyle = FontStyles.Bold;

        GameObject symbol = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text symbolText = symbol.GetComponent<TMP_Text>();
        symbolText.text = "Symbol: " + quickOrderDataRowComponent.setting.order.symbol;

        if (quickOrderDataRowComponent.setting.order.lossAmount <= 0)
        {
            GameObject lossPercentage = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
            TMP_Text lossPercentageText = lossPercentage.GetComponent<TMP_Text>();
            lossPercentageText.text = "Max Loss Percentage: " + quickOrderDataRowComponent.setting.order.lossPercentage.ToString();
        }
        else
        {
            GameObject lossAmount = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
            TMP_Text lossAmountText = lossAmount.GetComponent<TMP_Text>();
            lossAmountText.text = "Max Loss Amount: " + quickOrderDataRowComponent.setting.order.lossAmount.ToString();
        }

        GameObject marginDistributionMode = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text marginDistributionModeText = marginDistributionMode.GetComponent<TMP_Text>();
        marginDistributionModeText.text = "Margin Distribution Mode: " + quickOrderDataRowComponent.setting.order.marginDistributionMode.ToString();

        if (quickOrderDataRowComponent.setting.order.marginDistributionMode == MarginDistributionModeEnum.WEIGHTED)
        {
            GameObject marginWeightDistributionValue = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
            TMP_Text marginWeightDistributionValueText = marginWeightDistributionValue.GetComponent<TMP_Text>();
            marginWeightDistributionValueText.text = "Margin Weight Distribution Value: " + quickOrderDataRowComponent.setting.order.marginWeightDistributionValue.ToString();
        }

        GameObject takeProfitType = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text takeProfitTypeText = takeProfitType.GetComponent<TMP_Text>();
        takeProfitTypeText.text = "Take Profit Type: " + quickOrderDataRowComponent.setting.order.takeProfitType.ToString();

        if (quickOrderDataRowComponent.setting.order.takeProfitType > TakeProfitTypeEnum.NONE)
        {
            GameObject riskRewardRatio = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
            TMP_Text riskRewardRatioText = riskRewardRatio.GetComponent<TMP_Text>();
            riskRewardRatioText.text = "Risk Reward Ratio: " + quickOrderDataRowComponent.setting.order.riskRewardRatio.ToString();

            GameObject takeProfitQuantityPercentage = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
            TMP_Text takeProfitQuantityPercentageText = takeProfitQuantityPercentage.GetComponent<TMP_Text>();
            takeProfitQuantityPercentageText.text = "Take Profit Quantity %: " + quickOrderDataRowComponent.setting.order.takeProfitQuantityPercentage.ToString();

            if (quickOrderDataRowComponent.setting.order.takeProfitType == TakeProfitTypeEnum.TRAILING)
            {
                GameObject takeProfitTrailingCallbackPercentage = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
                TMP_Text takeProfitTrailingCallbackPercentageText = takeProfitTrailingCallbackPercentage.GetComponent<TMP_Text>();
                takeProfitTrailingCallbackPercentageText.text = "Take Profit Trailing Callback %: " + quickOrderDataRowComponent.setting.order.takeProfitTrailingCallbackPercentage.ToString();
            }
        }

        GameObject orderType = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text orderTypeText = orderType.GetComponent<TMP_Text>();
        orderTypeText.text = "Order Type: " + quickOrderDataRowComponent.setting.order.orderType.ToString();
        #endregion

        #region Quick Order
        Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);

        GameObject quickOrderTitle = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text quickOrderTitleText = quickOrderTitle.GetComponent<TMP_Text>();
        quickOrderTitleText.text = "Quick Order";
        quickOrderTitleText.fontStyle = FontStyles.Bold;

        GameObject quickEntryTimes = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text quickEntryTimesText = quickEntryTimes.GetComponent<TMP_Text>();
        quickEntryTimesText.text = "Entry Times: " + quickOrderDataRowComponent.setting.quickOrder.quickEntryTimes.ToString();

        GameObject atrTimeframe = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text atrTimeframeText = atrTimeframe.GetComponent<TMP_Text>();
        atrTimeframeText.text = "ATR Timeframe: " + TimeframeArray.TIMEFRAME_ARRAY[(int)quickOrderDataRowComponent.setting.quickOrder.atrTimeframe];

        GameObject atrLength = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text atrLengthText = atrLength.GetComponent<TMP_Text>();
        atrLengthText.text = "ATR Length: " + quickOrderDataRowComponent.setting.quickOrder.atrLength.ToString();

        GameObject atrMultiplier = Instantiate(quickOrderDataRowComponent.infoPanelData, quickOrderDataRowComponent.infoPanelContent);
        TMP_Text atrMultiplierText = atrMultiplier.GetComponent<TMP_Text>();
        atrMultiplierText.text = "ATR Multiplier: " + quickOrderDataRowComponent.setting.quickOrder.atrMultiplier.ToString();
        #endregion

    }
}
