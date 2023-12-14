using System.Collections.Generic;
using UnityEngine;

public class OrderPageResultComponent : MonoBehaviour
{
    [Header("Reference")]
    public Transform orderInfoParent;
    public Transform pricesParent;
    public Transform quantitiesParent;
    public Transform totalWinLossAmountParent;
    public Transform balanceParent;

    [Header("Runtime")]
    public GameObject orderInfoDataObject;
    public List<GameObject> pricesDataObjects;
    public List<GameObject> quantitiesDataObjects;
    public GameObject totalWinLossAmountDataObject;
    public GameObject balanceDataObject;
}
