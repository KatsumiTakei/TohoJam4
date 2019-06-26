using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    static Mouse mouse = new Mouse();


    public static void ManualUpdate()
    {
        mouse.ManualUpdate();
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

