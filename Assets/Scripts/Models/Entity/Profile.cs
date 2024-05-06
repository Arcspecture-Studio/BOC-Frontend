using System;

[Serializable]
public class Profile
{
    public string _id;
    public string platformId;
    public string name;
    public PlatformEnum? activePlatform; // TODO
    public Preference preference;
}