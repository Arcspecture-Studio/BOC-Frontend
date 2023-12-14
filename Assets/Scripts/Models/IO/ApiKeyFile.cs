using System;

[Serializable]
public class ApiKeyFile
{
    public PlatformEnum platform;
    public string apiKey;
    public string apiSecret;
    public string loginPhrase;

    public ApiKeyFile(PlatformEnum platform, string apiKey, string apiSecret, string loginPhrase)
    {
        this.platform = platform;
        this.apiKey = apiKey;
        this.apiSecret = apiSecret;
        this.loginPhrase = loginPhrase;
    }
}