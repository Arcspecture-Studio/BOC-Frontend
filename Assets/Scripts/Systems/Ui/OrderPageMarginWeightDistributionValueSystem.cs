using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WebSocketSharp;

public class OrderPageMarginWeightDistributionValueSystem : MonoBehaviour
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
        orderPageComponent.marginWeightDistributionValueInput.text = orderPageComponent.marginWeightDistributionValueSlider.value.ToString();
        orderPageComponent.marginWeightDistributionValueSlider.onValueChanged.AddListener(value =>
        {
            float roundedValue = (float)Utils.RoundTwoDecimal((double)value);
            orderPageComponent.marginWeightDistributionValueSlider.value = roundedValue;
            orderPageComponent.marginWeightDistributionValueInput.text = roundedValue.ToString();
        });
        orderPageComponent.marginWeightDistributionValueInput.onSubmit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = "0";
            double parsedValue = Math.Min(Math.Max(double.Parse(value), orderPageComponent.marginWeightDistributionValueSlider.minValue), orderPageComponent.marginWeightDistributionValueSlider.maxValue);
            float roundedValue = (float)Utils.RoundTwoDecimal(parsedValue);
            orderPageComponent.marginWeightDistributionValueInput.text = roundedValue.ToString();
            orderPageComponent.marginWeightDistributionValueSlider.value = roundedValue;
        });
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.marginWeightDistributionValueInput.text = settingPageComponent.marginWeightDistributionValueSlider.value.ToString();
        settingPageComponent.marginWeightDistributionValueSlider.onValueChanged.AddListener(value =>
        {
            double roundedValue = Utils.RoundTwoDecimal(value);
            settingPageComponent.marginWeightDistributionValueSlider.value = (float)roundedValue;
            settingPageComponent.marginWeightDistributionValueInput.text = roundedValue.ToString();

            profileComponent.activeProfile.preference.marginWeightDistributionValue = roundedValue;
        });
        EventTrigger.Entry pointerUpEvent = new() { eventID = EventTriggerType.PointerUp };
        pointerUpEvent.callback.AddListener(eventData => settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.marginWeightDistributionValueSliderTrigger.triggers.Add(pointerUpEvent);
        settingPageComponent.marginWeightDistributionValueInput.onSubmit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = "0";
            double parsedValue = Math.Min(Math.Max(double.Parse(value), settingPageComponent.marginWeightDistributionValueSlider.minValue), settingPageComponent.marginWeightDistributionValueSlider.maxValue);
            double roundedValue = Utils.RoundTwoDecimal(parsedValue);
            settingPageComponent.marginWeightDistributionValueInput.text = roundedValue.ToString();
            settingPageComponent.marginWeightDistributionValueSlider.value = (float)roundedValue;

            profileComponent.activeProfile.preference.marginWeightDistributionValue = roundedValue;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}