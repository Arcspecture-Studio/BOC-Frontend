using UnityEngine;

public class OrderPageApplyToPlatformButtonUpdateStatusSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] OrderPageThrottleComponent orderPageThrottleComponent;

    void Start()
    {
        OrderPageComponent_UpdateButtonStatus();
        OrderPageThrottleComponent_UpdateButtonStatus();

        if (orderPageComponent != null)
        {
            orderPageComponent.onChange_orderStatus.AddListener(OrderPageComponent_UpdateButtonStatus);
            orderPageComponent.onChange_orderStatusError.AddListener(OrderPageComponent_UpdateButtonStatus);
        }
        if (orderPageThrottleComponent != null)
        {
            orderPageThrottleComponent.onChange_orderStatus.AddListener(OrderPageThrottleComponent_UpdateButtonStatus);
            orderPageThrottleComponent.onChange_orderStatusError.AddListener(OrderPageThrottleComponent_UpdateButtonStatus);
        }
    }
    void OrderPageComponent_UpdateButtonStatus()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.placeOrderButton.interactable = true;
        orderPageComponent.cancelOrderButton.interactable = true;
        orderPageComponent.closePositionButton.interactable = true;
        orderPageComponent.cancelErrorOrderButton.interactable = true;
        orderPageComponent.closeErrorPositionButton.interactable = true;

        bool orderUnsumitted = orderPageComponent.orderStatus == OrderStatusEnum.UNSUBMITTED;
        orderPageComponent.calculateButton.interactable = orderUnsumitted;
        orderPageComponent.orderTypeDropdown.interactable = orderUnsumitted;
        switch (orderPageComponent.orderStatus)
        {
            case OrderStatusEnum.UNSUBMITTED:
                orderPageComponent.placeOrderButton.gameObject.SetActive(true);
                orderPageComponent.cancelOrderButton.gameObject.SetActive(false);
                orderPageComponent.closePositionButton.gameObject.SetActive(false);
                orderPageComponent.cancelErrorOrderButton.gameObject.SetActive(false);
                orderPageComponent.closeErrorPositionButton.gameObject.SetActive(false);
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
        EventManager.Invoke(EventManagerConfig.UPDATE_ORDER_STATUS);
    }
    void OrderPageThrottleComponent_UpdateButtonStatus()
    {
        if (orderPageThrottleComponent == null) return;
        orderPageThrottleComponent.placeOrderButton.interactable = true;
        orderPageThrottleComponent.cancelOrderButton.interactable = true;
        orderPageThrottleComponent.cancelBreakEvenOrderButton.interactable = true;

        bool orderUnsumitted = orderPageThrottleComponent.orderStatus == OrderStatusEnum.UNSUBMITTED;
        orderPageThrottleComponent.calculateButton.interactable = orderUnsumitted;
        orderPageThrottleComponent.orderTypeDropdown.interactable = orderUnsumitted;
        orderPageThrottleComponent.breakEvenTypeDropdown.interactable = orderUnsumitted;
        switch (orderPageThrottleComponent.orderStatus)
        {
            case OrderStatusEnum.UNSUBMITTED:
                orderPageThrottleComponent.placeOrderButton.gameObject.SetActive(true);
                orderPageThrottleComponent.cancelOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelBreakEvenOrderButton.gameObject.SetActive(false);
                break;
            case OrderStatusEnum.SUBMITTED:
                orderPageThrottleComponent.placeOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelOrderButton.gameObject.SetActive(true);
                orderPageThrottleComponent.cancelBreakEvenOrderButton.gameObject.SetActive(false);
                break;
            case OrderStatusEnum.FILLED:
                orderPageThrottleComponent.placeOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelOrderButton.gameObject.SetActive(false);
                orderPageThrottleComponent.cancelBreakEvenOrderButton.gameObject.SetActive(true);
                break;
        }
    }
}
