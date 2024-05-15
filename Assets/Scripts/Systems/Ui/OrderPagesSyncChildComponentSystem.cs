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
        if (orderPagesComponent.childOrderPageComponents == null) orderPagesComponent.childOrderPageComponents = new();
        if (orderPagesComponent.transform.childCount == orderPagesComponent.childOrderPageComponents.Count) return;
        orderPagesComponent.childOrderPageComponents = new();
        for (int i = 0; i < orderPagesComponent.transform.childCount; i++)
        {
            orderPagesComponent.childOrderPageComponents.Add(orderPagesComponent.transform.GetChild(i).GetComponent<OrderPageComponent>());
        }
    }
}