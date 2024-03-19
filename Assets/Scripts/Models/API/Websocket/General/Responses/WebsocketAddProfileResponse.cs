using System;

namespace General
{
    [Serializable]
    public class WebsocketAddProfileResponse : WebsocketGeneralResponse
    {
        public Profile profile;
    }
}