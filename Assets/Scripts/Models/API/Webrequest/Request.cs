using System;
using MongoDB.Bson;

[Serializable]
public class Request
{
    public PlatformEnum platform;
    public WebrequestRequestTypeEnum requestType;
    public string path;
    public string queries;
    public string id;

    public Request()
    {
        id = ObjectId.GenerateNewId().ToString();
    }
}