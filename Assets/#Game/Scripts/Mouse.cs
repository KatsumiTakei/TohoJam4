
using UnityEngine;

public static class Mouse
{

    public static void ManualUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.BroadcastMultipleInput(eInputType.ClickLeft);
        }

        if (Input.GetMouseButtonDown(1))
        {
            EventManager.BroadcastMultipleInput(eInputType.ClickRight);
        }
    }

}
