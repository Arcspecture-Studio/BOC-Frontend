using DG.Tweening;
using UnityEngine;

public class QuickTabComponent : MonoBehaviour
{
    [Header("Reference")]
    public RectTransform rectTransform;
    [Header("Config")]
    public float pageMoveDuration;
    public Ease pageMoveEase;

    [Header("Runtime")]
    public bool active = false;
}