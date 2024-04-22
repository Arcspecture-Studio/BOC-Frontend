using DG.Tweening;
using UnityEngine;

public class LoadingMaskIconAnimSystem : MonoBehaviour
{
    LoadingComponent loadingComponent;

    bool rotateClockwise;
    Tween rotateTween;
    void Start()
    {
        loadingComponent = GlobalComponent.instance.loadingComponent;

        RotateIcon();
    }
    void OnEnable()
    {
        if (loadingComponent == null) return;
        RotateIcon();
    }
    void OnDisable()
    {
        if (rotateTween.IsActive())
        {
            rotateTween.Kill();
        }
    }

    void RotateIcon()
    {
        rotateClockwise = !rotateClockwise;
        loadingComponent.iconRectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0).OnComplete(() =>
        rotateTween = loadingComponent.iconRectTransform.DOLocalRotate(new Vector3(0, 0, rotateClockwise ? -360 : 360), 1, RotateMode.FastBeyond360)
        .SetEase(loadingComponent.iconRotateAnimEase).OnComplete(RotateIcon));
    }
}
