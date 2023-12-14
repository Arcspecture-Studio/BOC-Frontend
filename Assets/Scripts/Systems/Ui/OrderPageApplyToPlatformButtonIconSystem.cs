using UnityEngine;
using UnityEngine.UI;

public class OrderPageApplyToPlatformButtonIconSystem : MonoBehaviour
{
    [SerializeField] Image binance;
    [SerializeField] Image mexc;

    PlatformComponent platformComponent;

    PlatformEnum? platform = null;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
    }
    void Update()
    {
        if (platform == platformComponent.tradingPlatform) return;
        platform = platformComponent.tradingPlatform;
        switch(platform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                binance.enabled = true;
                mexc.enabled = false;
                break;
            case PlatformEnum.MEXC:
                binance.enabled = false;
                mexc.enabled = true;
                break;
        }
    }
}
