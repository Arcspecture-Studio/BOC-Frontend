using UnityEngine;

public class OrderPageMarginDistributionModeSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;

    void Start()
    {
        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.marginWeightDistributionValueCustomSlider.gameObject.SetActive(orderPageComponent.marginDistributionModeDropdown.value == 1);
        orderPageComponent.marginDistributionModeDropdown.onValueChanged.AddListener(value =>
        {
            orderPageComponent.marginWeightDistributionValueCustomSlider.gameObject.SetActive(value == 1);
        });
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.marginWeightDistributionValueCustomSlider.gameObject.SetActive(settingPageComponent.marginDistributionModeDropdown.value == 1);
        settingPageComponent.marginDistributionModeDropdown.onValueChanged.AddListener(value =>
        {
            settingPageComponent.marginWeightDistributionValueCustomSlider.gameObject.SetActive(value == 1);
        });
    }
}
