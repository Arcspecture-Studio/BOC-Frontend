using General;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class OrderPageRestoreDataSystem : MonoBehaviour
{
    OrderPageComponent orderPageComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;
    PlatformComponent platformComponent;
    ProfileComponent profileComponent;

    WebsocketRetrieveOrdersData orderData = null;

    void Start()
    {
        orderPageComponent = GetComponent<OrderPageComponent>();
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        profileComponent = GlobalComponent.instance.profileComponent;

        ProfilePerference preference = profileComponent.activeProfile.preference;
        if (!preference.symbol.IsNullOrEmpty())
        {
            orderPageComponent.symbolDropdownComponent.selectedSymbol = preference.symbol.ToUpper();
        }
        orderPageComponent.maxLossPercentageInput.text = preference.lossPercentage == 0 ? "" : preference.lossPercentage.ToString();
        orderPageComponent.maxLossAmountInput.text = preference.lossAmount == 0 ? "" : preference.lossAmount.ToString();
        orderPageComponent.marginDistributionModeDropdown.value = (int)preference.marginDistributionMode;
        orderPageComponent.marginWeightDistributionValueSlider.value = (float)preference.marginWeightDistributionValue;
        orderPageComponent.takeProfitTypeDropdown.value = (int)preference.takeProfitType;
        orderPageComponent.riskRewardRatioInput.text = preference.riskRewardRatio.ToString();
        orderPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)preference.takeProfitTrailingCallbackPercentage;
        orderPageComponent.takeProfitTrailingCallbackPercentageInput.text = preference.takeProfitTrailingCallbackPercentage.ToString();
        orderPageComponent.orderTypeDropdown.value = (int)preference.orderType;
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
        if (retrieveOrdersComponent.ordersFromServer.ContainsKey(platformComponent.activePlatform))
        {
            if (retrieveOrdersComponent.ordersFromServer[platformComponent.activePlatform].ContainsKey(orderPageComponent.orderId))
            {
                orderData = retrieveOrdersComponent.ordersFromServer[platformComponent.activePlatform][orderPageComponent.orderId];
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
        orderPageComponent.marginWeightDistributionValueSlider.value = (float)orderData.marginCalculator.quantityWeight;
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
