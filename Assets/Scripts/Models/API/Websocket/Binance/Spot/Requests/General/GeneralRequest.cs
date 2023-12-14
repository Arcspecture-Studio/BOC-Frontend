using System;

namespace Binance
{
    [Serializable]
    public class GeneralRequest
    {
        public string id;
        public string method;

        public GeneralRequest(string method)
        {
            this.id = method.Replace(".", "_") + "_" + Utils.CurrentTimestamp().ToString();
            this.method = method;
        }
    }
}