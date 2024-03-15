using DG.Tweening;
using UnityEngine;

public class SettingPageSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    PlatformComponent platformComponent;
    LoginComponent loginComponent;

    bool? active = null;
    Tween tween = null;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        loginComponent = GlobalComponent.instance.loginComponent;

        settingPageComponent.switchPlatformButton.onClick.AddListener(() =>
        {
            platformComponent.gameObject.SetActive(true);
            settingPageComponent.active = false;
        });
        settingPageComponent.logoutButton.onClick.AddListener(() =>
        {
            loginComponent.logoutNow = true;
            settingPageComponent.logoutButton.interactable = false;
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
