using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TilesManager : UnityDLL.SingletonMonoBehaviour<TilesManager>
{
    [field: RenameField(nameof(Tiles))]
    public GameObject[] Tiles = null;

    public void PlacementTile(ePlacementType placementType)
    {
        HideAll();
        switch (placementType)
        {
            case ePlacementType.Any:
                Debug.LogWarning("fall throw ePlacementType Any");
                break;
            case ePlacementType.Random:
                PlacementTile((ePlacementType)Random.Range(1, 7));
                break;
            case ePlacementType.Top:
                Active(new bool[] { true, false, false, false });
                break;
            case ePlacementType.Bottom:
                Active(new bool[] { false, true, false, false });
                break;
            case ePlacementType.Right:
                Active(new bool[] { false, false, true, false });
                break;
            case ePlacementType.Left:
                Active(new bool[] { false, false, false, true });
                break;
            case ePlacementType.Vertical:
                Active(new bool[] { true, true, false, false });
                break;
            case ePlacementType.Horizon:
                Active(new bool[] { false, false, true, true });
                break;
            case ePlacementType.All:
                Active(new bool[] { true, true, true, true });
                break;
        }
    }

    void Active(bool []actives)
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i].SetActive(actives[i]);
        }
    }

    public void HideAll()
    {
        for (int i = 0;i < Tiles.Length; i++)
        {
            Tiles[i].SetActive(false);
        }
    }

}

public enum ePlacementType
{ 
    Any,

    Top,
    Bottom,
    Right,
    Left,

    Vertical,
    Horizon,

    All,

    Random = 99,

}

