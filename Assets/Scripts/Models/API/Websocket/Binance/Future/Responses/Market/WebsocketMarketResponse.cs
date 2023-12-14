#pragma warning disable CS8632

using System;

namespace Binance
{
    [Serializable]
    public class WebsocketMarketResponse : WebsocketMarketResponseData<WebsocketGeneralResponseData>
    {
        public string? stream;
    }

    [Serializable]
    public class WebsocketMarketResponseData<T>
    {
        public T data;
    }
}