using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrRemovePlatformResponse : WebsocketGeneralResponse
    {
        public PlatformEnum platform;
        public PlatformEnum newActivePlatform; // TODO: set profileComponent.activeProfile.activePlatform.Value to this when add or remove platform
    }
}