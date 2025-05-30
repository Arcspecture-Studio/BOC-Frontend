using UnityEngine;

public class Slider_TakeProfitTrailingCallbackPercentageSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;

    void Awake()
    {
        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.SetRangeAndPrecision(
            OrderConfig.TRAILING_MIN_PERCENTAGE,
            OrderConfig.TRAILING_MAX_PERCENTAGE
        );

        orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onSliderUp.AddListener(() => orderPageComponent.updateToServer = true);
        orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onInputSubmit.AddListener(value => orderPageComponent.updateToServer = true);
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.SetRangeAndPrecision(
            OrderConfig.TRAILING_MIN_PERCENTAGE,
            OrderConfig.TRAILING_MAX_PERCENTAGE
        );

        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onSliderMove.AddListener(value =>
        GlobalComponent.instance.profileComponent.activeProfile.preference.order.takeProfitTrailingCallbackPercentage = value);
        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onSliderUp.AddListener(() =>
        settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onInputSubmit.AddListener(value =>
        {
            GlobalComponent.instance.profileComponent.activeProfile.preference.order.takeProfitTrailingCallbackPercentage = value;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}