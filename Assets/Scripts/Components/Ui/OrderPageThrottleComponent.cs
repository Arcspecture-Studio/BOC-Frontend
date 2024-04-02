using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderPageThrottleComponent : MonoBehaviour
{
    [Header("References")]
    public TMP_InputField pnlInput;
    public TMP_InputField throttlePriceInput;
    public TMP_InputField throttleQuantityInput;
    public Button calculateButton;
    public TMP_Text calculateButtonText;
    public TMP_Text totalQuantityText;
    public TMP_Text avgEntryPriceText;
    public TMP_Text breakEvenPriceText;
    public TMP_Dropdown orderTypeDropdown;
    public Button placeOrderButton;
    public Button cancelOrderButton;
    public Button cancelBreakEvenOrderButton;
    public Button cancelErrorOrderButton;
    public Button closeTabButton;
    public GameObject resultObject;
    public GameObject orderTypeObject;
    public GameObject applyButtonObject;

    [Header("Runtime")]
    public bool calculate;
    public bool addToServer
    {
        set { onChange_addToServer.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_addToServer = new();
    public bool updateToServer
    {
        set { onChange_updateToServer.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_updateToServer = new();
    public bool deleteFromServer
    {
        set { onChange_deleteFromServer.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_deleteFromServer = new();
    public bool submitToServer
    {
        set { onChange_submitToServer.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_submitToServer = new();
    public CalculateThrottle throttleCalculator;
    public bool lockForEdit;
    public string orderId;
    private OrderStatusEnum _orderStatus = OrderStatusEnum.UNSUBMITTED;
    public OrderStatusEnum orderStatus
    {
        set
        {
            _orderStatus = value;
            onChange_orderStatus.Invoke();
        }
        get
        {
            return _orderStatus;
        }
    }
    public UnityEvent onChange_orderStatus = new();
    public bool _orderStatusError;
    public bool orderStatusError
    {
        set
        {
            _orderStatusError = value;
            onChange_orderStatus.Invoke();
        }
        get
        {
            return _orderStatusError;
        }
    }
    public UnityEvent onChange_orderStatusError = new();
}