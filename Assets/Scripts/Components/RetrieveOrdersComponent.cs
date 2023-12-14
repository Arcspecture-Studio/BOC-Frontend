using System;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveOrdersComponent : MonoBehaviour
{
    public Dictionary<PlatformEnum, Dictionary<Guid, General.WebsocketRetrieveOrdersData>> ordersFromServer = new();
    public bool destroyOrders = false;
    public bool instantiateOrders = false;
    public bool updateOrderStatus = false;
}