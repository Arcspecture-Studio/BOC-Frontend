using System.Collections.Generic;

public enum TimeframeEnum
{
    minute1,
    minute3,
    minute5,
    minute15,
    minute30,
    hour1,
    hour2,
    hour4,
    hour6,
    hour8,
    hour12,
    day1,
    day3,
    week1,
    month1
}
public static class TimeframeArray
{
    public static readonly List<string> TIMEFRAME_ARRAY = new(){
        "1 minute",
        "3 minutes",
        "5 minutes",
        "15 minutes",
        "30 minutes",
        "1 hour",
        "2 hours",
        "4 hours",
        "6 hours",
        "8 hours",
        "12 hours",
        "1 day",
        "3 days",
        "1 week",
        "1 month"
    };
}