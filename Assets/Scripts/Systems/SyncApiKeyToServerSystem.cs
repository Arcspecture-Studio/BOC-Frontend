using UnityEngine;
using WebSocketSharp;

public class SyncApiKeyToServerSystem : MonoBehaviour
{
    BinanceComponent binanceComponent;
    BinanceComponent binanceTestnetComponent;
    WebsocketComponent websocketComponent;
    LoginComponentOld loginComponent;

    void Start()
    {
        binanceComponent = GlobalComponent.instance.binanceComponent;
        binanceTestnetComponent = GlobalComponent.instance.binanceTestnetComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
    }
    void Update()
    {
        if (!websocketComponent.syncApiKeyToServer) return;
        websocketComponent.syncApiKeyToServer = false;
        syncBinance();
        syncBinanceTestnet();
    }
    void syncBinance()
    {
        if (binanceComponent.apiKey.IsNullOrEmpty() || binanceComponent.apiSecret.IsNullOrEmpty()) return;
        General.WebsocketSyncApiKeyRequest request = new General.WebsocketSyncApiKeyRequest(
                binanceComponent.apiKey,
                binanceComponent.apiSecret,
                PlatformEnum.BINANCE,
                binanceComponent.loginPhrase,
                Application.version);
        websocketComponent.generalRequests.Add(request);
    }
    void syncBinanceTestnet()
    {
        if (binanceTestnetComponent.apiKey.IsNullOrEmpty() || binanceTestnetComponent.apiSecret.IsNullOrEmpty()) return;
        General.WebsocketSyncApiKeyRequest request = new General.WebsocketSyncApiKeyRequest(
                binanceTestnetComponent.apiKey,
                binanceTestnetComponent.apiSecret,
                PlatformEnum.BINANCE_TESTNET,
                binanceTestnetComponent.loginPhrase,
                Application.version);
        websocketComponent.generalRequests.Add(request);
    }
}
