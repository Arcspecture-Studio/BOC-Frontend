using UnityEngine;

public class ClosePositionPromptSystem : MonoBehaviour
{
    ClosePositionPromptComponent closePositionPromptComponent;

    void Start()
    {
        closePositionPromptComponent = GlobalComponent.instance.closePositionPromptComponent;

        closePositionPromptComponent.closePositionButton.onClick.AddListener(ClosePosition);
        closePositionPromptComponent.cancelButton.onClick.AddListener(() => closePositionPromptComponent.active = false);
        closePositionPromptComponent.onChange_show.AddListener(Show);
    }
    void ClosePosition()
    {
        closePositionPromptComponent.active = false;
        if (closePositionPromptComponent.quantitySlider.value == closePositionPromptComponent.maxQuantity)
        {
            closePositionPromptComponent.orderPageComponent.closePositionButton.interactable = false;
            closePositionPromptComponent.orderPageComponent.submitToServer = true;
        }
        else
        {
            // TODO: close partial quantity
        }
    }
    void Show()
    {
        closePositionPromptComponent.active = true;
        closePositionPromptComponent.minQuantity = (float)Utils.GetDecimalPlaceNumber(closePositionPromptComponent.orderPageComponent.marginCalculator.quantityPrecision);
        closePositionPromptComponent.maxQuantity = float.Parse(closePositionPromptComponent.orderPageComponent.positionInfoQuantityFilledText.text);

        closePositionPromptComponent.minQuantityText.text = closePositionPromptComponent.minQuantity.ToString();
        closePositionPromptComponent.maxQuantityText.text = closePositionPromptComponent.orderPageComponent.positionInfoQuantityFilledText.text;
        closePositionPromptComponent.quantitySlider.minValue = closePositionPromptComponent.minQuantity;
        closePositionPromptComponent.quantitySlider.maxValue = closePositionPromptComponent.maxQuantity;

        closePositionPromptComponent.quantitySlider.value = closePositionPromptComponent.maxQuantity;
        closePositionPromptComponent.quantityInput.text = closePositionPromptComponent.orderPageComponent.positionInfoQuantityFilledText.text;
    }
}