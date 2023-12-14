using System;

[Serializable]
public class Request
{
    public PlatformEnum platform;
    public WebrequestRequestTypeEnum requestType;
    public string host;
    public string path;
    public string uri;
    public string id
    {
        get { return requestType + uri; }
    }
    public string queries;
}