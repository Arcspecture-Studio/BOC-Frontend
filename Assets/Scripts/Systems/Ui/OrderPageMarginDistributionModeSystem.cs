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
        orderPageComponent.marginWeightDistributionValueObject.SetActive(orderPageComponent.marginDistributionModeDropdown.value == 1);
        orderPageComponent.marginDistributionModeDropdown.onValueChanged.AddListener(value =>
        {
            orderPageComponent.marginWeightDistributionValueObject.SetActive(value == 1);
        });
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.marginWeightDistributionValueObject.SetActive(settingPageComponent.marginDistributionModeDropdown.value == 1);
        settingPageComponent.marginDistributionModeDropdown.onValueChanged.AddListener(value =>
        {
            settingPageComponent.marginWeightDistributionValueObject.SetActive(value == 1);
        });
    }
}
