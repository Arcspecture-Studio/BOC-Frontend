using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetBalanceResponse : WebsocketGeneralResponse
    {
        public Dictionary<string, double> balances;
    }
}