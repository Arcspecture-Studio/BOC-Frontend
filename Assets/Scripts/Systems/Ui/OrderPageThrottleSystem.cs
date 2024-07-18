using UnityEngine;
using WebSocketSharp;
using MongoDB.Bson;

public class OrderPageThrottleSystem : MonoBehaviour
{
    OrderPageThrottleComponent orderPageThrottleComponent;
    OrderPageComponent orderPageComponent;
    PromptComponent promptComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;

    bool? lockForEdit = null;
    bool calculateButtonPressed = false;
    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
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

        orderPageThrottleComponent.onChange_addToServer.AddListener(AddToServer);
        orderPageThrottleComponent.onChange_updateToServer.AddListener(UpdateToServer);
        orderPageThrottleComponent.onChange_deleteFromServer.AddListener(DeleteFromServer);
        orderPageThrottleComponent.onChange_submitToServer.AddListener(SubmitToServer);
    }
    void Update()
    {
        UpdateUiInteractableStatus();
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
            float pnl = orderPageThrottleComponent.pnlInput.text.IsNullOrEmpty() ? float.NaN :
                float.Parse(orderPageThrottleComponent.pnlInput.text);
            float currentPrice = orderPageComponent.positionInfoAvgEntryPriceFilledText.text.IsNullOrEmpty() ? float.NaN :
                float.Parse(orderPageComponent.positionInfoAvgEntryPriceFilledText.text);
            float currentQuantity = orderPageComponent.positionInfoQuantityFilledText.text.IsNullOrEmpty() ? float.NaN :
                float.Parse(orderPageComponent.positionInfoQuantityFilledText.text);
            float throttlePrice = orderPageThrottleComponent.throttlePriceInput.text.IsNullOrEmpty() ? float.NaN :
                float.Parse(orderPageThrottleComponent.throttlePriceInput.text);
            float throttleQuantity = orderPageThrottleComponent.throttleQuantityInput.text.IsNullOrEmpty() ? 0 :
                float.Parse(orderPageThrottleComponent.throttleQuantityInput.text);
            float paidFundingAmount = orderPageComponent.positionInfoPaidFundingAmount.text.IsNullOrEmpty() ? 0 : float.Parse(orderPageComponent.positionInfoPaidFundingAmount.text);

            // validate input
            if (pnl.Equals(float.NaN))
            {
                ShowPrompt("PNL cannot be empty.");
                return;
            }
            if (currentPrice.Equals(float.NaN))
            {
                ShowPrompt("Current price cannot be empty.");
                return;
            }
            if (currentQuantity.Equals(float.NaN))
            {
                ShowPrompt("Current quantity cannot be empty.");
                return;
            }
            if (throttlePrice.Equals(float.NaN))
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
            orderPageThrottleComponent.addToServer = true;
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
        float throttleQuantity = orderPageThrottleComponent.throttleQuantityInput.text.IsNullOrEmpty() ? 0 :
            float.Parse(orderPageThrottleComponent.throttleQuantityInput.text);
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
    void AddToServer()
    {
        websocketComponent.generalRequests.Add(new General.WebsocketAddThrottleOrderRequest(
            loginComponent.token,
            orderPageThrottleComponent.orderId,
            orderPageComponent.orderId,
            orderPageThrottleComponent.throttleCalculator,
            (OrderTypeEnum)orderPageThrottleComponent.orderTypeDropdown.value,
            (TakeProfitTypeEnum)orderPageThrottleComponent.breakEvenTypeDropdown.value + 1
        ));
    }
    public void UpdateToServer() // Used by throttle tab prefab -> order type & break even type dropdown template item
    {
        websocketComponent.generalRequests.Add(new General.WebsocketUpdateThrottleOrderRequest(
            loginComponent.token,
            orderPageThrottleComponent.orderId,
            (OrderTypeEnum)orderPageThrottleComponent.orderTypeDropdown.value,
            (TakeProfitTypeEnum)orderPageThrottleComponent.breakEvenTypeDropdown.value + 1
        ));
    }
    void DeleteFromServer()
    {
        websocketComponent.generalRequests.Add(new General.WebsocketDeleteThrottleOrderRequest(
            loginComponent.token,
            orderPageThrottleComponent.orderId
        ));
    }
    void SubmitToServer()
    {
        websocketComponent.generalRequests.Add(new General.WebsocketSubmitThrottleOrderRequest(
            loginComponent.token,
            orderPageThrottleComponent.orderId
        ));
    }
}