using System;

public static class SecretConfig
{
    public static readonly string ENCRYPTION_ACCESS_TOKEN_32 = Environment.GetEnvironmentVariable("ENCRYPTION_ACCESS_TOKEN_32") ?? "abcdefghijklmnopqrstuvwxyz123456";
}