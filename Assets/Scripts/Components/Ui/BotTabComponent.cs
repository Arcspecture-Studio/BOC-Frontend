using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BotTabComponent : MonoBehaviour
{
    [Header("Reference")]
    public RectTransform rectTransform;
    public TMP_InputField longOrderLimitInput;
    public TMP_InputField shortOrderLimitInput;
    public Toggle autoDestroyOrderToggle;
    public TMP_Dropdown botTypeDropdown;
    public GameObject premiumIndexSettingObj;
    public TMP_InputField premiumIndexSetting_longThresholdPercentage;
    public TMP_InputField premiumIndexSetting_shortThresholdPercentage;
    public TMP_InputField premiumIndexSetting_candleLength;
    public TMP_InputField premiumIndexSetting_candleConfirmation;
    public Button addBotButton;
    public GameObject botDataObjectList;
    public Transform botDataRowParent;
    public GameObject botDataRowPrefab;

    [Header("Config")]
    public float pageMoveDuration;
    public Ease pageMoveEase;
    public float activeYPosition;
    public float inactiveYPosition;
    public float activeToInactiveYMovement;
    public float inactiveToActiveYMovement;

    [Header("Runtime")]
    public bool active = false;
    public bool updatePreferenceUI
    {
        set { onChange_updatePreferenceUI.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_updatePreferenceUI = new();
    public bool updatingUIFromProfile;
    public BotTypeEnum botType
    {
        get { return (BotTypeEnum)botTypeDropdown.value; }
    }
    public bool addToServer
    {
        set { onChange_addToServer.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_addToServer = new();
    public string deleteFromServer
    {
        set { onChange_deleteFromServer.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<string> onChange_deleteFromServer = new();
    public Dictionary<string, GameObject> spawnedBotDataObjects = new();
}