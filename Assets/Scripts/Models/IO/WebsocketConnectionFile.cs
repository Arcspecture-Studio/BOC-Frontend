using System;

[Serializable]
public class WebsocketConnectionFile
{
    public string connectionId;
    public byte[] secondaryConnectionId;

    public WebsocketConnectionFile(string connectionId, byte[] iv)
    {
        this.connectionId = connectionId;
        this.secondaryConnectionId = iv;
    }
}