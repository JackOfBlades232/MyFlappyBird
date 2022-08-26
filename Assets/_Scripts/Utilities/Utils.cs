using UnityEngine;

public static class Utils
{
    public const float Precision = 0.001f;
    
    public const float StraightAngle = 90;

    public static void Pause() => Time.timeScale = 0;

    public static void Unpause() => Time.timeScale = 1;
}