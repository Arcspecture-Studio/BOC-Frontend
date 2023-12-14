using System;

namespace Binance
{
    [Serializable]
    public class RequestWrapper<T> : GeneralRequest
    {
        public T @params;

        public RequestWrapper(string method, T param) : base(method)
        {
            this.@params = param;
        }
    }
}