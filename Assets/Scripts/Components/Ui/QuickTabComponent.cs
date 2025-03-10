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
    public GameObject quickOrderDataObjectList;
    public Transform quickOrderDataRowParent;
    public GameObject quickOrderDataRowPrefab;

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
    public bool isLong;
    public Dictionary<string, GameObject> spawnedQuickOrderDataObjects = new();
}