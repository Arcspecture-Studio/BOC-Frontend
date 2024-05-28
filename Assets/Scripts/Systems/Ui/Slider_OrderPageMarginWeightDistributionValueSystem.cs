using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WebSocketSharp;

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
        orderPageComponent.marginWeightDistributionValueInput.text = orderPageComponent.marginWeightDistributionValueSlider.value.ToString();
        orderPageComponent.marginWeightDistributionValueSlider.onValueChanged.AddListener(value =>
        {
            double roundedValue = Utils.RoundTwoDecimal(value);
            orderPageComponent.marginWeightDistributionValueSlider.value = (float)roundedValue;
            orderPageComponent.marginWeightDistributionValueInput.text = roundedValue.ToString();
        });
        orderPageComponent.marginWeightDistributionValueInput.onSubmit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = orderPageComponent.marginWeightDistributionValueSlider.value.ToString();
            double parsedValue = Math.Min(Math.Max(double.Parse(value), orderPageComponent.marginWeightDistributionValueSlider.minValue), orderPageComponent.marginWeightDistributionValueSlider.maxValue);
            double roundedValue = Utils.RoundTwoDecimal(parsedValue);
            orderPageComponent.marginWeightDistributionValueInput.text = roundedValue.ToString();
            orderPageComponent.marginWeightDistributionValueSlider.value = (float)roundedValue;
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

            profileComponent.activeProfile.preference.order.marginWeightDistributionValue = roundedValue;
        });
        EventTrigger.Entry pointerUpEvent = new() { eventID = EventTriggerType.PointerUp };
        pointerUpEvent.callback.AddListener(eventData => settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.marginWeightDistributionValueSliderTrigger.triggers.Add(pointerUpEvent);
        settingPageComponent.marginWeightDistributionValueInput.onSubmit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = settingPageComponent.marginWeightDistributionValueSlider.value.ToString();
            double parsedValue = Math.Min(Math.Max(double.Parse(value), settingPageComponent.marginWeightDistributionValueSlider.minValue), settingPageComponent.marginWeightDistributionValueSlider.maxValue);
            double roundedValue = Utils.RoundTwoDecimal(parsedValue);
            settingPageComponent.marginWeightDistributionValueInput.text = roundedValue.ToString();
            settingPageComponent.marginWeightDistributionValueSlider.value = (float)roundedValue;

            profileComponent.activeProfile.preference.order.marginWeightDistributionValue = roundedValue;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}