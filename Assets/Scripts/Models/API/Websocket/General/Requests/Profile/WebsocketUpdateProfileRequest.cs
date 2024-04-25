#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateProfileRequest : WebsocketGeneralRequest
    {
        public string profileId;
        public string? name;
        public PlatformEnum? activePlatform;
        public bool? makeItDefault;
        public Perference? preference;

        public WebsocketUpdateProfileRequest(string token, string profileId, string name) : base(WebsocketEventTypeEnum.UPDATE_PROFILE, token)
        {
            this.profileId = profileId;
            this.name = name;
        }
        public WebsocketUpdateProfileRequest(string token, string profileId, PlatformEnum activePlatform) : base(WebsocketEventTypeEnum.UPDATE_PROFILE, token)
        {
            this.profileId = profileId;
            this.activePlatform = activePlatform;
        }
        public WebsocketUpdateProfileRequest(string token, string profileId, bool makeItDefault) : base(WebsocketEventTypeEnum.UPDATE_PROFILE, token)
        {
            this.profileId = profileId;
            this.makeItDefault = makeItDefault;
        }
        public WebsocketUpdateProfileRequest(string token, string profileId, Perference preference) : base(WebsocketEventTypeEnum.UPDATE_PROFILE, token)
        {
            this.profileId = profileId;
            this.preference = preference;
        }
    }
}