using UnityEngine;

public class Slider_OrderPageMarginWeightDistributionValueSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;

    void Start()
    {
        profileComponent = GlobalComponent.instance.profileComponent;

        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.marginWeightDistributionValueCustomSlider.SetRangeAndPrecision(OrderConfig.marginWeightDistributionValueMin, OrderConfig.marginWeightDistributionValueMax);
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.marginWeightDistributionValueCustomSlider.SetRangeAndPrecision(OrderConfig.marginWeightDistributionValueMin, OrderConfig.marginWeightDistributionValueMax);

        settingPageComponent.marginWeightDistributionValueCustomSlider.onSliderMove.AddListener(value => profileComponent.activeProfile.preference.order.marginWeightDistributionValue = value);
        settingPageComponent.marginWeightDistributionValueCustomSlider.onSliderUp.AddListener(() => settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.marginWeightDistributionValueCustomSlider.onInputSubmit.AddListener(value =>
        {
            profileComponent.activeProfile.preference.order.marginWeightDistributionValue = value;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}