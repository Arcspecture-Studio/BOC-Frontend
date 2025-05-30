using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModifiedScrollRect : ScrollRect
{
    public bool isDragging = false;
    public bool IsFreelyScrolling
    {
        get { return !velocity.Equals(Vector2.zero); }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        isDragging = true;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        isDragging = false;
    }
}
