using UnityEngine;

public class ScalableCanvas : MonoBehaviour
{
    public static ScalableCanvas instance;

    RectTransform rectTransform;
    public const float canvasHeight = 1000; // As defined in canvas scalar with height matching
    public float canvasWidth
    {
        get { return ConvertPixelToCanvasResolution(Screen.width); }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetTopPadding(float value)
    {
        rectTransform.offsetMax = new Vector2(0, -value);
    }
    public void SetBottomPadding(float value)
    {
        rectTransform.offsetMin = new Vector2(0, value);
    }
    public void SetLeftPadding(float value)
    {
        rectTransform.offsetMin = new Vector2(value, 0);
    }
    public void SetRightPadding(float value)
    {
        rectTransform.offsetMax = new Vector2(-value, 0);
    }
    public float ConvertPixelToCanvasResolution(float pixel)
    {
        return pixel / Screen.height * canvasHeight;
    }
    public float ConvertCanvasResolutionToPixel(float resolution)
    {
        return resolution / canvasHeight * Screen.height;
    }
}