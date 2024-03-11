using DG.Tweening;
using UnityEngine;

public class SettingPageSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    LoginComponentOld loginComponent;

    bool? active = null;
    Tween tween = null;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        loginComponent = GlobalComponent.instance.loginComponent;

        settingPageComponent.logoutButton.onClick.AddListener(() =>
        {
            loginComponent.changePlatform = true;
            settingPageComponent.active = false;
        });
    }
    void Update()
    {
        MovePage();
    }

    void MovePage()
    {
        if (active == settingPageComponent.active) return;
        if (tween != null)
        {
            if (tween.IsPlaying()) return;
        }
        active = settingPageComponent.active;
        float initialValue = active.Value ? settingPageComponent.inactiveXPosition : settingPageComponent.activeXPosition;
        float moveValue = active.Value ? settingPageComponent.inactiveToActiveXMovement : settingPageComponent.activeToInactiveXMovement;
        settingPageComponent.rectTransform.anchoredPosition = new Vector2(initialValue,
            settingPageComponent.rectTransform.anchoredPosition.y);
        tween = settingPageComponent.rectTransform.DOBlendableLocalMoveBy(new Vector3(moveValue, 0, 0), settingPageComponent.pageMoveDuration).SetEase(settingPageComponent.pageMoveEase);
    }
}
