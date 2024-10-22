#pragma warning disable 0618

using UnityEngine;

public class UnityLevelPlaySystem : MonoBehaviour
{
    UnityLevelPlayComponent unityLevelPlayComponent;

    string logPrefix = "[UnityLevelPlaySystem] ";

    void Start()
    {
        if (unityLevelPlayComponent == null) unityLevelPlayComponent = GlobalComponent.instance.unityLevelPlayComponent;
        if (!unityLevelPlayComponent.enableAds) return;

        unityLevelPlayComponent.onChange_showBanner.AddListener(ShowBanner);
        unityLevelPlayComponent.onChange_showRewardedVideo.AddListener(() =>
        {
            if (unityLevelPlayComponent.showRewardedVideo) ShowRewarded();
        });

#if UNITY_ANDROID
        unityLevelPlayComponent.appKey = UnityLevelPlayConfig.ANDROID_APPKEY;
#elif UNITY_IPHONE
        unityLevelPlayComponent.appKey = "";
#else
        unityLevelPlayComponent.appKey = "unexpected_platform";
#endif

        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(unityLevelPlayComponent.appKey);

        unityLevelPlayComponent.showBanner = true;
        LoadRewarded();
    }
    void OnEnable()
    {
        if (unityLevelPlayComponent == null) unityLevelPlayComponent = GlobalComponent.instance.unityLevelPlayComponent;
        if (!unityLevelPlayComponent.enableAds) return;

        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

        // AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

        // AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;

    }
    void OnApplicationPause(bool pause)
    {
        if (!unityLevelPlayComponent.enableAds) return;
        IronSource.Agent.onApplicationPause(pause);
    }

    void SdkInitializationCompletedEvent()
    {
        Debug.Log(logPrefix + "SdkInitializationCompletedEvent");
    }

    #region Banner
    void ShowBanner()
    {
        if (unityLevelPlayComponent.showBanner)
        {
            LoadBanner();
        }
        else
        {
            IronSource.Agent.destroyBanner();
            ScalableCanvas.instance.SetBottomPadding(0);
        }
    }
    void LoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
    }
    #endregion

    #region Banner Events
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log(logPrefix + "BannerOnAdLoadedEvent With AdInfo: " + adInfo);

        if (unityLevelPlayComponent.showBanner)
        {
            IronSource.Agent.displayBanner();
#if UNITY_ANDROID
            ScalableCanvas.instance.SetBottomPadding(ScalableCanvas.instance.ConvertDpToCanvasResolution(50));
            // ScalableCanvas.instance.SetBottomPadding(ScalableCanvas.instance.ConvertDpToCanvasResolution(Screen.width <= 720 ? 50 : 90));
#endif
        }
    }
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
    {
        Debug.Log(logPrefix + "BannerOnAdLoadFailedEvent With Error: " + ironSourceError);

        if (unityLevelPlayComponent.showBanner) LoadBanner();
    }
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log(logPrefix + "BannerOnAdClickedEvent With AdInfo: " + adInfo);
    }
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log(logPrefix + "BannerOnAdScreenPresentedEvent With AdInfo: " + adInfo);
    }
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log(logPrefix + "BannerOnAdScreenDismissedEvent With AdInfo: " + adInfo);
    }
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log(logPrefix + "BannerOnAdLeftApplicationEvent With AdInfo: " + adInfo);
    }
    #endregion

    #region Rewarded
    void LoadRewarded()
    {
        IronSource.Agent.loadRewardedVideo();
    }
    void ShowRewarded()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
    }
    #endregion

    #region Rewarded Events
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        // Indicates that there’s an available ad.
        // The adInfo object includes information about the ad that was loaded successfully
        // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
        Debug.Log(logPrefix + "RewardedVideoOnAdAvailable With AdInfo " + adInfo);

        if (unityLevelPlayComponent.showRewardedVideo) ShowRewarded();
    }
    void RewardedVideoOnAdUnavailable()
    {
        // Indicates that no ads are available to be displayed
        // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
        Debug.Log(logPrefix + "RewardedVideoOnAdUnavailable");

        if (unityLevelPlayComponent.enableAds) LoadRewarded();
    }
    void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        // The rewarded video ad was failed to show.
        Debug.Log(logPrefix + "RewardedVideoOnAdShowFailedEvent With Error" + ironSourceError + "And AdInfo " + adInfo);

        if (unityLevelPlayComponent.enableAds) LoadRewarded();
    }
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        // The Rewarded Video ad view has opened. Your activity will loose focus.
        Debug.Log(logPrefix + "RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);

        unityLevelPlayComponent.showRewardedVideo = false;
    }
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        Debug.Log(logPrefix + "RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
    }
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        // The user completed to watch the video, and should be rewarded.
        // The placement parameter will include the reward data.
        // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
        Debug.Log(logPrefix + "RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        // Invoked when the video ad was clicked.
        // This callback is not supported by all networks, and we recommend using it only if
        // it’s supported by all networks you included in your build.
        Debug.Log(logPrefix + "RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }
    #endregion
}
