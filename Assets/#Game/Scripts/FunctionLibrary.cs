using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FunctionLibrary
{
    public static eInputType ReverseInput(eInputType input) =>  (input == eInputType.ClickLeft) ? eInputType.ClickLeft : eInputType.ClickRight;
    public static eTileType ReverseTile(eTileType tile) => (tile == eTileType.Black) ? eTileType.Black : eTileType.White;
    public static eDirectionType ReverseDirection(eDirectionType direction)
    {
        eDirectionType res = eDirectionType.None;

        switch (direction)
        {
            case eDirectionType.Left:
                res = eDirectionType.Right;
                break;
            case eDirectionType.Right:
                res = eDirectionType.Left;
                break;
            case eDirectionType.Top:
                res = eDirectionType.Under;
                break;
            case eDirectionType.Under:
                res = eDirectionType.Top;
                break;
        }

        Debug.Assert(res != eDirectionType.None, "fall throw eDirectionType...");

        return res;
    }
}
