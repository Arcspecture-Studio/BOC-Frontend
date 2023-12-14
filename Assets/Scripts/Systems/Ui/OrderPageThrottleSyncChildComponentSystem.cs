using UnityEngine;

public class OrderPageThrottleSyncChildComponentSystem : MonoBehaviour
{
    OrderPageThrottleParentComponent orderPageThrottleParentComponent;

    void Start()
    {
        orderPageThrottleParentComponent = GetComponent<OrderPageThrottleParentComponent>();
        
        if(orderPageThrottleParentComponent.orderPageThrottleComponents == null)
        {
            orderPageThrottleParentComponent.orderPageThrottleComponents = new();
        }
    }
    void Update()
    {
        SyncNow();
    }
    void SyncNow()
    {
        if (orderPageThrottleParentComponent.transform.childCount == orderPageThrottleParentComponent.orderPageThrottleComponents.Count) return;
        orderPageThrottleParentComponent.orderPageThrottleComponents.Clear();
        for (int i = 0; i < orderPageThrottleParentComponent.transform.childCount; i++)
        {
            OrderPageThrottleComponent orderPageThrottleComponent = orderPageThrottleParentComponent.transform.GetChild(i).GetComponent<OrderPageThrottleComponent>();
            orderPageThrottleParentComponent.orderPageThrottleComponents.Add(orderPageThrottleComponent);
        }
    }
}