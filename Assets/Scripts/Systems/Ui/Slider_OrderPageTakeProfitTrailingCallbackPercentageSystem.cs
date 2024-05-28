using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WebSocketSharp;

public class Slider_OrderPageTakeProfitTrailingCallbackPercentageSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;
    PlatformComponent platformComponent;

    void Start()
    {
        profileComponent = GlobalComponent.instance.profileComponent;
        platformComponent = GlobalComponent.instance.platformComponent;

        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;

        switch (platformComponent.activePlatform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                orderPageComponent.takeProfitTrailingCallbackPercentageSlider.minValue = BinanceConfig.TRAILING_MIN_PERCENTAGE;
                orderPageComponent.takeProfitTrailingCallbackPercentageSlider.maxValue = BinanceConfig.TRAILING_MAX_PERCENTAGE;
                orderPageComponent.takeProfitTrailingCallbackPercentageMinText.text = BinanceConfig.TRAILING_MIN_PERCENTAGE.ToString() + "%";
                orderPageComponent.takeProfitTrailingCallbackPercentageMaxText.text = BinanceConfig.TRAILING_MAX_PERCENTAGE.ToString() + "%";
                break;
        }

        orderPageComponent.takeProfitTrailingCallbackPercentageInput.text = orderPageComponent.takeProfitTrailingCallbackPercentageSlider.value.ToString();

        orderPageComponent.takeProfitTrailingCallbackPercentageSlider.onValueChanged.AddListener(value =>
        {
            double roundedValue = Utils.RoundTwoDecimal(value);
            orderPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)roundedValue;
            orderPageComponent.takeProfitTrailingCallbackPercentageInput.text = roundedValue.ToString();
        });
        EventTrigger.Entry pointerUpEvent = new() { eventID = EventTriggerType.PointerUp };
        pointerUpEvent.callback.AddListener(eventData => orderPageComponent.updateTakeProfitPrice = true);
        orderPageComponent.takeProfitTrailingCallbackPercentageSliderTrigger.triggers.Add(pointerUpEvent);
        orderPageComponent.takeProfitTrailingCallbackPercentageInput.onSubmit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = orderPageComponent.takeProfitTrailingCallbackPercentageSlider.value.ToString();
            double parsedValue = Math.Min(Math.Max(double.Parse(value), BinanceConfig.TRAILING_MIN_PERCENTAGE), BinanceConfig.TRAILING_MAX_PERCENTAGE);
            double roundedValue = Utils.RoundTwoDecimal(parsedValue);
            orderPageComponent.takeProfitTrailingCallbackPercentageInput.text = roundedValue.ToString();
            orderPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)roundedValue;

            orderPageComponent.updateTakeProfitPrice = true;
        });
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;

        switch (platformComponent.activePlatform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                settingPageComponent.takeProfitTrailingCallbackPercentageSlider.minValue = BinanceConfig.TRAILING_MIN_PERCENTAGE;
                settingPageComponent.takeProfitTrailingCallbackPercentageSlider.maxValue = BinanceConfig.TRAILING_MAX_PERCENTAGE;
                settingPageComponent.takeProfitTrailingCallbackPercentageMinText.text = BinanceConfig.TRAILING_MIN_PERCENTAGE.ToString() + "%";
                settingPageComponent.takeProfitTrailingCallbackPercentageMaxText.text = BinanceConfig.TRAILING_MAX_PERCENTAGE.ToString() + "%";
                break;
        }

        settingPageComponent.takeProfitTrailingCallbackPercentageInput.text = settingPageComponent.takeProfitTrailingCallbackPercentageSlider.value.ToString();

        settingPageComponent.takeProfitTrailingCallbackPercentageSlider.onValueChanged.AddListener(value =>
        {
            double roundedValue = Utils.RoundTwoDecimal(value);
            settingPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)roundedValue;
            settingPageComponent.takeProfitTrailingCallbackPercentageInput.text = roundedValue.ToString();

            profileComponent.activeProfile.preference.order.takeProfitTrailingCallbackPercentage = roundedValue;
        });
        EventTrigger.Entry pointerUpEvent = new() { eventID = EventTriggerType.PointerUp };
        pointerUpEvent.callback.AddListener(eventData => settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.takeProfitTrailingCallbackPercentageSliderTrigger.triggers.Add(pointerUpEvent);
        settingPageComponent.takeProfitTrailingCallbackPercentageInput.onSubmit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = settingPageComponent.takeProfitTrailingCallbackPercentageSlider.value.ToString();
            double parsedValue = Math.Min(Math.Max(double.Parse(value), BinanceConfig.TRAILING_MIN_PERCENTAGE), BinanceConfig.TRAILING_MAX_PERCENTAGE);
            double roundedValue = Utils.RoundTwoDecimal(parsedValue);
            settingPageComponent.takeProfitTrailingCallbackPercentageInput.text = roundedValue.ToString();
            settingPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)roundedValue;

            profileComponent.activeProfile.preference.order.takeProfitTrailingCallbackPercentage = roundedValue;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}