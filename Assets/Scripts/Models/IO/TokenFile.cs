using System;

[Serializable]
public class TokenFile
{
    public string token;
    public byte[] cache;

    public TokenFile(string token, byte[] cache)
    {
        this.token = token;
        this.cache = cache;
    }
}