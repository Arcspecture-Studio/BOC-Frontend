using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AdjustHeightBasedOnChildsHeight : MonoBehaviour
{
    public Transform parent;
    public float padding;
    public float spacing;

    RectTransform rectTransform;
    List<RectTransform> childrens;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ReinitializeChildrens();
    }
    void Update()
    {
        if (parent == null) return;
        if (childrens == null || childrens.Count != parent.childCount)
        {
            ReinitializeChildrens();
        }
        float y = padding * 2;
        int activeChildCount = 0;
        foreach (RectTransform child in childrens)
        {
            if (child.gameObject.activeSelf)
            {
                activeChildCount++;
                y += child.sizeDelta.y;
            }
        }
        y += spacing * (activeChildCount - 1);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
    }

    void ReinitializeChildrens()
    {
        if (parent == null) return;
        childrens = new List<RectTransform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            childrens.Add(parent.GetChild(i).GetComponent<RectTransform>());
        }
    }
}