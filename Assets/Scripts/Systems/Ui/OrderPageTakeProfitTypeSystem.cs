using UnityEngine;

public class OrderPageTakeProfitTypeSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;

    const int takeOnReturnTrailingEnum = (int)OrderTakeProfitTypeEnum.TAKE_ON_RETURN_TRAILING;
    void Start()
    {
        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.riskRewardRatioObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value > (int)OrderTakeProfitTypeEnum.NONE);
        orderPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value == takeOnReturnTrailingEnum);
        orderPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            orderPageComponent.riskRewardRatioObject.SetActive(orderPageComponent.lockForEdit && value > (int)OrderTakeProfitTypeEnum.NONE);
            orderPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(orderPageComponent.lockForEdit && value == takeOnReturnTrailingEnum);
        });
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(settingPageComponent.takeProfitTypeDropdown.value == takeOnReturnTrailingEnum);
        settingPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            settingPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(value == takeOnReturnTrailingEnum);
        });
    }
}