using System;

[Serializable]
public class Response
{
    public string id;
    public WebrequestStatusEnum status;
    public string responseJsonString;

    public Response(string id, WebrequestStatusEnum status, string responseJsonString)
    {
        this.id = id;
        this.status = status;
        this.responseJsonString = responseJsonString;
    }
}