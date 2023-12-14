#pragma warning disable CS8632

using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetBalanceResponseList : List<WebrequestGetBalanceResponseData>
    {
    }

    [Serializable]
    public class WebrequestGetBalanceResponseData : WebrequestGeneralResponse
    {
        public string? accountAlias;
        public string? asset;
        public string? balance;
        public string? crossWalletBalance;
        public string? crossUnPnl;
        public string? availableBalance;
        public string? maxWithdrawAmount;
        public bool? marginAvailable;
        public long? updateTime;
    }
}