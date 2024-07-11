using System;
using Codice.Client.BaseCommands.Download;
using DG.Tweening;
using UnityEngine;

public class NavigationBarSystem : MonoBehaviour
{
    NavigationBarComponent navigationBarComponent;
    InputComponent inputComponent;
    float buttonWidth;
    string moveNavBarFunctionName = "MoveNavBar";

    void Start()
    {
        navigationBarComponent = GlobalComponent.instance.navigationBarComponent;
        inputComponent = GlobalComponent.instance.inputComponent;
    }
    void Update()
    {
        UpdateContentWidth();
        UpdateContentXToSnap();
    }

    void UpdateContentXToSnap()
    {
        if (inputComponent.hold.IsPressed())
        {
            if (DOTween.IsTweening(moveNavBarFunctionName)) DOTween.Kill(moveNavBarFunctionName);
            return;
        }
        float x = buttonWidth * Mathf.Round(navigationBarComponent.contentRect.localPosition.x / buttonWidth);
        x = Mathf.Clamp(x, -buttonWidth * Math.Max(0, GetActiveChild() - 3), 0);
        if (navigationBarComponent.contentRect.localPosition.x == x) return;
        navigationBarComponent.contentRect.DOLocalMoveX(x, navigationBarComponent.pageScrollDelayDuration).SetId(moveNavBarFunctionName);
    }
    void UpdateContentWidth()
    {
        buttonWidth = navigationBarComponent.scrollViewRect.rect.width / 3;
        navigationBarComponent.contentRect.sizeDelta = new Vector2(buttonWidth * GetActiveChild(), navigationBarComponent.contentRect.sizeDelta.y);
    }
    int GetActiveChild()
    {
        int childCount = 0;
        for (int i = 0; i < navigationBarComponent.contentRect.childCount; i++)
        {
            if (navigationBarComponent.contentRect.GetChild(i).gameObject.activeSelf) childCount++;
        }
        return childCount;
    }

}
