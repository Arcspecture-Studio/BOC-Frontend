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
        closePositionPromptComponent.orderPageComponent.quantityToClose = closePositionPromptComponent.customSlider.slider.value == closePositionPromptComponent.customSlider.slider.maxValue ?
        0 : closePositionPromptComponent.customSlider.slider.value;
        closePositionPromptComponent.orderPageComponent.submitToServer = true;
    }
    void Show()
    {
        closePositionPromptComponent.active = true;

        float minQuantity = Utils.GetDecimalPlaceNumber(closePositionPromptComponent.orderPageComponent.marginCalculator.quantityPrecision);
        float maxQuantity = float.Parse(closePositionPromptComponent.orderPageComponent.positionInfoQuantityFilledText.text);
        closePositionPromptComponent.customSlider.SetRangeAndPrecision(
            minQuantity,
            maxQuantity,
            maxQuantity,
            (int)closePositionPromptComponent.orderPageComponent.marginCalculator.quantityPrecision
        );
    }
}