using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using WebSocketSharp;

public class OrderPageSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    OrderPageComponent orderPageComponent;
    PlatformComponent platformComponent;
    PreferenceComponent preferenceComponent;
    WebsocketComponent websocketComponent;
    PromptComponent promptComponent;

    bool? lockForEdit = null;
    OrderStatusEnum? orderStatus = null;
    bool calculateButtonPressed = false;

    static Color green = new Color(0, 108 / 255f, 0);

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        orderPageComponent = GetComponent<OrderPageComponent>();
        platformComponent = GlobalComponent.instance.platformComponent;
        preferenceComponent = GlobalComponent.instance.preferenceComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

        orderPageComponent.orderIdButton.onClick.AddListener(() =>
        {
            GUIUtility.systemCopyBuffer = orderPageComponent.orderId.ToString();
        });
        orderPageComponent.calculateButton.onClick.AddListener(() =>
        {
            if (orderPageComponent.lockForEdit)
            {
                orderPageComponent.lockForEdit = false;
                orderPageComponent.deleteFromServer = true;
            }
            else
            {
                orderPageComponent.calculate = true;
                calculateButtonPressed = true;
            }
        });
        orderPageComponent.placeOrderButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.PLACE_ORDER_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageComponent.placeOrderButton.interactable = false;
                orderPageComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageComponent.cancelOrderButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.CANCEL_ORDER_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageComponent.cancelOrderButton.interactable = false;
                orderPageComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageComponent.closePositionButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.CLOSE_POSITION_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageComponent.closePositionButton.interactable = false;
                orderPageComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageComponent.cancelErrorOrderButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.CANCEL_ORDER_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageComponent.cancelErrorOrderButton.interactable = false;
                orderPageComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageComponent.closeErrorPositionButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.CLOSE_POSITION_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageComponent.closeErrorPositionButton.interactable = false;
                orderPageComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageComponent.riskRewardRatioInput.onSubmit.AddListener(value => UpdateTakeProfitPrice());
        orderPageComponent.riskRewardMinusButton.onClick.AddListener(() =>
        {
            double rrr = double.Parse(orderPageComponent.riskRewardRatioInput.text);
            rrr -= 0.01;
            orderPageComponent.riskRewardRatioInput.text = rrr.ToString();

            UpdateTakeProfitPrice();
        });
        orderPageComponent.riskRewardAddButton.onClick.AddListener(() =>
        {
            double rrr = double.Parse(orderPageComponent.riskRewardRatioInput.text);
            rrr += 0.01;
            orderPageComponent.riskRewardRatioInput.text = rrr.ToString();

            UpdateTakeProfitPrice();
        });

        orderPageComponent.orderIdText.text = "Order Id: " + orderPageComponent.orderId.ToString();
    }
    void Update()
    {
        UpdateUiInteractableStatus();
        UpdateOrderStatus();
        UpdatePositionInfo();
        UpdateSaveOrder();
        StartCoroutine(CalculateMargin());
    }

    IEnumerator CalculateMargin()
    {
        if (!orderPageComponent.calculate) yield break;
        orderPageComponent.calculate = false;

        #region Update button
        orderPageComponent.calculateButton.interactable = false;
        orderPageComponent.lockForEdit = true;
        #endregion

        if (calculateButtonPressed)
        {
            #region Prepare data
            // get input
            string walletUnit = platformComponent.marginAssets[orderPageComponent.symbolDropdownComponent.selectedSymbol.ToUpper()];
            double maxLossPercentage = orderPageComponent.maxLossPercentageInput.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageComponent.maxLossPercentageInput.text);
            double amountToLoss = orderPageComponent.maxLossAmountInput.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageComponent.maxLossAmountInput.text);
            long entryTimes = orderPageComponent.entryTimesInput.text.IsNullOrEmpty() ? 0 :
                long.Parse(orderPageComponent.entryTimesInput.text);
            List<double> entryPrices = new List<double>();
            orderPageComponent.inputEntryPricesComponent.entryPriceInputs.ForEach(input =>
            {
                if (input.text != "") entryPrices.Add(double.Parse(input.text));
            });
            double stopLossPrice = orderPageComponent.stopLossInput.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageComponent.stopLossInput.text);
            double takeProfitPrice = orderPageComponent.takeProfitInput.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageComponent.takeProfitInput.text);
            double riskRewardRatio = orderPageComponent.riskRewardRatioInput.text.IsNullOrEmpty() ? preferenceComponent.riskRewardRatio : double.Parse(orderPageComponent.riskRewardRatioInput.text);
            double takeProfitTrailingCallbackPercentage = orderPageComponent.takeProfitTrailingCallbackPercentageInput.text.IsNullOrEmpty() ? preferenceComponent.takeProfitTrailingCallbackPercentage : double.Parse(orderPageComponent.takeProfitTrailingCallbackPercentageInput.text);
            double weightDistributionValue = orderPageComponent.marginWeightDistributionValueSlider.value * orderPagesComponent.marginWeightDistributionRange;

            // validate input
            if (walletUnit.IsNullOrEmpty())
            {
                ShowPrompt("Wallet unit not available.");
                yield break;
            }
            if (maxLossPercentage.Equals(double.NaN) && amountToLoss.Equals(double.NaN))
            {
                ShowPrompt("Either one of the loss percentage or loss amount must have value.");
                yield break;
            }
            if (entryPrices.Count <= 0)
            {
                ShowPrompt("Entry price(s) cannot be empty.");
                yield break;
            }
            if (stopLossPrice.Equals(double.NaN))
            {
                ShowPrompt("Stop loss price cannot be empty.");
                yield break;
            }
            if (!((entryPrices[0] > stopLossPrice && entryPrices[^1] > stopLossPrice) || (entryPrices[0] < stopLossPrice && entryPrices[^1] < stopLossPrice)))
            {
                ShowPrompt("Entry price(s) and stop loss price not valid.");
                yield break;
            }
            if (!takeProfitPrice.Equals(double.NaN))
            {
                if (!((entryPrices[0] > stopLossPrice && entryPrices[^1] > stopLossPrice && entryPrices[0] < takeProfitPrice && entryPrices[^1] < takeProfitPrice)
                  || (entryPrices[0] < stopLossPrice && entryPrices[^1] < stopLossPrice && entryPrices[0] > takeProfitPrice && entryPrices[^1] > takeProfitPrice)))
                {
                    ShowPrompt("Entry price(s) and take profit price not valid.");
                    yield break;
                }
            }

            platformComponent.walletBalances = new Dictionary<string, double>();
            platformComponent.getBalance = true;
            yield return new WaitUntil(() => platformComponent.walletBalances.ContainsKey(walletUnit));
            double currentWalletBalance = platformComponent.walletBalances[walletUnit];

            yield return new WaitUntil(() => platformComponent.fees.ContainsKey(orderPageComponent.symbolDropdownComponent.selectedSymbol)
            && platformComponent.fees[orderPageComponent.symbolDropdownComponent.selectedSymbol].HasValue);
            double feeRate = platformComponent.fees[orderPageComponent.symbolDropdownComponent.selectedSymbol].Value;
            #endregion

            #region Create calculator instance
            orderPageComponent.marginCalculator = new CalculateMargin(
                    currentWalletBalance,
                    maxLossPercentage,
                    amountToLoss,
                    entryTimes,
                    entryPrices,
                    stopLossPrice,
                    riskRewardRatio,
                    takeProfitTrailingCallbackPercentage,
                    feeRate,
                    platformComponent.quantityPrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol],
                    platformComponent.pricePrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol],
                    orderPageComponent.marginDistributionModeDropdown.value == 1,
                    weightDistributionValue);
            #endregion
        }

        #region Prepare game object for display
        orderPageComponent.resultComponent.pricesDataObjects.ForEach(obj => Destroy(obj));
        orderPageComponent.resultComponent.pricesDataObjects.Clear();
        orderPageComponent.resultComponent.quantitiesDataObjects.ForEach(obj => Destroy(obj));
        orderPageComponent.resultComponent.quantitiesDataObjects.Clear();
        if (orderPageComponent.resultComponent.orderInfoDataObject == null)
        {
            orderPageComponent.resultComponent.orderInfoDataObject = Instantiate(orderPageComponent.dataRowPrefab, orderPageComponent.resultComponent.orderInfoParent);
        }
        if (orderPageComponent.resultComponent.totalWinLossAmountDataObject == null)
        {
            orderPageComponent.resultComponent.totalWinLossAmountDataObject = Instantiate(orderPageComponent.dataRowPrefab, orderPageComponent.resultComponent.totalWinLossAmountParent);
        }
        if (orderPageComponent.resultComponent.balanceDataObject == null)
        {
            orderPageComponent.resultComponent.balanceDataObject = Instantiate(orderPageComponent.dataRowPrefab, orderPageComponent.resultComponent.balanceParent);
        }
        #endregion

        #region Assign value into game object for display
        TMP_Text temp;
        #region Order title
        string direction = orderPageComponent.marginCalculator.isLong ? "LONG" : "SHORT";
        Color directionColor = orderPageComponent.marginCalculator.isLong ? green : Color.red;
        orderPageComponent.orderTitleText.text = orderPageComponent.symbolDropdownComponent.selectedSymbol + ": " + direction;
        orderPageComponent.orderTitleText.color = directionColor;
        #endregion
        #region Order info
        orderPageComponent.resultComponent.orderInfoDataObject.transform.GetChild(0).GetComponent<TMP_Text>().text = orderPageComponent.symbolDropdownComponent.selectedSymbol;
        temp = orderPageComponent.resultComponent.orderInfoDataObject.transform.GetChild(1).GetComponent<TMP_Text>();
        temp.text = direction;
        temp.color = directionColor;
        orderPageComponent.resultComponent.orderInfoDataObject.transform.GetChild(2).gameObject.SetActive(false);
        orderPageComponent.resultComponent.orderInfoDataObject.transform.GetChild(3).GetComponent<TMP_Text>().text = orderPageComponent.orderStatus.ToString();
        orderPageComponent.resultComponent.orderInfoDataObject.transform.GetChild(4).gameObject.SetActive(false);
        #endregion
        #region Prices & Quantities
        List<double> tpPrices = (OrderTakeProfitTypeEnum)orderPageComponent.takeProfitTypeDropdown.value == OrderTakeProfitTypeEnum.TAKE_ON_RETURN_TRAILING ? orderPageComponent.marginCalculator.takeProfitTrailingPrices : orderPageComponent.marginCalculator.takeProfitPrices;
        for (int i = 0; i < orderPageComponent.marginCalculator.entryPrices.Count; i++)
        {
            #region Prices
            GameObject entryPriceDataObject = Instantiate(orderPageComponent.dataRowPrefab, orderPageComponent.resultComponent.pricesParent);
            entryPriceDataObject.transform.GetChild(0).GetComponent<TMP_Text>().text = orderPageComponent.marginCalculator.entryPrices[i].ToString();
            entryPriceDataObject.transform.GetChild(1).GetComponent<TMP_Text>().text = orderPageComponent.marginCalculator.avgEntryPrices[i].ToString();
            entryPriceDataObject.transform.GetChild(2).gameObject.SetActive(false);
            temp = entryPriceDataObject.transform.GetChild(3).GetComponent<TMP_Text>();
            temp.text = tpPrices[i].ToString();
            temp.color = green;
            entryPriceDataObject.transform.GetChild(4).gameObject.SetActive(false);
            orderPageComponent.resultComponent.pricesDataObjects.Add(entryPriceDataObject);
            #endregion
            #region Quantities
            GameObject listedWinLossAmountDataObject = Instantiate(orderPageComponent.dataRowPrefab,
                orderPageComponent.resultComponent.quantitiesParent);
            listedWinLossAmountDataObject.transform.GetChild(0).GetComponent<TMP_Text>().text = orderPageComponent.marginCalculator.quantities[i].ToString();
            listedWinLossAmountDataObject.transform.GetChild(1).GetComponent<TMP_Text>().text = orderPageComponent.marginCalculator.cumQuantities[i].ToString();
            listedWinLossAmountDataObject.transform.GetChild(2).gameObject.SetActive(false);
            temp = listedWinLossAmountDataObject.transform.GetChild(3).GetComponent<TMP_Text>();
            temp.text = Utils.RoundTwoDecimal(orderPageComponent.marginCalculator.stopLossAmounts[i]).ToString();
            temp.color = Color.red;
            temp = listedWinLossAmountDataObject.transform.GetChild(4).GetComponent<TMP_Text>();
            temp.text = "Fee: " + Utils.RoundNDecimal(orderPageComponent.marginCalculator.fees[i], 6).ToString();
            temp.color = Color.red;
            orderPageComponent.resultComponent.quantitiesDataObjects.Add(listedWinLossAmountDataObject);
            #endregion
        }
        #endregion
        #region Total win loss amount
        temp = orderPageComponent.resultComponent.totalWinLossAmountDataObject.transform.GetChild(0).GetComponent<TMP_Text>();
        temp.text = orderPageComponent.marginCalculator.stopLossPrice.ToString();
        temp.color = Color.red;
        temp = orderPageComponent.resultComponent.totalWinLossAmountDataObject.transform.GetChild(1).GetComponent<TMP_Text>();
        temp.text = Utils.RoundTwoDecimal(orderPageComponent.marginCalculator.totalLossAmount).ToString();
        temp.color = Color.red;
        temp = orderPageComponent.resultComponent.totalWinLossAmountDataObject.transform.GetChild(2).GetComponent<TMP_Text>();
        temp.text = "Fee: " + Utils.RoundNDecimal(orderPageComponent.marginCalculator.totalFee, 6).ToString();
        temp.color = Color.red;
        temp = orderPageComponent.resultComponent.totalWinLossAmountDataObject.transform.GetChild(3).GetComponent<TMP_Text>();
        temp.text = Utils.RoundTwoDecimal(orderPageComponent.marginCalculator.totalWinAmount).ToString();
        temp.color = green;
        orderPageComponent.resultComponent.totalWinLossAmountDataObject.transform.GetChild(4).gameObject.SetActive(false);
        #endregion
        #region Balance
        orderPageComponent.resultComponent.balanceDataObject.transform.GetChild(0).GetComponent<TMP_Text>().text = Utils.TruncTwoDecimal(orderPageComponent.marginCalculator.balance).ToString();
        temp = orderPageComponent.resultComponent.balanceDataObject.transform.GetChild(1).GetComponent<TMP_Text>();
        temp.text = Utils.TruncTwoDecimal(orderPageComponent.marginCalculator.balanceAfterLoss).ToString();
        temp.color = Color.red;
        temp = orderPageComponent.resultComponent.balanceDataObject.transform.GetChild(2).GetComponent<TMP_Text>();
        temp.text = "-" + Utils.RoundTwoDecimal(Utils.RateToPercentage(orderPageComponent.marginCalculator.balanceDecrementRate)).ToString() + " %";
        temp.color = Color.red;
        temp = orderPageComponent.resultComponent.balanceDataObject.transform.GetChild(3).GetComponent<TMP_Text>();
        temp.text = Utils.TruncTwoDecimal(orderPageComponent.marginCalculator.balanceAfterFullWin).ToString();
        temp.color = green;
        temp = orderPageComponent.resultComponent.balanceDataObject.transform.GetChild(4).GetComponent<TMP_Text>();
        temp.text = "+" + Utils.RoundTwoDecimal(Utils.RateToPercentage(orderPageComponent.marginCalculator.balanceIncrementRate)).ToString() + " %";
        temp.color = green;
        #endregion
        #endregion

        #region Update button after finish calculated
        if (orderPageComponent.orderStatus == OrderStatusEnum.UNSUBMITTED)
        {
            orderPageComponent.calculateButton.interactable = true;
        }
        #endregion

        #region Save order when calculate button is pressed
        if (calculateButtonPressed)
        {
            calculateButtonPressed = false;
            orderPageComponent.saveToServer = true;
        }
        #endregion
    }
    void ShowPrompt(string message, bool goToEditMode = true)
    {
        promptComponent.ShowPrompt("ERROR", message, () =>
        {
            promptComponent.active = false;
            if (goToEditMode)
            {
                orderPageComponent.lockForEdit = false;
                orderPageComponent.calculateButton.interactable = true;
            }
        });
    }
    void UpdateUiInteractableStatus()
    {
        UpdateUiInteractableStatusBasedOnLockForEdit();
        UpdateUiInteractableStatusBasedOnOrderStatus();
    }
    void UpdateUiInteractableStatusBasedOnLockForEdit()
    {
        if (lockForEdit == orderPageComponent.lockForEdit) return;
        lockForEdit = orderPageComponent.lockForEdit;
        orderPageComponent.symbolDropdownComponent.dropdown.interactable = !lockForEdit.Value;
        orderPageComponent.maxLossPercentageInput.interactable = !lockForEdit.Value;
        orderPageComponent.inputEntryPricesComponent.entryPriceInputs.ForEach(input => input.interactable = !lockForEdit.Value);
        orderPageComponent.inputEntryPricesComponent.entryPriceCloseButtons.ForEach(button => button.interactable = !lockForEdit.Value);
        orderPageComponent.inputEntryPricesComponent.addButtons.interactable = !lockForEdit.Value;
        orderPageComponent.maxLossAmountInput.interactable = !lockForEdit.Value;
        orderPageComponent.entryTimesInput.interactable = !lockForEdit.Value;
        orderPageComponent.stopLossInput.interactable = !lockForEdit.Value;
        orderPageComponent.takeProfitInput.interactable = !lockForEdit.Value;
        orderPageComponent.marginDistributionModeDropdown.interactable = !lockForEdit.Value;
        orderPageComponent.marginWeightDistributionValueSlider.interactable = !lockForEdit.Value;
        orderPageComponent.marginWeightDistributionValueInput.interactable = !lockForEdit.Value;
        if (lockForEdit.Value) orderPageComponent.calculateButtonText.text = "Edit Order";
        else orderPageComponent.calculateButtonText.text = "Calculate";
        orderPageComponent.orderTypeDropdown.interactable = lockForEdit.Value;
        orderPageComponent.placeOrderButton.interactable = lockForEdit.Value;
        orderPageComponent.cancelOrderButton.interactable = lockForEdit.Value;
        orderPageComponent.closePositionButton.interactable = lockForEdit.Value;
        orderPageComponent.cancelErrorOrderButton.interactable = lockForEdit.Value;
        orderPageComponent.resultComponent.gameObject.SetActive(lockForEdit.Value);
        orderPageComponent.takeProfitTypeObject.SetActive(lockForEdit.Value);
        orderPageComponent.riskRewardRatioObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value > (int)OrderTakeProfitTypeEnum.NONE);
        orderPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value == (int)OrderTakeProfitTypeEnum.TAKE_ON_RETURN_TRAILING);
        orderPageComponent.orderTypeObject.SetActive(lockForEdit.Value);
        orderPageComponent.applyButtonObject.SetActive(lockForEdit.Value);
    }
    void UpdateUiInteractableStatusBasedOnOrderStatus()
    {
        if (orderStatus == orderPageComponent.orderStatus) return;
        orderStatus = orderPageComponent.orderStatus;
        bool isFilled = orderStatus.Equals(OrderStatusEnum.FILLED);
        orderPageComponent.positionInfoObject.SetActive(isFilled);
        orderPageComponent.throttleObject.SetActive(isFilled);
        if (orderStatus != OrderStatusEnum.FILLED) orderPageComponent.positionInfoPaidFundingAmount.text = "0";
    }
    void UpdateOrderStatus()
    {
        string saveOrderString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SAVE_ORDER.ToString());
        if (saveOrderString.IsNullOrEmpty()) return;
        General.WebsocketSaveOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketSaveOrderResponse>(saveOrderString, JsonSerializerConfig.settings);
        if (response.orderId.Equals(orderPageComponent.orderId))
        {
            websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SAVE_ORDER.ToString());
            orderPageComponent.orderStatus = response.status;
            orderPageComponent.orderStatusError = response.statusError;
            orderPageComponent.resultComponent.orderInfoDataObject.transform.GetChild(3).GetComponent<TMP_Text>().text = orderPageComponent.orderStatus.ToString();
            if (!response.errorJsonString.IsNullOrEmpty())
            {
                switch (platformComponent.tradingPlatform)
                {
                    case PlatformEnum.BINANCE:
                    case PlatformEnum.BINANCE_TESTNET:
                        Binance.WebrequestGeneralResponse binanceResponse = JsonConvert.DeserializeObject<Binance.WebrequestGeneralResponse>(response.errorJsonString, JsonSerializerConfig.settings);
                        if (binanceResponse.code.HasValue)
                        {
                            string message = binanceResponse.msg + " (Binance Error Code: " + binanceResponse.code.Value + ")";
                            ShowPrompt(message, false);
                        }
                        break;
                }
            }
        }
    }
    void UpdateSaveOrder()
    {
        if (orderPageComponent.saveToServer)
        {
            orderPageComponent.saveToServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveOrderRequest(
                orderPageComponent.orderId,
                platformComponent.tradingPlatform,
                orderPageComponent.marginCalculator,
                orderPageComponent.symbolDropdownComponent.selectedSymbol,
                (OrderTakeProfitTypeEnum)orderPageComponent.takeProfitTypeDropdown.value,
                (OrderTypeEnum)orderPageComponent.orderTypeDropdown.value
            ));
        }
        if (orderPageComponent.updateToServer)
        {
            orderPageComponent.updateToServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveOrderRequest(
                orderPageComponent.orderId,
                platformComponent.tradingPlatform,
                orderPageComponent.marginCalculator,
                (OrderTakeProfitTypeEnum)orderPageComponent.takeProfitTypeDropdown.value,
                (OrderTypeEnum)orderPageComponent.orderTypeDropdown.value
            ));
        }
        if (orderPageComponent.submitToServer)
        {
            orderPageComponent.submitToServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveOrderRequest(
               orderPageComponent.orderId,
               platformComponent.tradingPlatform,
               true
            ));
        }
        if (orderPageComponent.deleteFromServer)
        {
            orderPageComponent.deleteFromServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveOrderRequest(
                orderPageComponent.orderId,
                platformComponent.tradingPlatform
            ));
        }
    }
    void UpdatePositionInfo()
    {
        string retrievePositionInfoString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_POSITION_INFO.ToString());
        if (retrievePositionInfoString.IsNullOrEmpty()) return;
        General.WebsocketRetrievePositionInfoResponse response = JsonConvert.DeserializeObject<General.WebsocketRetrievePositionInfoResponse>(retrievePositionInfoString, JsonSerializerConfig.settings);
        if (response.orderId.Equals(orderPageComponent.orderId))
        {
            websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_POSITION_INFO.ToString());
            if (response.averagePriceFilled.HasValue)
            {
                orderPageComponent.positionInfoAvgEntryPriceFilledText.text = Utils.RoundNDecimal(response.averagePriceFilled.Value, platformComponent.pricePrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
            }
            if (response.quantityFilled.HasValue)
            {
                orderPageComponent.positionInfoQuantityFilledText.text = Utils.RoundNDecimal(response.quantityFilled.Value, platformComponent.quantityPrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
            }
            if (response.actualTakeProfitPrice.HasValue)
            {
                orderPageComponent.positionInfoActualTakeProfitPriceText.text = Utils.RoundNDecimal(response.actualTakeProfitPrice.Value, platformComponent.pricePrecisions[orderPageComponent.symbolDropdownComponent.selectedSymbol]).ToString();
            }
            if (response.paidFundingAmount.HasValue)
            {
                orderPageComponent.positionInfoPaidFundingAmount.text = response.paidFundingAmount.Value.ToString();
            }
        }
    }
    public void UpdateToServer()
    {
        orderPageComponent.updateToServer = true;
    }
    public void UpdateTakeProfitPrice()
    {
        #region Calculate and get latest take profit prices
        if (orderPageComponent.takeProfitTypeDropdown.value > (int)OrderTakeProfitTypeEnum.NONE)
        {
            orderPageComponent.marginCalculator.RecalculateTakeProfitPrices(
                double.Parse(orderPageComponent.riskRewardRatioInput.text),
                double.Parse(orderPageComponent.takeProfitTrailingCallbackPercentageInput.text));
        }
        List<double> tpPrices = orderPageComponent.marginCalculator.takeProfitPrices;
        if (orderPageComponent.takeProfitTypeDropdown.value == (int)OrderTakeProfitTypeEnum.TAKE_ON_RETURN_TRAILING)
        {
            tpPrices = orderPageComponent.marginCalculator.takeProfitTrailingPrices;
        }
        #endregion

        #region Update value in game object for display
        for (int i = 0; i < orderPageComponent.marginCalculator.entryPrices.Count; i++)
        {
            Transform entryPriceDataTransform = orderPageComponent.resultComponent.pricesParent.GetChild(i + 1);
            TMP_Text temp = entryPriceDataTransform.GetChild(3).GetComponent<TMP_Text>();
            temp.text = tpPrices[i].ToString();
        }
        orderPageComponent.resultComponent.totalWinLossAmountDataObject.transform.GetChild(3).GetComponent<TMP_Text>().text = Utils.RoundTwoDecimal(orderPageComponent.marginCalculator.totalWinAmount).ToString();
        orderPageComponent.resultComponent.balanceDataObject.transform.GetChild(3).GetComponent<TMP_Text>().text = Utils.TruncTwoDecimal(orderPageComponent.marginCalculator.balanceAfterFullWin).ToString();
        orderPageComponent.resultComponent.balanceDataObject.transform.GetChild(4).GetComponent<TMP_Text>().text = "+" + Utils.RoundTwoDecimal(Utils.RateToPercentage(orderPageComponent.marginCalculator.balanceIncrementRate)).ToString() + " %";
        #endregion

        orderPageComponent.updateToServer = true;
    }
}
