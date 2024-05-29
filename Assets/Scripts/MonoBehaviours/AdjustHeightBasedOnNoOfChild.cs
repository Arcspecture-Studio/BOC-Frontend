using UnityEngine;

[ExecuteInEditMode]
public class AdjustHeightBasedOnNoOfChild : MonoBehaviour
{
    public Transform parent;
    public float childHeight;
    public int childOffset;
    int childCount;
    int actualChildCount
    {
        get
        {
            int count = 0;
            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).gameObject.activeSelf) count++;
            }
            return count;
        }
    }

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        childCount = -1;
    }
    void Update()
    {
        if (parent == null) return;
        if (childCount == actualChildCount) return;
        childCount = actualChildCount;
        float y = (childCount + childOffset) * childHeight;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
    }
}
