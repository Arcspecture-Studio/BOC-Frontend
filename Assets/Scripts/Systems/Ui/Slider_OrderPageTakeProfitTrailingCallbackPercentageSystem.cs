using UnityEngine;

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
                orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.SetRangeAndPrecision(
                    BinanceConfig.TRAILING_MIN_PERCENTAGE,
                    BinanceConfig.TRAILING_MAX_PERCENTAGE
                );
                break;
        }

        orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onSliderUp.AddListener(() => orderPageComponent.updateTakeProfitPrice = true);
        orderPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onInputSubmit.AddListener(value => orderPageComponent.updateTakeProfitPrice = true);
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;

        switch (platformComponent.activePlatform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.SetRangeAndPrecision(
                    BinanceConfig.TRAILING_MIN_PERCENTAGE,
                    BinanceConfig.TRAILING_MAX_PERCENTAGE
                );
                break;
        }

        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onSliderMove.AddListener(value => profileComponent.activeProfile.preference.order.takeProfitTrailingCallbackPercentage = value);
        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onSliderUp.AddListener(() => settingPageComponent.updatePreferenceToServer = true);
        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.onInputSubmit.AddListener(value =>
        {
            profileComponent.activeProfile.preference.order.takeProfitTrailingCallbackPercentage = value;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}