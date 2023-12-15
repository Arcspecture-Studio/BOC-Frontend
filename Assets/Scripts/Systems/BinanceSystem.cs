#pragma warning disable CS0168

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

[Serializable]
public class BinanceSystem : MonoBehaviour
{
    [SerializeField] bool testnet;
    BinanceComponent binanceComponent
    {
        get
        {
            return testnet
                ? GlobalComponent.instance.binanceTestnetComponent
                : GlobalComponent.instance.binanceComponent;
        }
    }
    WebrequestComponent webrequestComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    IoComponent ioComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;
    PlatformComponent platformComponent;

    Binance.WebrequestRequest createListenKeyRequest = null;
    Binance.WebrequestRequest getExchangeInfoRequest = null;
    Binance.WebrequestRequest getAccInfoRequest = null;
    Binance.WebrequestRequest getBalanceRequest = null;

    float updateListenKeyTimer;
    string logPrefix = "[BinanceSystem] ";

    void Start()
    {
        webrequestComponent = GlobalComponent.instance.webrequestComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        ioComponent = GlobalComponent.instance.ioComponent;
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        platformComponent = GlobalComponent.instance.platformComponent;

        //websocketComponent.connectMarketSocket = true;
    }
    void Update()
    {
        GetExchangeInfo();
        ReceiveExchangeInfo();
        GetBalance();
        ReceiveBalance();
    }

    void GetBalance()
    {
        if (!binanceComponent.getBalance) return;
        binanceComponent.getBalance = false;
        if (binanceComponent.walletBalances == null)
        {
            binanceComponent.walletBalances = new Dictionary<string, double>();
        }
        else
        {
            binanceComponent.walletBalances.Clear();
        }
        getBalanceRequest = new Binance.WebrequestGetBalanceRequest(testnet, binanceComponent.apiSecret);
        webrequestComponent.requests.Add(getBalanceRequest);
    }
    void ReceiveBalance()
    {
        if (getBalanceRequest == null) return;
        if (webrequestComponent.responses.ContainsKey(getBalanceRequest.id))
        {
            Binance.WebrequestGetBalanceResponseList response = null;
            try
            {
                response = JsonConvert.DeserializeObject<Binance.WebrequestGetBalanceResponseList>(webrequestComponent.responses[getBalanceRequest.id], JsonSerializerConfig.settings);
            }
            catch (Exception ex)
            {
                Debug.Log(logPrefix + "Receive balance deserialization error");
            }
            webrequestComponent.responses.Remove(getBalanceRequest.id);
            if (response == null || response.Count == 0) return;
            response.ForEach(asset =>
            {
                if (!binanceComponent.walletBalances.TryAdd(asset.asset, double.Parse(asset.balance)))
                {
                    binanceComponent.walletBalances[asset.asset] = double.Parse(asset.balance);
                }
            });
            if (!binanceComponent.loggedIn)
            {
                binanceComponent.getExchangeInfo = true;
            }
        }
    }
    void GetExchangeInfo()
    {
        if (!binanceComponent.getExchangeInfo) return;
        binanceComponent.getExchangeInfo = false;
        binanceComponent.allSymbols.Clear();
        binanceComponent.quantityPrecisions.Clear();
        binanceComponent.pricePrecisions.Clear();
        getExchangeInfoRequest = new Binance.WebrequestGetExchangeInfoRequest(testnet);
        webrequestComponent.requests.Add(getExchangeInfoRequest);
    }
    void ReceiveExchangeInfo()
    {
        if (getExchangeInfoRequest == null) return;
        if (webrequestComponent.responses.ContainsKey(getExchangeInfoRequest.id))
        {
            Binance.WebrequestGetExchangeInfoResponse response = JsonConvert.DeserializeObject<Binance.WebrequestGetExchangeInfoResponse>(webrequestComponent.responses[getExchangeInfoRequest.id], JsonSerializerConfig.settings);
            webrequestComponent.responses.Remove(getExchangeInfoRequest.id);
            if (response.timezone.IsNullOrEmpty())
            {
                binanceComponent.getExchangeInfo = true;
                return;
            }
            response.symbols.ForEach(symbol =>
            {
                if (!binanceComponent.allSymbols.Contains(symbol.symbol))
                {
                    binanceComponent.allSymbols.Add(symbol.symbol);
                }
                binanceComponent.quantityPrecisions.TryAdd(symbol.symbol, symbol.quantityPrecision);
                long pricePrecision = symbol.pricePrecision;
                symbol.filters.ForEach(filter =>
                {
                    if (filter.tickSize != null)
                    {
                        pricePrecision = Math.Min(Utils.CountDecimalPlaces(double.Parse(filter.tickSize)), pricePrecision);
                    }
                });
                binanceComponent.pricePrecisions.TryAdd(symbol.symbol, pricePrecision);
            });
            binanceComponent.loggedIn = true;
            ioComponent.writeApiKey = true;
            if (platformComponent.testnet == testnet)
            {
                loginComponent.gameObj.SetActive(false);
                retrieveOrdersComponent.destroyOrders = true;
                retrieveOrdersComponent.instantiateOrders = true;
            }
        }
    }

    #region Unused
    void GetAccInfo()
    {
        if (!binanceComponent.getAccInfo) return;
        binanceComponent.getAccInfo = false;
        if (binanceComponent.walletBalances == null)
        {
            binanceComponent.walletBalances = new Dictionary<string, double>();
        }
        else
        {
            binanceComponent.walletBalances.Clear();
        }
        getAccInfoRequest = new Binance.WebrequestGetAccInfoRequest(testnet, binanceComponent.apiSecret);
        webrequestComponent.requests.Add(getAccInfoRequest);
    }
    void ReceiveAccInfo()
    {
        if (getAccInfoRequest == null) return;
        if (webrequestComponent.responses.ContainsKey(getAccInfoRequest.id))
        {
            Binance.WebrequestGetAccInfoResponse response = JsonConvert.DeserializeObject<Binance.WebrequestGetAccInfoResponse>(webrequestComponent.responses[getAccInfoRequest.id], JsonSerializerConfig.settings);
            webrequestComponent.responses.Remove(getAccInfoRequest.id);
            if (response.assets == null) return;
            response.assets.ForEach(asset =>
            {
                if (!binanceComponent.walletBalances.TryAdd(asset.asset, double.Parse(asset.walletBalance)))
                {
                    binanceComponent.walletBalances[asset.asset] = double.Parse(asset.walletBalance);
                }
            });
        }
    }
    #endregion
    #region Execute at server
    void CreateListenKey()
    {
        binanceComponent.listenKey = null;
        createListenKeyRequest = new Binance.WebrequestCreateListenKeyRequest(testnet);
        webrequestComponent.requests.Add(createListenKeyRequest);
        updateListenKeyTimer = 0;
    }
    void ReceiveListenKey()
    {
        if (webrequestComponent.responses.ContainsKey(createListenKeyRequest.id))
        {
            Binance.WebrequestCreateListenKeyResponse response = JsonConvert.DeserializeObject<Binance.WebrequestCreateListenKeyResponse>(webrequestComponent.responses[createListenKeyRequest.id], JsonSerializerConfig.settings);
            webrequestComponent.responses.Remove(createListenKeyRequest.id);
            if (response.listenKey.IsNullOrEmpty())
            {
                CreateListenKey();
                return;
            }
            binanceComponent.listenKey = response.listenKey;
            websocketComponent.connectUserDataSocket = true;
        }
    }
    void RenewListenKey()
    {
        if (binanceComponent.listenKey.IsNullOrEmpty()) return;
        if (updateListenKeyTimer < BinanceConfig.UPDATE_LISTEN_KEY_INTERVAL)
        {
            updateListenKeyTimer += Time.deltaTime;
        }
        else
        {
            updateListenKeyTimer -= BinanceConfig.UPDATE_LISTEN_KEY_INTERVAL;
            Binance.WebrequestRequest request = new Binance.WebrequestRenewListenKeyRequest(testnet);
            webrequestComponent.requests.Add(request);
        }
    }
    #endregion
}