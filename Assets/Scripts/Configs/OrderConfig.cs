using UnityEngine;

public static class OrderConfig
{
    public static readonly Color DISPLAY_COLOR_GREEN = new(0, 108 / 255f, 0);
    public static readonly Color DISPLAY_COLOR_RED = Color.red;
    public static readonly Color DISPLAY_COLOR_BLACK = Color.black;
    public static readonly Color DISPLAY_COLOR_ORANGE = new(1, 150 / 255f, 0);
    public static readonly Color DISPLAY_COLOR_YELLOW = new(150 / 255f, 150 / 255f, 0);
    public static readonly Color DISPLAY_COLOR_CYAN = new(0, 150 / 255f, 150 / 255f);
    public static readonly float MARGIN_WEIGHT_DISTRIBUTION_VALUE_MIN = -5;
    public static readonly float MARGIN_WEIGHT_DISTRIBUTION_VALUE_MAX = 5;
    public static readonly float TRAILING_MIN_PERCENTAGE = 0.1f;
    public static readonly float TRAILING_MAX_PERCENTAGE = 99f;
}