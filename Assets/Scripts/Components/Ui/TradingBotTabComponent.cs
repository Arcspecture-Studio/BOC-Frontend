using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TradingBotTabComponent : MonoBehaviour
{
    [Header("Reference")]
    public RectTransform rectTransform;
    public GameObject tradingBotListObject;

    [Header("Config")]
    public float pageMoveDuration;
    public Ease pageMoveEase;
    public float activeYPosition;
    public float inactiveYPosition;
    public float activeToInactiveYMovement;
    public float inactiveToActiveYMovement;

    [Header("Runtime")]
    public bool active = false;
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
    public Dictionary<string, GameObject> spawnedTradingBotObjects = new();
}