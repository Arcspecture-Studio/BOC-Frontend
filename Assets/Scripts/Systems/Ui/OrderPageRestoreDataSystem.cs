using General;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class OrderPageRestoreDataSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    OrderPageComponent orderPageComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;
    PlatformComponent platformComponent;
    PreferenceComponent preferenceComponent;

    WebsocketRetrieveOrdersData orderData = null;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        orderPageComponent = GetComponent<OrderPageComponent>();
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        preferenceComponent = GlobalComponent.instance.preferenceComponent;

        if (!preferenceComponent.symbol.IsNullOrEmpty())
        {
            orderPageComponent.symbolDropdownComponent.selectedSymbol = preferenceComponent.symbol.ToUpper();
        }
        orderPageComponent.maxLossPercentageInput.text = preferenceComponent.lossPercentage == 0 ? "" : preferenceComponent.lossPercentage.ToString();
        orderPageComponent.maxLossAmountInput.text = preferenceComponent.lossAmount == 0 ? "" : preferenceComponent.lossAmount.ToString();
        orderPageComponent.marginDistributionModeDropdown.value = (int)preferenceComponent.marginDistributionMode;
        orderPageComponent.marginWeightDistributionValueSlider.value = (float)preferenceComponent.marginWeightDistributionValue;
        orderPageComponent.takeProfitTypeDropdown.value = (int)preferenceComponent.takeProfitType;
        orderPageComponent.riskRewardRatioInput.text = preferenceComponent.riskRewardRatio.ToString();
        orderPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)preferenceComponent.takeProfitTrailingCallbackPercentage;
        orderPageComponent.takeProfitTrailingCallbackPercentageInput.text = preferenceComponent.takeProfitTrailingCallbackPercentage.ToString();
        orderPageComponent.orderTypeDropdown.value = (int)preferenceComponent.orderType;
    }
    void Update()
    {
        if (!orderPageComponent.restoreData) return;
        orderPageComponent.restoreData = false;
        ReadData();
        ApplyDataIntoGameObject();
    }
    void ReadData()
    {
        if (retrieveOrdersComponent.ordersFromServer.ContainsKey(platformComponent.tradingPlatform))
        {
            if (retrieveOrdersComponent.ordersFromServer[platformComponent.tradingPlatform].ContainsKey(orderPageComponent.orderId))
            {
                orderData = retrieveOrdersComponent.ordersFromServer[platformComponent.tradingPlatform][orderPageComponent.orderId];
                return;
            }
        }
    }
    void ApplyDataIntoGameObject()
    {
        if (orderData == null) return;
        orderPageComponent.calculate = true;
        orderPageComponent.orderStatus = orderData.status;
        orderPageComponent.orderStatusError = orderData.statusError;
        orderPageComponent.symbolDropdownComponent.dropdown.value = orderPageComponent.symbolDropdownComponent.symbols.IndexOf(orderData.symbol);
        orderPageComponent.symbolDropdownComponent.selectedSymbol = orderData.symbol;
        orderPageComponent.maxLossPercentageInput.text = Utils.RoundTwoDecimal(Utils.RateToPercentage(orderData.marginCalculator.balanceDecrementRate)).ToString();
        orderPageComponent.maxLossAmountInput.text = Utils.RoundTwoDecimal(orderData.marginCalculator.amountToLoss).ToString();
        #region Removed all the price input objects
        for (int i = orderPageComponent.inputEntryPricesComponent.parent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(orderPageComponent.inputEntryPricesComponent.parent.GetChild(i).gameObject);
        }
        orderPageComponent.inputEntryPricesComponent.entryPriceInputs.Clear();
        orderPageComponent.inputEntryPricesComponent.entryPriceCloseButtons.Clear();
        #endregion
        #region Reinstantiate all the price input objects
        for (int i = 0; i < orderData.marginCalculator.entryPrices.Count; i++)
        {
            GameObject priceInputObject = Instantiate(orderPageComponent.inputEntryPricesComponent.priceInput, orderPageComponent.inputEntryPricesComponent.parent);
            orderPageComponent.inputEntryPricesComponent.entryPriceInputs.Add(priceInputObject.transform.GetChild(0).GetComponent<TMP_InputField>());
            orderPageComponent.inputEntryPricesComponent.entryPriceCloseButtons.Add(priceInputObject.transform.GetChild(1).GetComponent<Button>());
            orderPageComponent.inputEntryPricesComponent.entryPriceInputs[i].text = Utils.RoundTwoDecimal(orderData.marginCalculator.entryPrices[i]).ToString();
        }
        #endregion
        orderPageComponent.entryTimesInput.text = orderData.marginCalculator.entryPrices.Count.ToString();
        orderPageComponent.stopLossInput.text = Utils.RoundTwoDecimal(orderData.marginCalculator.stopLossPrice).ToString();
        orderPageComponent.takeProfitTypeDropdown.value = (int)orderData.takeProfitType;
        orderPageComponent.riskRewardRatioInput.text = orderData.marginCalculator.riskRewardRatio.ToString();
        orderPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)orderData.marginCalculator.takeProfitTrailingCallbackPercentage;
        orderPageComponent.takeProfitTrailingCallbackPercentageInput.text = orderData.marginCalculator.takeProfitTrailingCallbackPercentage.ToString();
        orderPageComponent.orderTypeDropdown.value = (int)orderData.orderType;
        orderPageComponent.marginDistributionModeDropdown.value = orderData.marginCalculator.weightedQuantity ? 1 : 0;
        orderPageComponent.marginWeightDistributionValueSlider.value = (float)orderData.marginCalculator.quantityWeight / orderPagesComponent.marginWeightDistributionRange;
        orderPageComponent.marginCalculator = orderData.marginCalculator;
        orderPageComponent.positionInfoAvgEntryPriceFilledText.text = Utils.RoundNDecimal(orderData.averagePriceFilled, platformComponent.pricePrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
        orderPageComponent.positionInfoQuantityFilledText.text = Utils.RoundNDecimal(orderData.quantityFilled, platformComponent.quantityPrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
        orderPageComponent.positionInfoActualTakeProfitPriceText.text = Utils.RoundNDecimal(orderData.actualTakeProfitPrice, platformComponent.pricePrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
        orderPageComponent.positionInfoPaidFundingAmount.text = orderData.paidFundingAmount.ToString();
        foreach (KeyValuePair<string, WebsocketRetrieveThrottleOrdersData> throttleOrder in orderData.throttleOrders)
        {
            GameObject throttleTabObject = Instantiate(orderPageComponent.throttleParentComponent.throttleTabPrefab, orderPageComponent.throttleParentComponent.transform);
            OrderPageThrottleComponent throttleComponent = throttleTabObject.GetComponent<OrderPageThrottleComponent>();
            throttleComponent.calculate = true;
            throttleComponent.orderId = throttleOrder.Key;
            throttleComponent.orderStatus = throttleOrder.Value.status;
            throttleComponent.orderStatusError = throttleOrder.Value.statusError;
            throttleComponent.throttleCalculator = throttleOrder.Value.throttleCalculator;
            throttleComponent.pnlInput.text = throttleOrder.Value.throttleCalculator.realizedPnl.ToString();
            if (throttleOrder.Value.throttleCalculator.throttleQty > 0)
            {
                throttleComponent.throttlePriceInput.text = throttleOrder.Value.throttleCalculator.throttlePrice.ToString();
                throttleComponent.throttleQuantityInput.text = throttleOrder.Value.throttleCalculator.throttleQty.ToString();
            }
            throttleComponent.orderTypeDropdown.value = (int)throttleOrder.Value.orderType;
        }
    }
}
