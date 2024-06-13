using System.Collections.Generic;
using UnityEngine;

public static class OrderConfig
{
    public static readonly Color DISPLAY_COLOR_GREEN = new(0, 108 / 255f, 0);
    public static readonly Color DISPLAY_COLOR_RED = Color.red;
    public static readonly Color DISPLAY_COLOR_BLACK = Color.black;
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
    public static readonly float marginWeightDistributionValueMin = -5;
    public static readonly float marginWeightDistributionValueMax = 5;
}