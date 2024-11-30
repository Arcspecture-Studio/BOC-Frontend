using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderPageComponent : MonoBehaviour
{
    [Header("Reference")]
    public RectTransform rectTransform;
    public ScrollRect scrollRect;
    public TMP_Text orderTitleText;
    public Button orderIdButton;
    public TMP_Text orderIdText;
    public OrderPageSymbolDropdownComponent symbolDropdownComponent;
    public TMP_InputField maxLossPercentageInput;
    public TMP_InputField maxLossAmountInput;
    public OrderPageInputEntryPricesComponent inputEntryPricesComponent;
    public TMP_InputField entryTimesInput;
    public TMP_InputField stopLossInput;
    public TMP_InputField takeProfitInput;
    public OrderPageResultComponent resultComponent;
    public GameObject dataRowPrefab;
    public TMP_Dropdown marginDistributionModeDropdown;
    public CustomSlider marginWeightDistributionValueCustomSlider;
    public Button calculateButton;
    public TMP_Text calculateButtonText;
    public GameObject takeProfitTypeObject;
    public TMP_Dropdown takeProfitTypeDropdown;
    public GameObject riskRewardRatioObject;
    public TMP_InputField riskRewardRatioInput;
    public Button riskRewardMinusButton;
    public Button riskRewardAddButton;
    public CustomSlider takeProfitQuantityPercentageCustomSlider;
    public CustomSlider takeProfitTrailingCallbackPercentageCustomSlider;
    public GameObject orderTypeObject;
    public TMP_Dropdown orderTypeDropdown;
    public GameObject fundingFeeHandlerObject;
    public TMP_Dropdown fundingFeeHandlerDropdown;
    public GameObject applyButtonObject;
    public Button placeOrderButton;
    public Button cancelOrderButton;
    public Button closePositionButton;
    public Button cancelErrorOrderButton;
    public Button closeErrorPositionButton;
    public GameObject positionInfoObject;
    public TMP_Text positionInfoAvgEntryPriceFilledText;
    public TMP_Text positionInfoActualTakeProfitPriceText;
    public TMP_Text positionInfoActualStopLossPriceText;
    public TMP_Text positionInfoActualBreakEvenPriceText;
    public TMP_Text positionInfoQuantityFilledText;
    public TMP_Text positionInfoPaidFundingAmount;
    public GameObject positionInfoBotInChargeObject;
    public TMP_Dropdown positionInfoBotInChargeDropdown;
    public OrderPageThrottleParentComponent throttleParentComponent;
    public GameObject throttleObject;

    [Header("Runtime")]
    public bool destroySelf;
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
    public MarginCalculator marginCalculator; // Server side data
    private MarginCalculatorAdd _marginCalculatorRequest; // Used to send to server
    public MarginCalculatorAdd marginCalculatorRequest
    {
        get
        {
            if (_marginCalculatorRequest == null && marginCalculator != null)
            {
                _marginCalculatorRequest = marginCalculator.GetMarginCalculatorAdd();
            }
            return _marginCalculatorRequest;
        }
        set
        {
            _marginCalculatorRequest = value;
        }
    }
    public bool lockForEdit;
    public string orderId;
    [SerializeField] private OrderStatusEnum _orderStatus = OrderStatusEnum.UNSUBMITTED;
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
    [HideInInspector] public UnityEvent onChange_orderStatus = new();
    [SerializeField] private bool _orderStatusError;
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
    [HideInInspector] public UnityEvent onChange_orderStatusError = new();
    public string tradingBotId = "";
    public Tween spawnTween;
    public bool instantiateWithData;
    public float scrollRectYPos;
    public bool updateTakeProfitPrice
    {
        set
        {
            onChange_updateTakeProfitPrice.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_updateTakeProfitPrice = new();
    public float quantityToClose;
    [SerializeField] private long _spawnTime; // TIMESTAMP
    public long spawnTime
    {
        set
        {
            _spawnTime = value;
            resultComponent.spawnTimeText.text = DateTimeOffset.FromUnixTimeMilliseconds(_spawnTime).ToLocalTime().ToString();
        }
        get
        {
            return _spawnTime;
        }
    }
    [SerializeField] private ExitOrderTypeEnum _exitOrderType;
    public ExitOrderTypeEnum exitOrderType
    {
        set
        {
            _exitOrderType = value;
            resultComponent.exitOrderTypeText.text = _exitOrderType.ToString();
            switch (_exitOrderType)
            {
                case ExitOrderTypeEnum.NONE:
                    resultComponent.exitOrderTypeText.color = OrderConfig.DISPLAY_COLOR_BLACK;
                    break;
                case ExitOrderTypeEnum.STOP_LOSS:
                    resultComponent.exitOrderTypeText.color = OrderConfig.DISPLAY_COLOR_RED;
                    break;
                case ExitOrderTypeEnum.TAKE_PROFIT:
                    resultComponent.exitOrderTypeText.color = OrderConfig.DISPLAY_COLOR_GREEN;
                    break;
                case ExitOrderTypeEnum.THROTTLE_STOP:
                    resultComponent.exitOrderTypeText.color = OrderConfig.DISPLAY_COLOR_ORANGE;
                    break;
            }
        }
        get
        {
            return _exitOrderType;
        }
    }
    public bool postCalculate
    {
        set
        {
            if (value) onChange_postCalculate.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_postCalculate = new();

}