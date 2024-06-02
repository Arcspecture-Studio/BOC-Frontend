using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOrderSystem : MonoBehaviour
{
    SpawnOrderComponent spawnOrderComponent;
    OrderPagesComponent orderPagesComponent;
    PlatformComponent platformComponent;

    void Start()
    {
        spawnOrderComponent = GlobalComponent.instance.spawnOrderComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        platformComponent = GlobalComponent.instance.platformComponent;

        spawnOrderComponent.onChange_orderToSpawn.AddListener(SpawnOrder);
    }

    void SpawnOrder(General.WebsocketGetOrderResponse response)
    {
        GameObject orderPageObject = Instantiate(orderPagesComponent.orderPagePrefab, orderPagesComponent.transform, false);
        OrderPageComponent orderPageComponent = orderPageObject.GetComponent<OrderPageComponent>();
        orderPageComponent.rectTransform.localScale = new Vector2(0, 0);
        orderPagesComponent.status = OrderPagesStatusEnum.DETACH;
        orderPagesComponent.currentPageIndex = orderPagesComponent.transform.childCount;

        #region Apply data into game object
        orderPageComponent.instantiateWithData = true;
        orderPageComponent.orderId = response.id;
        orderPageComponent.spawnTime = response.spawnTime;
        orderPageComponent.calculate = true;
        orderPageComponent.orderStatus = response.status;
        orderPageComponent.orderStatusError = response.statusError;
        orderPageComponent.tradingBotId = response.tradingBotId;
        orderPageComponent.symbolDropdownComponent.dropdown.value = orderPageComponent.symbolDropdownComponent.symbols.IndexOf(response.symbol);
        orderPageComponent.symbolDropdownComponent.selectedSymbol = response.symbol;
        orderPageComponent.maxLossPercentageInput.text = Utils.RoundTwoDecimal(Utils.RateToPercentage(response.marginCalculator.balanceDecrementRate)).ToString();
        orderPageComponent.maxLossAmountInput.text = Utils.RoundTwoDecimal(response.marginCalculator.amountToLoss).ToString();
        #region Removed all the price input objects
        for (int i = orderPageComponent.inputEntryPricesComponent.parent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(orderPageComponent.inputEntryPricesComponent.parent.GetChild(i).gameObject);
        }
        orderPageComponent.inputEntryPricesComponent.entryPriceInputs.Clear();
        orderPageComponent.inputEntryPricesComponent.entryPriceCloseButtons.Clear();
        #endregion
        #region Reinstantiate all the price input objects
        for (int i = 0; i < response.marginCalculator.entryPrices.Count; i++)
        {
            GameObject priceInputObject = Instantiate(orderPageComponent.inputEntryPricesComponent.priceInput, orderPageComponent.inputEntryPricesComponent.parent);
            orderPageComponent.inputEntryPricesComponent.entryPriceInputs.Add(priceInputObject.transform.GetChild(0).GetComponent<TMP_InputField>());
            orderPageComponent.inputEntryPricesComponent.entryPriceCloseButtons.Add(priceInputObject.transform.GetChild(1).GetComponent<Button>());
            orderPageComponent.inputEntryPricesComponent.entryPriceInputs[i].text = Utils.RoundTwoDecimal(response.marginCalculator.entryPrices[i]).ToString();
        }
        #endregion
        orderPageComponent.entryTimesInput.text = response.marginCalculator.entryPrices.Count.ToString();
        orderPageComponent.stopLossInput.text = Utils.RoundTwoDecimal(response.marginCalculator.stopLossPrice).ToString();
        orderPageComponent.takeProfitTypeDropdown.value = (int)response.marginCalculator.takeProfitType;
        orderPageComponent.riskRewardRatioInput.text = response.marginCalculator.riskRewardRatio.ToString();
        orderPageComponent.takeProfitQuantityPercentageCustomSlider.SetValue(response.marginCalculator.takeProfitQuantityPercentage);
        orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.SetValue(response.marginCalculator.takeProfitTrailingCallbackPercentage);
        orderPageComponent.orderTypeDropdown.value = (int)response.orderType;
        orderPageComponent.marginDistributionModeDropdown.value = response.marginCalculator.weightedQuantity ? 1 : 0;
        orderPageComponent.marginWeightDistributionValueCustomSlider.SetValue(response.marginCalculator.quantityWeight);
        orderPageComponent.marginCalculator = response.marginCalculator;
        orderPageComponent.positionInfoAvgEntryPriceFilledText.text = Utils.RoundNDecimal(response.averagePriceFilled, platformComponent.pricePrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
        orderPageComponent.positionInfoQuantityFilledText.text = Utils.RoundNDecimal(response.quantityFilled, platformComponent.quantityPrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
        orderPageComponent.positionInfoActualTakeProfitPriceText.text = Utils.RoundNDecimal(response.actualTakeProfitPrice, platformComponent.pricePrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
        orderPageComponent.positionInfoPaidFundingAmount.text = response.paidFundingAmount.ToString();
        foreach (General.WebsocketGetThrottleOrderResponse throttleOrder in response.throttleOrders)
        {
            GameObject throttleTabObject = Instantiate(orderPageComponent.throttleParentComponent.throttleTabPrefab, orderPageComponent.throttleParentComponent.transform);
            OrderPageThrottleComponent throttleComponent = throttleTabObject.GetComponent<OrderPageThrottleComponent>();
            throttleComponent.calculate = true;
            throttleComponent.orderId = throttleOrder.id;
            throttleComponent.orderStatus = throttleOrder.status;
            throttleComponent.orderStatusError = throttleOrder.statusError;
            throttleComponent.throttleCalculator = throttleOrder.throttleCalculator;
            throttleComponent.pnlInput.text = throttleOrder.throttleCalculator.realizedPnl.ToString();
            if (throttleOrder.throttleCalculator.throttleQty > 0)
            {
                throttleComponent.throttlePriceInput.text = throttleOrder.throttleCalculator.throttlePrice.ToString();
                throttleComponent.throttleQuantityInput.text = throttleOrder.throttleCalculator.throttleQty.ToString();
            }
            throttleComponent.orderTypeDropdown.value = (int)throttleOrder.orderType;
        }
        #endregion
    }
}