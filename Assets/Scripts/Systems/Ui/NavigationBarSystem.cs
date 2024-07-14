using System;
using DG.Tweening;
using UnityEngine;

public class NavigationBarSystem : MonoBehaviour
{
    NavigationBarComponent navigationBarComponent;
    float buttonWidth;
    string moveNavBarFunctionName = "MoveNavBar";

    void Start()
    {
        navigationBarComponent = GlobalComponent.instance.navigationBarComponent;

        DefineButtonListeners();
    }
    void Update()
    {
        UpdateContentWidth();
        UpdateContentXToSnap();
    }

    void DefineButtonListeners()
    {
        navigationBarComponent.rightButton.onClick.AddListener(() =>
        {
            MoveContent(-buttonWidth);
        });
        navigationBarComponent.leftButton.onClick.AddListener(() =>
        {
            MoveContent(buttonWidth);
        });
    }
    void UpdateContentXToSnap()
    {
        if (navigationBarComponent.scrollView.isDragging)
        {
            if (DOTween.IsTweening(moveNavBarFunctionName)) DOTween.Kill(moveNavBarFunctionName);
            return;
        }
        MoveContent();
    }
    void MoveContent(float offset = 0)
    {
        if (DOTween.IsTweening(moveNavBarFunctionName) || navigationBarComponent.scrollView.IsFreelyScrolling) return;
        float x = buttonWidth * Mathf.Round(navigationBarComponent.contentRect.localPosition.x / buttonWidth);
        x += offset;
        x = Mathf.Clamp(x, -buttonWidth * Math.Max(0, GetActiveChild() - 3), 0);
        if (navigationBarComponent.contentRect.localPosition.x == x) return;
        navigationBarComponent.contentRect.DOLocalMoveX(x, navigationBarComponent.pageScrollDelayDuration)
        .SetId(moveNavBarFunctionName);
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
