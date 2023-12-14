using System;

[Serializable]
public class Response
{
    public string id;
    public string logStatus;
    public string responseJsonString;

    public Response(string id, string logStatus, string responseJsonString)
    {
        this.id = id;
        this.logStatus = logStatus;
        this.responseJsonString = responseJsonString;
    }
}