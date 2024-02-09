using TMPro;
using UnityEngine;
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
    public bool saveToServer;
    public bool updateToServer;
    public bool deleteFromServer;
    public bool submitToServer;
    public CalculateThrottle throttleCalculator;
    public bool lockForEdit;
    public string orderId;
    public OrderStatusEnum orderStatus = OrderStatusEnum.UNSUBMITTED;
    public bool orderStatusError = false;
}