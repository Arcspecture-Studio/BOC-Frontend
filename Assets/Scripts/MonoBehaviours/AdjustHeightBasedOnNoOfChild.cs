using UnityEngine;

[ExecuteInEditMode]
public class AdjustHeightBasedOnNoOfChild : MonoBehaviour
{
    public Transform parent;
    public float childHeight;
    public long childOffset;
    long childCount;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        childCount = -1;
    }
    void Update()
    {
        if (parent == null) return;
        if (childCount == parent.childCount) return;
        childCount = parent.childCount;
        float y = (childCount + childOffset) * childHeight;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
    }
}
