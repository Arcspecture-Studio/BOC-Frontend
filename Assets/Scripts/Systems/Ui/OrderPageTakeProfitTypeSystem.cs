using UnityEngine;

public class OrderPageTakeProfitTypeSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;

    const int takeOnReturnTrailingEnum = (int)TakeProfitTypeEnum.TRAILING;
    void Start()
    {
        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.riskRewardRatioObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value > (int)TakeProfitTypeEnum.NONE);
        orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.gameObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value == takeOnReturnTrailingEnum);
        orderPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            orderPageComponent.riskRewardRatioObject.SetActive(orderPageComponent.lockForEdit && value > (int)TakeProfitTypeEnum.NONE);
            orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.gameObject.SetActive(orderPageComponent.lockForEdit && value == takeOnReturnTrailingEnum);

            orderPageComponent.updateTakeProfitPrice = true;
        });
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.riskRewardRatioObject.SetActive(settingPageComponent.takeProfitTypeDropdown.value != (int)TakeProfitTypeEnum.NONE);
        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.gameObject.SetActive(settingPageComponent.takeProfitTypeDropdown.value == takeOnReturnTrailingEnum);
        settingPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            settingPageComponent.riskRewardRatioObject.SetActive(value != (int)TakeProfitTypeEnum.NONE);
            settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.gameObject.SetActive(value == takeOnReturnTrailingEnum);
        });
    }
}