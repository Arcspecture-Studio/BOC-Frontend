#pragma warning disable CS8632

using System.Collections.Generic;
using UnityEngine;

public class ProfileComponent : MonoBehaviour
{
    public Dictionary<string, Profile> profiles;
    public string activeProfileId;
    public Profile activeProfile
    {
        get
        {
            if (profiles == null) return null;
            profiles.TryGetValue(activeProfileId, out Profile profile);
            return profile;
        }
    }
}
