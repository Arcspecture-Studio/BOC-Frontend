using UnityEngine;

public class Slider_TakeProfitQuantityPercentageSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;

    float min = 1;
    float max = 100;

    void Start()
    {
        profileComponent = GlobalComponent.instance.profileComponent;

        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.takeProfitQuantityPercentageCustomSlider.SetRangeAndPrecision(min, max);

        orderPageComponent.takeProfitQuantityPercentageCustomSlider.onSliderUp.AddListener(() => orderPageComponent.updateTakeProfitPrice = true);
        orderPageComponent.takeProfitQuantityPercentageCustomSlider.onInputSubmit.AddListener(value => orderPageComponent.updateTakeProfitPrice = true);
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.takeProfitQuantityPercentageCustomSlider.SetRangeAndPrecision(min, max);

        settingPageComponent.takeProfitQuantityPercentageCustomSlider.onSliderMove.AddListener(value =>
        profileComponent.activeProfile.preference.order.takeProfitQuantityPercentage = value);
        settingPageComponent.takeProfitQuantityPercentageCustomSlider.onSliderUp.AddListener(() =>
        settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.takeProfitQuantityPercentageCustomSlider.onInputSubmit.AddListener(value =>
        {
            profileComponent.activeProfile.preference.order.takeProfitQuantityPercentage = value;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}
