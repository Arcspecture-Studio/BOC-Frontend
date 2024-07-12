using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModifiedScrollRect : ScrollRect
{
    public bool isDragging = false;

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
