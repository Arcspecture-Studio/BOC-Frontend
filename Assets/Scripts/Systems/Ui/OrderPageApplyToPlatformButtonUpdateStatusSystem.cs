using UnityEngine;

public class OrderPageApplyToPlatformButtonUpdateStatusSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] OrderPageThrottleComponent orderPageThrottleComponent;

    OrderStatusEnum status = OrderStatusEnum.UNSUBMITTED;
    bool statusError = false;

    void Start()
    {
        OrderPageComponent_UpdateButtonStatus(false);
        OrderPageThrottleComponent_UpdateButtonStatus(false);
    }
    void Update()
    {
        OrderPageComponent_UpdateButtonStatus(true);
        OrderPageThrottleComponent_UpdateButtonStatus(true);
    }
    void OrderPageComponent_UpdateButtonStatus(bool isUpdate)
    {
        if (orderPageComponent == null) return;
        if (isUpdate)
        {
            if (status == orderPageComponent.orderStatus && statusError == orderPageComponent.orderStatusError) return;
            status = orderPageComponent.orderStatus;
            statusError = orderPageComponent.orderStatusError;
        }
        orderPageComponent.placeOrderButton.interactable = true;
        orderPageComponent.cancelOrderButton.interactable = true;
        orderPageComponent.closePositionButton.interactable = true;
        orderPageComponent.cancelErrorOrderButton.interactable = true;
        orderPageComponent.closeErrorPositionButton.interactable = true;

        bool orderUnsumitted = status == OrderStatusEnum.UNSUBMITTED;
        orderPageComponent.calculateButton.interactable = orderUnsumitted;
        orderPageComponent.orderTypeDropdown.interactable = orderUnsumitted;
        switch (status)
        {
            case OrderStatusEnum.UNSUBMITTED:
                orderPageComponent.placeOrderButton.gameObject.SetActive(true);
                orderPageComponent.cancelOrderButton.gameObject.SetActive(false);
                orderPageComponent.closePositionButton.gameObject.SetActive(false);
                orderPageComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                orderPageComponent.closeErrorPositionButton.gameObject.SetActive(false);
                orderPageComponent.orderStatusError = false;
                break;
            case OrderStatusEnum.SUBMITTED:
                if (orderPageComponent.orderStatusError)
                {
                    orderPageComponent.placeOrderButton.gameObject.SetActive(false);
                    orderPageComponent.cancelOrderButton.gameObject.SetActive(false);
                    orderPageComponent.closePositionButton.gameObject.SetActive(false);
                    orderPageComponent.cancelErrorOrderButton.gameObject.SetActive(true);
                    orderPageComponent.closeErrorPositionButton.gameObject.SetActive(false);
                }
                else
                {
                    orderPageComponent.placeOrderButton.gameObject.SetActive(false);
                    orderPageComponent.cancelOrderButton.gameObject.SetActive(true);
                    orderPageComponent.closePositionButton.gameObject.SetActive(false);
                    orderPageComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                    orderPageComponent.closeErrorPositionButton.gameObject.SetActive(false);
                }
                break;
            case OrderStatusEnum.FILLED:
                if (orderPageComponent.orderStatusError)
                {
                    orderPageComponent.placeOrderButton.gameObject.SetActive(false);
                    orderPageComponent.cancelOrderButton.gameObject.SetActive(false);
                    orderPageComponent.closePositionButton.gameObject.SetActive(false);
                    orderPageComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                    orderPageComponent.closeErrorPositionButton.gameObject.SetActive(true);
                }
                else
                {
                    orderPageComponent.placeOrderButton.gameObject.SetActive(false);
                    orderPageComponent.cancelOrderButton.gameObject.SetActive(false);
                    orderPageComponent.closePositionButton.gameObject.SetActive(true);
                    orderPageComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                    orderPageComponent.closeErrorPositionButton.gameObject.SetActive(false);
                }
                break;
        }
    }
    void OrderPageThrottleComponent_UpdateButtonStatus(bool isUpdate)
    {
        if (orderPageThrottleComponent == null) return;
        if (isUpdate)
        {
            if (status == orderPageThrottleComponent.orderStatus && statusError == orderPageThrottleComponent.orderStatusError) return;
            status = orderPageThrottleComponent.orderStatus;
            statusError = orderPageThrottleComponent.orderStatusError;
        }
        orderPageThrottleComponent.placeOrderButton.interactable = true;
        orderPageThrottleComponent.cancelOrderButton.interactable = true;
        orderPageThrottleComponent.cancelBreakEvenOrderButton.interactable = true;
        orderPageThrottleComponent.cancelErrorOrderButton.interactable = true;

        bool orderUnsumitted = status == OrderStatusEnum.UNSUBMITTED;
        orderPageThrottleComponent.calculateButton.interactable = orderUnsumitted;
        orderPageThrottleComponent.orderTypeDropdown.interactable = orderUnsumitted;
        switch (status)
        {
            case OrderStatusEnum.UNSUBMITTED:
                orderPageThrottleComponent.placeOrderButton.gameObject.SetActive(true);
                orderPageThrottleComponent.cancelOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelBreakEvenOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.orderStatusError = false;
                break;
            case OrderStatusEnum.SUBMITTED:
                orderPageThrottleComponent.placeOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelOrderButton.gameObject.SetActive(true);
                orderPageThrottleComponent.cancelBreakEvenOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                break;
            case OrderStatusEnum.FILLED:
                if (orderPageThrottleComponent.orderStatusError)
                {
                    orderPageThrottleComponent.placeOrderButton.gameObject.SetActive(false);
                    orderPageThrottleComponent.cancelOrderButton.gameObject.SetActive(false);
                    orderPageThrottleComponent.cancelBreakEvenOrderButton.gameObject.SetActive(false);
                    orderPageThrottleComponent.cancelErrorOrderButton.gameObject.SetActive(true);
                }
                else
                {
                    orderPageThrottleComponent.placeOrderButton.gameObject.SetActive(false);
                    orderPageThrottleComponent.cancelOrderButton.gameObject.SetActive(false);
                    orderPageThrottleComponent.cancelBreakEvenOrderButton.gameObject.SetActive(true);
                    orderPageThrottleComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                }
                break;
        }
    }

}
