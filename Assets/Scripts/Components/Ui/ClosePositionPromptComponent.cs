using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClosePositionPromptComponent : MonoBehaviour
{
    public TMP_Text minQuantityText;
    public TMP_Text maxQuantityText;
    public Slider quantitySlider;
    public TMP_InputField quantityInput;
    public Button closePositionButton;
    public Button cancelButton;

    public float minQuantity;
    public float maxQuantity;
    private bool _active;
    public bool active
    {
        get { return _active; }
        set
        {
            _active = value;
            onChange_active.Invoke(value);
        }
    }
    [HideInInspector] public UnityEvent<bool> onChange_active = new();
    public OrderPageComponent orderPageComponent;
    public OrderPageComponent show
    {
        set
        {
            orderPageComponent = value;
            onChange_show.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_show = new();
}