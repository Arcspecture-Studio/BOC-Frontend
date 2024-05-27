#pragma warning disable CS0168

using System;
using UnityEngine;
using WebSocketSharp;

public class Slider_ClosePositionPromptQuantitySystem : MonoBehaviour
{
    ClosePositionPromptComponent closePositionPromptComponent;

    void Start()
    {
        closePositionPromptComponent = GlobalComponent.instance.closePositionPromptComponent;

        closePositionPromptComponent.quantitySlider.onValueChanged.AddListener(value =>
        {
            try
            {
                float roundedValue = (float)Utils.RoundNDecimal((double)value, closePositionPromptComponent.orderPageComponent.marginCalculator.quantityPrecision);
                closePositionPromptComponent.quantitySlider.value = roundedValue;
                closePositionPromptComponent.quantityInput.text = roundedValue.ToString();
            }
            catch (Exception ex) { }
        });
        closePositionPromptComponent.quantityInput.onSubmit.AddListener(value =>
        {
            try
            {
                if (value.IsNullOrEmpty()) value = closePositionPromptComponent.quantitySlider.value.ToString();
                double parsedValue = Math.Min(Math.Max(double.Parse(value), closePositionPromptComponent.minQuantity), closePositionPromptComponent.maxQuantity);
                float roundedValue = (float)Utils.RoundNDecimal(parsedValue, closePositionPromptComponent.orderPageComponent.marginCalculator.quantityPrecision);
                closePositionPromptComponent.quantityInput.text = roundedValue.ToString();
                closePositionPromptComponent.quantitySlider.value = roundedValue;
            }
            catch (Exception ex) { }
        });
    }
}