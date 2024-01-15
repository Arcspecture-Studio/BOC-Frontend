using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class OrderPagesComponent : MonoBehaviour
{
    [Header("Reference")]
    public GameObject orderPagePrefab;
    public Transform orderPagesDeletedParent;
    public RectTransform closeTabButtonRect;
    public RectTransform undoButtonRect;

    [Header("Config")]
    public OrderPagesStatusEnum status;
    public float pageScrollDelayDuration;
    [Range(0.01f, 1)] public float pageMoveDeAccel;
    public float pageScaleTarget;
    public float pageAnimDuration;
    public Ease pageScaleEase;
    public float pageDeletedDelaySeconds;
    public bool scaleOrders = false;
    public float borderGap;

    [Header("Runtime")]
    public float currentXPos;
    public long currentPageIndex;
    public float orderPagesGap;
    public List<RectTransform> childRectTransforms;
    public List<OrderPageComponent> childOrderPageComponents;
}
