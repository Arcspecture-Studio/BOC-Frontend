using DG.Tweening;
using UnityEngine;

public class SettingPageSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    PlatformComponent platformComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;

    bool? active = null;
    Tween tween = null;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

        settingPageComponent.switchPlatformButton.onClick.AddListener(() =>
        {
            platformComponent.gameObject.SetActive(true);
            settingPageComponent.active = false;
        });
        settingPageComponent.logoutButton.onClick.AddListener(() =>
        {
            promptComponent.ShowSelection(PromptConstant.LOGOUT, PromptConstant.LOGOUT_CONFIRM, PromptConstant.YES_PROCEED, PromptConstant.NO, () =>
            {
                loginComponent.logoutNow = true;
                settingPageComponent.logoutButton.interactable = false;
                settingPageComponent.active = false;

                promptComponent.active = false;
            }, () =>
            {
                promptComponent.active = false;
            });
        });
    }
    void Update()
    {
        MovePage();
    }

    void MovePage()
    {
        if (active == settingPageComponent.active) return;
        if (tween != null && tween.IsActive()) return;

        active = settingPageComponent.active;
        float initialValue = active.Value ? settingPageComponent.inactiveXPosition : settingPageComponent.activeXPosition;
        float moveValue = active.Value ? settingPageComponent.inactiveToActiveXMovement : settingPageComponent.activeToInactiveXMovement;
        settingPageComponent.rectTransform.anchoredPosition = new Vector2(initialValue,
            settingPageComponent.rectTransform.anchoredPosition.y);
        tween = settingPageComponent.rectTransform.DOBlendableLocalMoveBy(new Vector3(moveValue, 0, 0), settingPageComponent.pageMoveDuration).SetEase(settingPageComponent.pageMoveEase);
    }
}
