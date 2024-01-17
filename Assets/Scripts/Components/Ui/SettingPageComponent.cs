using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingPageComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Text activePlatformText;
    public TMP_Text totalOrdersText;
    public TMP_Text balanceUdstText;
    public TMP_Text balanceBusdText;
    public RectTransform rectTransform;
    public TMP_Dropdown platformsDropdown;
    public TMP_InputField symbolInput;
    public TMP_InputField lossPercentageInput;
    public TMP_InputField lossAmountInput;
    public TMP_Dropdown marginDistributionModeDropdown;
    public GameObject marginWeightDistributionValueObject;
    public Slider marginWeightDistributionValueSlider;
    public EventTrigger marginWeightDistributionValueSliderTrigger;
    public TMP_InputField marginWeightDistributionValueInput;
    public TMP_Dropdown takeProfitTypeDropdown;
    public GameObject riskRewardRatioObject;
    public TMP_InputField riskRewardRatioInput;
    public GameObject takeProfitTrailingCallbackPercentageObject;
    public Slider takeProfitTrailingCallbackPercentageSlider;
    public EventTrigger takeProfitTrailingCallbackPercentageSliderTrigger;
    public TMP_Text takeProfitTrailingCallbackPercentageMinText;
    public TMP_Text takeProfitTrailingCallbackPercentageMaxText;
    public TMP_InputField takeProfitTrailingCallbackPercentageInput;
    public TMP_Dropdown orderTypeDropdown;
    public TMP_Dropdown tradingBotDropdown; // PENDING: temporarly put here
    public Button logoutButton;

    [Header("Config")]
    public float pageMoveDuration;
    public Ease pageMoveEase;

    [Header("Runtime")]
    public bool active = false;
    public bool syncSetting = false;
}
