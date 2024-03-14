using UnityEngine;
using UnityEngine.UI;

public class OrderPageApplyToPlatformButtonIconSystem : MonoBehaviour
{
    [SerializeField] Image binance;
    [SerializeField] Image mexc;

    PlatformComponentOld platformComponentOld;

    PlatformEnum? platform = null;

    void Start()
    {
        platformComponentOld = GlobalComponent.instance.platformComponentOld;
    }
    void Update()
    {
        if (platform == platformComponentOld.activePlatform) return;
        platform = platformComponentOld.activePlatform;
        switch (platform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                binance.enabled = true;
                mexc.enabled = false;
                break;
        }
    }
}
