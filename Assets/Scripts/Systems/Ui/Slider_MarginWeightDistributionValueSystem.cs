using UnityEngine;

public class Slider_MarginWeightDistributionValueSystem : MonoBehaviour
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
        orderPageComponent.marginWeightDistributionValueCustomSlider.SetRangeAndPrecision(
            OrderConfig.MARGIN_WEIGHT_DISTRIBUTION_VALUE_MIN,
            OrderConfig.MARGIN_WEIGHT_DISTRIBUTION_VALUE_MAX
        );
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.marginWeightDistributionValueCustomSlider.SetRangeAndPrecision(
            OrderConfig.MARGIN_WEIGHT_DISTRIBUTION_VALUE_MIN,
            OrderConfig.MARGIN_WEIGHT_DISTRIBUTION_VALUE_MAX
        );

        settingPageComponent.marginWeightDistributionValueCustomSlider.onSliderMove.AddListener(value => GlobalComponent.instance.profileComponent.activeProfile.preference.order.marginWeightDistributionValue = value);
        settingPageComponent.marginWeightDistributionValueCustomSlider.onSliderUp.AddListener(() => settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.marginWeightDistributionValueCustomSlider.onInputSubmit.AddListener(value =>
        {
            GlobalComponent.instance.profileComponent.activeProfile.preference.order.marginWeightDistributionValue = value;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}