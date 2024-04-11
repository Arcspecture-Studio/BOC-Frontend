using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingPageComponent : MonoBehaviour
{
    #region Info
    [Header("Reference")]
    public RectTransform rectTransform;
    public RectTransform InfoBodyPanel;
    public TMP_Text activePlatformText;
    public TMP_Text totalOrdersText;
    public GameObject balanceLabelPrefab;
    #endregion

    #region Profile
    public TMP_Dropdown profileDropdown;
    public GameObject addProfileButtonObj;
    public Button addProfileButton;
    public GameObject addProfileNameInputObj;
    public TMP_InputField addProfileNameInput;
    public GameObject confirmAddProfileButtonObj;
    public Button confirmAddProfileButton;
    public Button cancelAddProfileButton;
    public GameObject renameProfileButtonObj;
    public Button renameProfileButton;
    public GameObject renameProfileNameInputObj;
    public TMP_InputField renameProfileNameInput;
    public GameObject confirmRenameProfileButtonObj;
    public Button confirmRenameProfileButton;
    public Button cancelRenameProfileButton;
    public Button removeProfileButton;
    public GameObject removeProfileButtonObj;
    public Button switchPlatformButton;
    public Button logoutButton;
    #endregion

    #region Preferences
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
    #endregion

    [Header("Config")]
    public float pageMoveDuration;
    public Ease pageMoveEase;
    public float activeXPosition;
    public float inactiveXPosition;
    public float activeToInactiveXMovement;
    public float inactiveToActiveXMovement;

    [Header("Runtime")]
    public bool active = false;
    public bool syncSetting = false;
    public bool updateInfo
    {
        set { onChange_updateInfo.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_updateInfo = new();
    public bool updateProfile
    {
        set { onChange_updateProfile.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_updateProfile = new();
    public bool _showAddNewProfileButton;
    public bool showAddNewProfileButton
    {
        get { return _showAddNewProfileButton; }
        set
        {
            _showAddNewProfileButton = value;
            onChange_showAddNewProfileButton.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_showAddNewProfileButton = new();
    public bool _showRenameProfileButton;
    public bool showRenameProfileButton
    {
        get { return _showRenameProfileButton; }
        set
        {
            _showRenameProfileButton = value;
            onChange_showRenameProfileButton.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_showRenameProfileButton = new();
    public bool updatePreferenceUI
    {
        set { onChange_updatePreferenceUI.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_updatePreferenceUI = new();
    public bool updatePreferenceToServer
    {
        set { onChange_updatePreferenceToServer.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_updatePreferenceToServer = new();
}
