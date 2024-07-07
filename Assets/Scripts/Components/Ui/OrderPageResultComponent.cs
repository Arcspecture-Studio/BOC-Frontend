using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPageResultComponent : MonoBehaviour
{
    [Header("Reference")]
    public Transform orderInfoParent;
    public Transform pricesParent;
    public Transform quantitiesParent;
    public Transform totalWinLossAmountParent;
    public Transform balanceParent;
    public TMP_Text spawnTimeText;
    public TMP_Text exitOrderTypeText;

    [Header("Runtime")]
    public GameObject orderInfoDataObject;
    public List<GameObject> pricesDataObjects;
    public List<GameObject> quantitiesDataObjects;
    public GameObject totalWinLossAmountDataObject;
    public GameObject balanceDataObject;
}
