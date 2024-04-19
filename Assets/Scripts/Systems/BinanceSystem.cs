#pragma warning disable CS0168

using Newtonsoft.Json;
using System;
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
    MiniPromptComponent miniPromptComponent;

    Binance.WebrequestRequest getExchangeInfoRequest = null;
    Binance.WebrequestRequest getBalanceRequest = null;

    string logPrefix = "[BinanceSystem] ";

    void Start()
    {
        webrequestComponent = GlobalComponent.instance.webrequestComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;
    }

    #region Deprecated, for reference only
    void GetBalance()
    {
        getBalanceRequest = new Binance.WebrequestGetBalanceRequest(testnet);
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

            miniPromptComponent.message = "Balance Updated";

            binanceComponent.walletBalances.Clear();
            response.ForEach(asset =>
            {
                binanceComponent.walletBalances.Add(asset.asset, double.Parse(asset.balance));
            });
        }
    }
    void GetExchangeInfo()
    {
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
                // binanceComponent.getExchangeInfo = true;
                return;
            }

            binanceComponent.allSymbols.Clear();
            binanceComponent.marginAssets.Clear();
            binanceComponent.quantityPrecisions.Clear();
            binanceComponent.pricePrecisions.Clear();
            response.symbols.ForEach(symbol =>
            {
                binanceComponent.allSymbols.Add(symbol.symbol);
                binanceComponent.marginAssets.TryAdd(symbol.symbol, symbol.marginAsset);
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
        }
    }
    #endregion
}