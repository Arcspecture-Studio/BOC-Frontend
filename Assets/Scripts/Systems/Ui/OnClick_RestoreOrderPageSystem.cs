using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OnClick_RestoreOrderPageSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;

    Button button;

    private void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;

        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (orderPagesComponent.orderPagesDeletedParent.childCount > 0)
            {
                RectTransform deletedOrder = orderPagesComponent.orderPagesDeletedParent.GetChild(orderPagesComponent.orderPagesDeletedParent.childCount - 1).GetComponent<RectTransform>();
                deletedOrder.SetParent(orderPagesComponent.transform);
                deletedOrder.gameObject.SetActive(true);
                deletedOrder.DOScale(0, 0).OnComplete(() =>
                {
                    deletedOrder.DOScale(orderPagesComponent.pageScaleTarget, orderPagesComponent.pageAnimDuration).SetEase(orderPagesComponent.pageScaleEase);
                });
                orderPagesComponent.currentPageIndex = orderPagesComponent.transform.childCount;
                OrderPageComponent deletedOrderComponent = deletedOrder.GetComponent<OrderPageComponent>();
                deletedOrderComponent.orderStatus = OrderStatusEnum.UNSUBMITTED;
                deletedOrderComponent.addToServer = true;
            }
        });
    }
}