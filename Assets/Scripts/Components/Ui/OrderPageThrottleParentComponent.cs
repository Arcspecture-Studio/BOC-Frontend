using System.Collections.Generic;
using UnityEngine;

public class OrderPageThrottleParentComponent : MonoBehaviour
{
    [Header("References")]
    public OrderPageComponent orderPageComponent;
    public GameObject throttleTabPrefab;

    [Header("Runtime")]
    public List<OrderPageThrottleComponent> orderPageThrottleComponents;
}
