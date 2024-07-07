using System;
using AYellowpaper.SerializedCollections;

namespace General
{
    [Serializable]
    public class WebsocketGetBalanceResponse : WebsocketGeneralResponse
    {
        public SerializedDictionary<string, float> balances;
    }
}