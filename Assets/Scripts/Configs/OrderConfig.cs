using System.Collections.Generic;
using UnityEngine;

public static class OrderConfig
{
    public static readonly Color DISPLAY_COLOR_GREEN = new(0, 108 / 255f, 0);
    public static readonly List<string> TIMEFRAME_ARRAY = new(){
        "1m",
        "3m",
        "5m",
        "15m",
        "30m",
        "1h",
        "2h",
        "4h",
        "6h",
        "8h",
        "12h",
        "1d",
        "3d",
        "1w",
        "1M"
    };
}