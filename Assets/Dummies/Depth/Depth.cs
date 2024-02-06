using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Depth : MonoBehaviour
{
    public GameObject barPrefab;
    public int barCount;

    List<RectTransform> barRectTransform;
    List<Image> barImages;

    void Start()
    {
        barRectTransform = new();
        barImages = new();
        for (int i = 0; i < barCount; i++)
        {
            GameObject barObject = Instantiate(barPrefab);
            barObject.transform.SetParent(transform);
            RectTransform rectTransform = barObject.GetComponent<RectTransform>();
            Image image = barObject.GetComponent<Image>();
            barRectTransform.Add(rectTransform);
            barImages.Add(image);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 1280f / barCount);
        }
    }
}
