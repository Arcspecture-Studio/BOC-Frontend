public class WebsocketConfigEnvData
{
    public string host;
    public string port;
    public bool enableLog;
    public bool enableEncryption;

    public WebsocketConfigEnvData(string host, string port, bool enableLog, bool enableEncryption)
    {
        this.host = host;
        this.port = port;
        this.enableLog = enableLog;
        this.enableEncryption = enableEncryption;
    }
}