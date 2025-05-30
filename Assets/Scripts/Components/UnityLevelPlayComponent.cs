using UnityEngine;
using UnityEngine.Events;

public class UnityLevelPlayComponent : MonoBehaviour
{
    [Header("Config")]
    public string appKey;
    public bool enableAds;
    private bool _showBanner;
    public bool showBanner
    {
        set
        {
            _showBanner = value;
            onChange_showBanner.Invoke();
        }
        get { return _showBanner; }
    }
    [HideInInspector] public UnityEvent onChange_showBanner = new();
    private bool _showRewardedVideo;
    public bool showRewardedVideo
    {
        set
        {
            _showRewardedVideo = value;
            onChange_showRewardedVideo.Invoke();
        }
        get { return _showRewardedVideo; }
    }
    [HideInInspector] public UnityEvent onChange_showRewardedVideo = new();
}
