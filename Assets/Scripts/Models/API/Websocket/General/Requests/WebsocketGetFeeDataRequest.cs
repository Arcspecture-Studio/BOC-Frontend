using System;

namespace General
{
    [Serializable]
    public class WebsocketGetFeeDataRequest : WebsocketGeneralRequest
    {
        public string platformId;
        public string symbol;

        public WebsocketGetFeeDataRequest(string token, string platformId, string symbol) : base(WebsocketEventTypeEnum.GET_FEE, token)
        {
            this.platformId = platformId;
            this.symbol = symbol;
        }
    }
}