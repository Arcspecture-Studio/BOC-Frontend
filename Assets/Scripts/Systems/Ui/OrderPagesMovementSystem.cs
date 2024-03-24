using DG.Tweening;
using System;
using UnityEngine;

public class OrderPagesMovementSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    InputComponent inputComponent;
    RectTransform rectTransform;
    float rectWidth;
    float xVelocity;
    bool isSwiping
    {
        get { return inputComponent.hold.IsPressed() && orderPagesComponent.status == OrderPagesStatusEnum.DETACH; }
    }

    string movePagesFunctionName = "MovePages";

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        inputComponent = GlobalComponent.instance.inputComponent;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdatePagesGap();
        InputCurrentXPos();
        AutoAlign();
        MovePages();
    }

    void UpdatePagesGap()
    {
        if (rectWidth == rectTransform.rect.width) return;
        rectWidth = rectTransform.rect.width;
        orderPagesComponent.orderPagesGap = rectTransform.rect.width * orderPagesComponent.pageScaleTarget * 1.05f;
    }
    void InputCurrentXPos()
    {
        if (isSwiping)
        {
            xVelocity = inputComponent.drag.ReadValue<Vector2>().x;
        }
        else
        {
            if (Math.Round(xVelocity) == 0)
            {
                xVelocity = 0;
            }
            else
            {
                xVelocity = Mathf.Lerp(xVelocity, 0, orderPagesComponent.pageMoveDeAccel);
            }
        }
        orderPagesComponent.currentXPos -= xVelocity;
        // TODO: investiage how does currentXPos got negative value at startup
    }
    void AutoAlign()
    {
        //if(orderPagesComponent.status == OrderPagesStatusEnum.DETACH)
        {
            if (xVelocity != 0)
            {
                orderPagesComponent.currentPageIndex = (long)Math.Round(orderPagesComponent.currentXPos / orderPagesComponent.orderPagesGap);
            }
            if (orderPagesComponent.currentXPos < -orderPagesComponent.borderGap)
            //if (orderPagesComponent.currentPageIndex < 0)
            {
                orderPagesComponent.currentPageIndex = 0;
                xVelocity = 0;
            }
            else if (orderPagesComponent.currentXPos > orderPagesComponent.orderPagesGap * (orderPagesComponent.transform.childCount - 1) + orderPagesComponent.borderGap)
            //else if (orderPagesComponent.currentPageIndex >= orderPagesComponent.transform.childCount)
            {
                orderPagesComponent.currentPageIndex = orderPagesComponent.transform.childCount - 1;
                xVelocity = 0;
            }
        }

        if ((xVelocity == 0 || orderPagesComponent.status == OrderPagesStatusEnum.IMMERSIVE) && !isSwiping)
        {
            orderPagesComponent.currentXPos = orderPagesComponent.currentPageIndex * orderPagesComponent.orderPagesGap;
            // TODO: investiage how does currentXPos got negative value at startup
        }
    }
    void MovePages()
    {
        if (orderPagesComponent.childRectTransforms == null || orderPagesComponent.childRectTransforms.Count == 0) return;
        for (int i = 0; i < orderPagesComponent.childRectTransforms.Count; i++)
        {
            if (orderPagesComponent.childRectTransforms[i] == null) continue;
            if (isSwiping)
            {
                if (DOTween.IsTweening(movePagesFunctionName)) DOTween.Kill(movePagesFunctionName);
                orderPagesComponent.childRectTransforms[i].DOLocalMoveX(orderPagesComponent.childRectTransforms[i].localPosition.x + xVelocity, 0f);
            }
            else
            {
                float x = (i * orderPagesComponent.orderPagesGap) - orderPagesComponent.currentXPos;
                orderPagesComponent.childRectTransforms[i].DOLocalMoveX(x, orderPagesComponent.pageScrollDelayDuration).SetId(movePagesFunctionName);
            }
        }
    }
}