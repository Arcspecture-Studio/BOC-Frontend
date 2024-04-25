using System;

[Serializable]
public class Profile
{
    public string _id;
    public string name;
    public PlatformEnum? activePlatform;
    public Perference preference;
}