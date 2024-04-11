#pragma warning disable CS8632

using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetInitialDataResponse : WebsocketGetRuntimeDataResponse
    {
        public string defaultProfileId;
        public WebsocketGetInitialDataAccountData accountData;
        public object? platformData;
    }
    [Serializable]
    public class WebsocketGetInitialDataAccountData
    {
        public Dictionary<string, Profile> profiles;
        public List<PlatformEnum> platformList;
    }
}