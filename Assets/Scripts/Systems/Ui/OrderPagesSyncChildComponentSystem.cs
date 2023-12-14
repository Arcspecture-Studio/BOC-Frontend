using System.Collections.Generic;
using UnityEngine;

public class OrderPagesSyncChildComponentSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
    }
    void Update()
    {
        if (orderPagesComponent.childRectTransforms == null) orderPagesComponent.childRectTransforms = new List<RectTransform>();
        if (orderPagesComponent.transform.childCount == orderPagesComponent.childRectTransforms.Count) return;
        orderPagesComponent.childRectTransforms.Clear();
        orderPagesComponent.childOrderPageComponents.Clear();
        for (int i = 0; i < orderPagesComponent.transform.childCount; i++)
        {
            orderPagesComponent.childRectTransforms.Add(orderPagesComponent.transform.GetChild(i).GetComponent<RectTransform>());
            orderPagesComponent.childOrderPageComponents.Add(orderPagesComponent.transform.GetChild(i).GetComponent<OrderPageComponent>());
        }
    }
}