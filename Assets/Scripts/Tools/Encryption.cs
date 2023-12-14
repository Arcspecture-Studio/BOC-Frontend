using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Encryption
{
    public static string Encrypt(string str, string key, byte[] iv)
    {
        int keyLength = 32;
        if (keyLength > key.Length) keyLength = key.Length;
        key = key.Substring(0, keyLength);
        using (var cipher = Aes.Create())
        {
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.PKCS7;
            cipher.KeySize = 256;
            cipher.BlockSize = 128;
            cipher.Key = Encoding.UTF8.GetBytes(key);
            cipher.IV = iv;

            using (var encryptor = cipher.CreateEncryptor(cipher.Key, cipher.IV))
            {
                byte[] data = Encoding.UTF8.GetBytes(str);
                byte[] encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(encryptedData).Replace("-", "").ToLower();
            }
        }
    }
    public static string Decrypt(string str, string key, byte[] iv)
    {
        int keyLength = 32;
        if (keyLength > key.Length) keyLength = key.Length;
        key = key.Substring(0, keyLength);
        using (var cipher = Aes.Create())
        {
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.PKCS7;
            cipher.KeySize = 256;
            cipher.BlockSize = 128;
            cipher.Key = Encoding.UTF8.GetBytes(key);
            cipher.IV = iv;

            using (var decryptor = cipher.CreateDecryptor(cipher.Key, cipher.IV))
            {
                byte[] data = StringToByteArray(str);
                byte[] decryptedData = decryptor.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
    public static byte[] GetIv()
    {
        byte[] iv = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(iv);
        }
        return iv;
    }
    private static byte[] StringToByteArray(string str)
    {
        int length = str.Length;
        byte[] bytes = new byte[length / 2];
        for (int i = 0; i < length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
        }
        return bytes;
    }
}