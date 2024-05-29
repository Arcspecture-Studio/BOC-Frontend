using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPageSymbolDropdownShowItemSystem : MonoBehaviour
{
    [SerializeField] OrderPageSymbolDropdownComponent orderPageSymbolDropdownComponent;
    RectTransform rectTransform;
    List<TMP_Text> texts;
    int count;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        texts = new List<TMP_Text>();
        for (int i = 0; i < transform.childCount; i++)
        {
            texts.Add(transform.GetChild(i).GetChild(2).GetComponent<TMP_Text>());
        }
    }
    void Update()
    {
        if (count == orderPageSymbolDropdownComponent.symbols.Count) return;
        count = orderPageSymbolDropdownComponent.symbols.Count;
        int activeChildCounts = 0;
        for (int i = 1; i < texts.Count; i++)
        {
            bool active = orderPageSymbolDropdownComponent.symbols.Contains(texts[i].text);
            texts[i].transform.parent.gameObject.SetActive(active);
            if (active)
            {
                activeChildCounts++;
            }
        }
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, activeChildCounts * 40);
    }
}
