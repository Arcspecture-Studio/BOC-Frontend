using UnityEngine;

[ExecuteInEditMode]
public class SettingContentHeightSystem : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] RectTransform navigationBarRectTransform;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        float targetHeight = canvasRectTransform.sizeDelta.y - navigationBarRectTransform.sizeDelta.y;
        if (rectTransform.sizeDelta.y == targetHeight) return;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetHeight);
    }
}
