using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetProfileDataResponse : WebsocketGetRuntimeDataResponse
    {
        public string defaultProfileId;
        public WebsocketGetInitialDataAccountData accountData;
    }
    [Serializable]
    public class WebsocketGetInitialDataAccountData
    {
        public Dictionary<string, Profile> profiles;
        public Dictionary<string, PlatformEnum> platforms;
    }
}