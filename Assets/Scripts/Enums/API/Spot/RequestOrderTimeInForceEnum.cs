public static class RequestOrderTimeInForceEnum
{
    public const string GTC = "GTC"; // Good 'til Canceled – the order will remain on the book until you cancel it, or the order is completely filled.
    public const string IOC = "IOC"; // Immediate or Cancel – the order will be filled for as much as possible, the unfilled quantity immediately expires.
    public const string FOK = "FOK"; // Fill or Kill – the order will expire unless it cannot be immediately filled for the entire quantity.
}