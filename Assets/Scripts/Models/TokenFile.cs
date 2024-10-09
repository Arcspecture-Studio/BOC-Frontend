using System;

[Serializable]
public class TokenFile
{
    public string token;
    public byte[] key;

    public TokenFile(string token, byte[] key)
    {
        this.token = token;
        this.key = key;
    }
}