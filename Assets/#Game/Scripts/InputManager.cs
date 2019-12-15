using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

    public static void ManualUpdate()
    {
        Mouse.ManualUpdate();

#if  UNITY_ANDROID && !UNITY_EDITOR
        
        AccelerationVallue.ManualUpdate();

#endif  //  UNITY_ANDROID && !UNITY_EDITOR
    }

    public static bool IsClick(eInputType inputType)
    {
        return (inputType == eInputType.ClickLeft || inputType == eInputType.ClickRight);
    }

    public static bool IsClickRight(eInputType inputType)
    {
        return (inputType == eInputType.ClickRight);
    }

    public static bool IsClickLeft(eInputType inputType)
    {
        return (inputType == eInputType.ClickLeft);
    }
}


public enum eInputType
{
    Any,
    ClickRight,
    ClickLeft,

    Random = 99,

}

