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

    public OrderPageComponent orderPageComponent;
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

    void Start()
    {
        closePositionButton.onClick.AddListener(ClosePosition);
        cancelButton.onClick.AddListener(() => active = false);
    }
    void ClosePosition()
    {
        active = false;
        if (quantitySlider.value == maxQuantity)
        {
            orderPageComponent.closePositionButton.interactable = false;
            orderPageComponent.submitToServer = true;
        }
        else
        {
            // TODO: close partial quantity
        }
    }
    public void Show(OrderPageComponent orderPageComponent)
    {
        active = true;
        this.orderPageComponent = orderPageComponent;
        minQuantity = (float)Utils.GetDecimalPlaceNumber(orderPageComponent.marginCalculator.quantityPrecision);
        maxQuantity = float.Parse(orderPageComponent.positionInfoQuantityFilledText.text);

        minQuantityText.text = minQuantity.ToString();
        maxQuantityText.text = orderPageComponent.positionInfoQuantityFilledText.text;
        quantitySlider.minValue = minQuantity;
        quantitySlider.maxValue = maxQuantity;

        quantitySlider.value = maxQuantity;
        quantityInput.text = orderPageComponent.positionInfoQuantityFilledText.text;
    }
}