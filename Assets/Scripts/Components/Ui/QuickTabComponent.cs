using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuickTabComponent : MonoBehaviour
{
    [Header("Reference")]
    public RectTransform rectTransform;
    public TMP_InputField entryTimesInput;
    public TMP_InputField entryPriceInput;
    public Button clearEntryPriceButton;
    public TMP_Dropdown atrTimeframeDropdown;
    public TMP_InputField atrLengthInput;
    public TMP_InputField atrMultiplierInput;
    public Button longButton;
    public Button shortButton;
    public GameObject quickOrdersObject;
    public Transform orderInfoTransform;
    public GameObject quickOrderDataRowPrefab;

    [Header("Config")]
    public float pageMoveDuration;
    public Ease pageMoveEase;

    [Header("Runtime")]
    public bool active = false;
    public bool syncDataFromPreference = false;
    public bool saveToServer = false;
    public bool isLong;
    public Dictionary<string, General.WebsocketRetrieveQuickOrdersData> _quickOrdersFromServer;
    public Dictionary<string, General.WebsocketRetrieveQuickOrdersData> quickOrdersFromServer
    {
        get { return _quickOrdersFromServer; }
        set
        {
            _quickOrdersFromServer = value;
            onChange_quickOrdersFromServer.Invoke(value);
        }
    }
    [HideInInspector] public UnityEvent<Dictionary<string, General.WebsocketRetrieveQuickOrdersData>> onChange_quickOrdersFromServer = new();
    public Dictionary<string, GameObject> spawnedQuickOrderObjects = new();
}