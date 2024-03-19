using System;

namespace General
{
    [Serializable]
    public class WebsocketRemoveProfileResponse : WebsocketGeneralResponse
    {
        public string profileId;
    }
}