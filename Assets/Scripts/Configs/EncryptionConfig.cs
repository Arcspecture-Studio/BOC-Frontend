using System;

public static class EncryptionConfig
{
    public static readonly int IV_LENGTH = 16;
    public static readonly string ENCRYPTION_ACCESS_TOKEN_32 = Environment.GetEnvironmentVariable("ENCRYPTION_ACCESS_TOKEN_32") ?? "abcdefghijklmnopqrstuvwxyz123456";
}