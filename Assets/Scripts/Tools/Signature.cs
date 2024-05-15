using System.Security.Cryptography;
using System.Text;
using System;

public static class Signature
{
    public static string Binance(string queryString, string apiSecret)
    {
        byte[] secretKeyBytes = Encoding.UTF8.GetBytes(apiSecret);
        byte[] queryStringBytes = Encoding.UTF8.GetBytes(queryString);
        using (var hmac = new HMACSHA256(secretKeyBytes))
        {
            byte[] hashedBytes = hmac.ComputeHash(queryStringBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
