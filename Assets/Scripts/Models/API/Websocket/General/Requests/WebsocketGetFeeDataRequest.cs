using System;

namespace General
{
    [Serializable]
    public class WebsocketGetFeeDataRequest : WebsocketGeneralRequest
    {
        public PlatformEnum platform;
        public string symbol;

        public WebsocketGetFeeDataRequest(string token, PlatformEnum platform, string symbol) : base(WebsocketEventTypeEnum.GET_FEE, token)
        {
            this.platform = platform;
            this.symbol = symbol;
        }
    }
}