using UnityEngine;

public static class FunctionLibrary
{
    public static eInputType ReverseInput(eInputType input)
    {
        switch (input)
        {
            case eInputType.ClickLeft:
                input = eInputType.ClickRight;
                break;

            case eInputType.ClickRight:
                input = eInputType.ClickLeft;
                break;

            case eInputType.Any:
                input = eInputType.Any;
                break;
        }

        return input;
    }
    public static eTileType ReverseTile(eTileType tile)
    {
        switch (tile)
        {
            case eTileType.None:
                tile = eTileType.None;
                break;
            case eTileType.White:
                tile = eTileType.Black;
                break;
            case eTileType.Black:
                tile = eTileType.White;
                break;
        }

        return tile;
    }
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
            case eDirectionType.None:
                Debug.Log("fall throw eDirectionType...");
                break;
        }


        return res;
    }
}
