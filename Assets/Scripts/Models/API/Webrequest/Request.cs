using System;
using MongoDB.Bson;

[Serializable]
public class Request
{
    public string id;
    public string platformId;
    public WebrequestRequestTypeEnum requestType;
    public string path;
    public string queries;

    public Request(string platformId)
    {
        this.id = ObjectId.GenerateNewId().ToString();
        this.platformId = platformId;
    }
}