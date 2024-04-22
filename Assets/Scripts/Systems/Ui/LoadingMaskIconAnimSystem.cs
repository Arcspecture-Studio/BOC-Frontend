using DG.Tweening;
using UnityEngine;

public class LoadingMaskIconAnimSystem : MonoBehaviour
{
    LoadingComponent loadingComponent;

    bool rotateClockwise;
    void Start()
    {
        loadingComponent = GlobalComponent.instance.loadingComponent;

        RotateIcon();
    }

    void RotateIcon()
    {
        rotateClockwise = !rotateClockwise;
        loadingComponent.iconRectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0).OnComplete(() =>
        loadingComponent.iconRectTransform.DOLocalRotate(new Vector3(0, 0, rotateClockwise ? -360 : 360), 1, RotateMode.FastBeyond360)
        .SetEase(loadingComponent.iconRotateAnimEase).OnComplete(RotateIcon));
    }
}
