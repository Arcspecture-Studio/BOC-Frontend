using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;
using MongoDB.Bson;

public class OrderPageThrottleSystem : MonoBehaviour
{
    OrderPageThrottleComponent orderPageThrottleComponent;
    OrderPageComponent orderPageComponent;
    PromptComponent promptComponent;
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;

    bool? lockForEdit = null;
    bool calculateButtonPressed = false;
    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        orderPageThrottleComponent = GetComponent<OrderPageThrottleComponent>();
        orderPageComponent = orderPageThrottleComponent.transform.parent.GetComponent<OrderPageThrottleParentComponent>().orderPageComponent;

        if (orderPageThrottleComponent.orderId.IsNullOrEmpty())
            orderPageThrottleComponent.orderId = ObjectId.GenerateNewId().ToString();
        orderPageThrottleComponent.calculateButton.onClick.AddListener(() =>
        {
            if (orderPageThrottleComponent.lockForEdit)
            {
                orderPageThrottleComponent.lockForEdit = false;
                orderPageThrottleComponent.deleteFromServer = true;
            }
            else
            {
                orderPageThrottleComponent.calculate = true;
                calculateButtonPressed = true;
            }
        });
        orderPageThrottleComponent.closeTabButton.onClick.AddListener(() =>
        {
            if (orderPageThrottleComponent.orderStatus == OrderStatusEnum.UNSUBMITTED)
            {
                orderPageThrottleComponent.deleteFromServer = true;
                UpdateOrderToServer();
                Destroy(orderPageThrottleComponent.gameObject);
            }
            else
            {
                switch (orderPageThrottleComponent.orderStatus)
                {
                    case OrderStatusEnum.SUBMITTED:
                        orderPageThrottleComponent.cancelOrderButton.onClick.Invoke();
                        break;
                    case OrderStatusEnum.FILLED:
                        if (orderPageThrottleComponent.orderStatusError)
                            orderPageThrottleComponent.cancelErrorOrderButton.onClick.Invoke();
                        else
                            orderPageThrottleComponent.cancelBreakEvenOrderButton.onClick.Invoke();
                        break;
                }
                promptComponent.leftButton.onClick.AddListener(() =>
                {
                    orderPageThrottleComponent.deleteFromServer = true;
                    UpdateOrderToServer();
                    Destroy(orderPageThrottleComponent.gameObject);
                });
            }
        });
        orderPageThrottleComponent.placeOrderButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.PLACE_THROTTLE_ORDER_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageThrottleComponent.placeOrderButton.interactable = false;
                orderPageThrottleComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageThrottleComponent.cancelOrderButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.CANCEL_THROTTLE_ORDER_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageThrottleComponent.cancelOrderButton.interactable = false;
                orderPageThrottleComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageThrottleComponent.cancelBreakEvenOrderButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.CLOSE_THROTTLE_POSITION_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageThrottleComponent.cancelBreakEvenOrderButton.interactable = false;
                orderPageThrottleComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
        orderPageThrottleComponent.cancelErrorOrderButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.NOTICE, PromptConstant.CLOSE_THROTTLE_POSITION_PROMPT,
                PromptConstant.YES_PROCEED, PromptConstant.NO,
            () =>
            {
                orderPageThrottleComponent.cancelErrorOrderButton.interactable = false;
                orderPageThrottleComponent.submitToServer = true;
                promptComponent.active = false;
            },
            () =>
            {
                promptComponent.active = false;
            });
        });
    }
    void Update()
    {
        UpdateUiInteractableStatus();
        UpdateOrderStatus();
        UpdateOrderToServer();
        CalculateThrottle();
    }

    void CalculateThrottle()
    {
        if (!orderPageThrottleComponent.calculate) return;
        orderPageThrottleComponent.calculate = false;

        #region Update button
        orderPageThrottleComponent.lockForEdit = true;
        #endregion

        if (calculateButtonPressed)
        {
            #region Prepare data
            // get input
            double pnl = orderPageThrottleComponent.pnlInput.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageThrottleComponent.pnlInput.text);
            double currentPrice = orderPageComponent.positionInfoAvgEntryPriceFilledText.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageComponent.positionInfoAvgEntryPriceFilledText.text);
            double currentQuantity = orderPageComponent.positionInfoQuantityFilledText.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageComponent.positionInfoQuantityFilledText.text);
            double throttlePrice = orderPageThrottleComponent.throttlePriceInput.text.IsNullOrEmpty() ? double.NaN :
                double.Parse(orderPageThrottleComponent.throttlePriceInput.text);
            double throttleQuantity = orderPageThrottleComponent.throttleQuantityInput.text.IsNullOrEmpty() ? 0 :
                double.Parse(orderPageThrottleComponent.throttleQuantityInput.text);
            double paidFundingAmount = orderPageComponent.positionInfoPaidFundingAmount.text.IsNullOrEmpty() ? 0 : double.Parse(orderPageComponent.positionInfoPaidFundingAmount.text);

            // validate input
            if (pnl.Equals(double.NaN))
            {
                ShowPrompt("PNL cannot be empty.");
                return;
            }
            if (currentPrice.Equals(double.NaN))
            {
                ShowPrompt("Current price cannot be empty.");
                return;
            }
            if (currentQuantity.Equals(double.NaN))
            {
                ShowPrompt("Current quantity cannot be empty.");
                return;
            }
            if (throttlePrice.Equals(double.NaN))
            {
                if (throttleQuantity > 0)
                {
                    ShowPrompt("Throttle price cannot be empty when throttle quantity is more than 0.");
                    return;
                }
                else
                {
                    throttlePrice = 0;
                }
            }
            #endregion

            #region Create calculator instance
            orderPageThrottleComponent.throttleCalculator = new CalculateThrottle(
                currentPrice,
                currentQuantity,
                throttlePrice,
                throttleQuantity,
                pnl,
                paidFundingAmount,
                orderPageComponent.marginCalculator.isLong,
                orderPageComponent.marginCalculator.feeRate,
                orderPageComponent.marginCalculator.quantityPrecision,
                orderPageComponent.marginCalculator.pricePrecision);
            #endregion
        }

        #region Assign value into game object for display
        orderPageThrottleComponent.totalQuantityText.text = orderPageThrottleComponent.throttleCalculator.totalQuantity.ToString();
        orderPageThrottleComponent.avgEntryPriceText.text = orderPageThrottleComponent.throttleCalculator.avgPrice.ToString();
        orderPageThrottleComponent.breakEvenPriceText.text = orderPageThrottleComponent.throttleCalculator.breakEvenPrice.ToString();
        #endregion

        #region Save order when calculate button is pressed
        if (calculateButtonPressed)
        {
            calculateButtonPressed = false;
            orderPageThrottleComponent.saveToServer = true;
        }
        #endregion
    }
    void UpdateUiInteractableStatus()
    {
        if (lockForEdit == orderPageThrottleComponent.lockForEdit) return;
        lockForEdit = orderPageThrottleComponent.lockForEdit;
        orderPageThrottleComponent.pnlInput.interactable = !lockForEdit.Value;
        orderPageThrottleComponent.throttlePriceInput.interactable = !lockForEdit.Value;
        orderPageThrottleComponent.throttleQuantityInput.interactable = !lockForEdit.Value;
        if (lockForEdit.Value) orderPageThrottleComponent.calculateButtonText.text = "Edit Order";
        else orderPageThrottleComponent.calculateButtonText.text = "Calculate";
        double throttleQuantity = orderPageThrottleComponent.throttleQuantityInput.text.IsNullOrEmpty() ? 0 :
            double.Parse(orderPageThrottleComponent.throttleQuantityInput.text);
        orderPageThrottleComponent.resultObject.SetActive(lockForEdit.Value);
        orderPageThrottleComponent.orderTypeObject.SetActive(throttleQuantity > 0 && lockForEdit.Value);
        orderPageThrottleComponent.applyButtonObject.SetActive(lockForEdit.Value);
    }
    void ShowPrompt(string message, bool goToEditMode = true)
    {
        promptComponent.ShowPrompt(PromptConstant.ERROR, message, () =>
        {
            promptComponent.active = false;
            if (goToEditMode)
            {
                orderPageThrottleComponent.lockForEdit = false;
            }
        });
    }
    void UpdateOrderStatus()
    {
        // TODO: move to OrderPagesWebsocketResponseSystem
        string saveThrottleOrderString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER);
        if (saveThrottleOrderString.IsNullOrEmpty()) return;
        General.WebsocketSaveThrottleOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketSaveThrottleOrderResponse>(saveThrottleOrderString, JsonSerializerConfig.settings);
        if (response.orderId.Equals(orderPageThrottleComponent.orderId) && response.parentOrderId.Equals(orderPageComponent.orderId))
        {
            websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER);
            orderPageThrottleComponent.orderStatus = response.status;
            orderPageThrottleComponent.orderStatusError = response.statusError;
            if (!response.errorJsonString.IsNullOrEmpty())
            {
                switch (platformComponent.activePlatform)
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
    void UpdateOrderToServer()
    {
        if (orderPageThrottleComponent.saveToServer)
        {
            orderPageThrottleComponent.saveToServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveThrottleOrderRequest(
                orderPageThrottleComponent.orderId,
                orderPageComponent.orderId,
                platformComponent.activePlatform,
                orderPageThrottleComponent.throttleCalculator,
                (OrderTypeEnum)orderPageThrottleComponent.orderTypeDropdown.value
            ));
        }
        if (orderPageThrottleComponent.updateToServer)
        {
            orderPageThrottleComponent.updateToServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveThrottleOrderRequest(
                orderPageThrottleComponent.orderId,
                orderPageComponent.orderId,
                platformComponent.activePlatform,
                (OrderTypeEnum)orderPageThrottleComponent.orderTypeDropdown.value
            ));
        }
        if (orderPageThrottleComponent.submitToServer)
        {
            orderPageThrottleComponent.submitToServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveThrottleOrderRequest(
                orderPageThrottleComponent.orderId,
                orderPageComponent.orderId,
                platformComponent.activePlatform,
                true
            ));
        }
        if (orderPageThrottleComponent.deleteFromServer)
        {
            orderPageThrottleComponent.deleteFromServer = false;
            websocketComponent.generalRequests.Add(new General.WebsocketSaveThrottleOrderRequest(
                orderPageThrottleComponent.orderId,
                orderPageComponent.orderId,
                platformComponent.activePlatform
            ));
        }
    }
    public void UpdateToServer()
    {
        orderPageThrottleComponent.updateToServer = true;
    }
}