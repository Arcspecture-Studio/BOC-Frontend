using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetAccInfoResponse : WebrequestGeneralResponse
    {
        public long feeTier;
        public bool canTrade;
        public bool canDeposit;
        public bool canWithdraw;
        public long updateTime;
        public bool multiAssetsMargin;
        public string totalInitialMargin;
        public string totalMaintMargin;
        public string totalWalletBalance;
        public string totalUnrealizedProfit;
        public string totalMarginBalance;
        public string totalPositionInitialMargin;
        public string totalOpenOrderInitialMargin;
        public string totalCrossWalletBalance;
        public string totalCrossUnPnl;
        public string availableBalance;
        public string maxWithdrawAmount;
        public List<WebrequestGetAccInfoResponseAsset> assets;
        public List<WebrequestGetAccInfoResponsePosition> positions;
    }

    [Serializable]
    public class WebrequestGetAccInfoResponseAsset
    {
        public string asset;
        public string walletBalance;
        public string unrealizedProfit;
        public string marginBalance;
        public string maintMargin;
        public string initialMargin;
        public string positionInitialMargin;
        public string openOrderInitialMargin;
        public string maxWithdrawAmount;
        public string crossWalletBalance;
        public string crossUnPnl;
        public string availableBalance;
        public string marginAvailable;
        public long updateTime;
    }

    [Serializable]
    public class WebrequestGetAccInfoResponsePosition
    {
        public string symbol;
        public string initialMargin;
        public string maintMargin;
        public string unrealizedProfit;
        public string positionInitialMargin;
        public string openOrderInitialMargin;
        public string leverage;
        public string isolated;
        public string entryPrice;
        public string maxNotional;
        public string positionSide;
        public string positionAmt;
        public string notional;
        public string isolatedWallet;
        public long updateTime;
        public string bidNotional;
        public string askNotional;
    }
}