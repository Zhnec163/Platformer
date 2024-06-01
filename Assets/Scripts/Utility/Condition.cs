using UnityEngine;

public class Condition
{
    public static bool IsNotAboutZero(float value) => Mathf.Approximately(value, 0) == false;
    
    public static bool IsAboutZero(float value) => Mathf.Approximately(value, 0);
    
    public static bool IsAboveZero(float value) => value > 0;
    
    public static bool IsLessZero(float value) => value < 0;
}