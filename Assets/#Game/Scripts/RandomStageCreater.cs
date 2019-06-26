
using UnityEngine;

public class RandomStageCreater : MonoBehaviour
{
    public void OnClickCreateRandomMap()
    {
        QuestionData data = new QuestionData();

        for (int i = 0; i < 50; i++)
        {
            data.impl.Add(CreateQuestData());

        }
        JsonManager.ToJson(Constant.MainData, data);
    }

    QuestionData.ImplQuestionData CreateQuestData()
    {
        var data = new QuestionData.ImplQuestionData(eInputType.Any, eTileType.None, eDirectionType.None, ePlacementType.Any);
        data.tile = (eTileType)Random.Range(1, 2 + 1);
        data.input = (eInputType)Random.Range(0, 2 + 1);
        data.placement = (ePlacementType)Random.Range(1, 7 + 1);
        data.direction = CheckPlacement(data.placement);

        return data;
    }

    eDirectionType CheckPlacement(ePlacementType placementType)
    {
        eDirectionType type = eDirectionType.None;
        switch (placementType)
        {
            case ePlacementType.Any:
                type = eDirectionType.None;
                break;
            case ePlacementType.Top:
                type = eDirectionType.None;
                break;
            case ePlacementType.Bottom:
                type = eDirectionType.None;
                break;
            case ePlacementType.Right:
                type = eDirectionType.None;
                break;
            case ePlacementType.Left:
                type = eDirectionType.None;
                break;
            case ePlacementType.Vertical:
                type = (eDirectionType)Random.Range(1, 2 + 1);
                break;
            case ePlacementType.Horizon:
                type = (eDirectionType)Random.Range(3, 4 + 1);
                break;
            case ePlacementType.All:
                type = (eDirectionType)Random.Range(1, 4 + 1);
                break;
        }

        return type;

    }

}
