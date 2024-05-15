using System.Collections.Generic;
using UnityEngine;

public class ProfileComponent : MonoBehaviour
{
    public Dictionary<string, Profile> profiles = new();
    public string activeProfileId;
    public Profile activeProfile
    {
        get
        {
            profiles.TryGetValue(activeProfileId, out Profile profile);
            return profile;
        }
    }
}
