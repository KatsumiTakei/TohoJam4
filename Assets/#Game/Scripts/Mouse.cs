using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse
{

    public void ManualUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EventManager.BroadcastMultipleInput(eInputType.ClickLeft);
        }

        if (Input.GetMouseButtonDown(1))
        {
            EventManager.BroadcastMultipleInput(eInputType.ClickRight);
        }
    }

}
