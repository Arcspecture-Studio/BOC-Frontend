using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScaleOnClick : MonoBehaviour
{
    public float scale = 1.2f;
    public float duration = 0.2f;

    RectTransform rectTransform;
    Button button;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();

        button.onClick.AddListener(PlayAnimation);
    }

    void PlayAnimation()
    {
        float halfDuration = duration / 2;
        rectTransform.DOScale(scale, halfDuration).OnComplete(() =>
        {
            rectTransform.DOScale(1, halfDuration);
        });
    }
}