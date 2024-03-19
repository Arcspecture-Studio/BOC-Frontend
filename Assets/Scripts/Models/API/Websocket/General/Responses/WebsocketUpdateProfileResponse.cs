using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateProfileResponse : WebsocketGeneralResponse
    {
        public string profileId;
        public UpdateProfilePropertyEnum property;
    }
}