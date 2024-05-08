#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateProfileRequest : WebsocketGeneralRequest
    {
        public string profileId;
        public string? name;
        public string? platformId;
        public bool? makeItDefault;
        public Preference? preference;

        public WebsocketUpdateProfileRequest(string token, string profileId, string nameOrPlatformId, UpdateProfilePropertyEnum updateProfilePropertyEnum) : base(WebsocketEventTypeEnum.UPDATE_PROFILE, token)
        {
            this.profileId = profileId;
            switch (updateProfilePropertyEnum)
            {
                case UpdateProfilePropertyEnum.name:
                    this.name = nameOrPlatformId;
                    break;
                case UpdateProfilePropertyEnum.platformId:
                    this.platformId = nameOrPlatformId;
                    break;
            }
        }
        public WebsocketUpdateProfileRequest(string token, string profileId, bool makeItDefault) : base(WebsocketEventTypeEnum.UPDATE_PROFILE, token)
        {
            this.profileId = profileId;
            this.makeItDefault = makeItDefault;
        }
        public WebsocketUpdateProfileRequest(string token, string profileId, Preference preference) : base(WebsocketEventTypeEnum.UPDATE_PROFILE, token)
        {
            this.profileId = profileId;
            this.preference = preference;
        }
    }
}