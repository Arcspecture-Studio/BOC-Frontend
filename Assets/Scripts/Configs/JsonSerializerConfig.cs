using Unity.Plastic.Newtonsoft.Json;

public static class JsonSerializerConfig
{
    public static readonly JsonSerializerSettings settings = new JsonSerializerSettings
    {
        FloatFormatHandling = FloatFormatHandling.DefaultValue,
        NullValueHandling = NullValueHandling.Ignore
    };
}