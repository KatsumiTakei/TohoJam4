using System.Collections;
using UnityDLL;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData
{

    public List<ImplQuestionData> impl = new List<ImplQuestionData>();


    [System.Serializable]
    public class ImplQuestionData
    {
        public eInputType input =  eInputType.Any;
        public eTileType tile = eTileType.None;
        public eDirectionType direction = eDirectionType.None;
        public ePlacementType placement = ePlacementType.Top;

        public ImplQuestionData(eInputType input, eTileType tile, eDirectionType direction, ePlacementType placement)
        {
            this.tile = tile;
            this.input = input;
            this.direction = direction;
            this.placement = placement;
        }
    }

    public void ToJson(string filename)
    {
        JsonManager.ToJson(filename, this);
    }

    public bool IsCheckAllSame(ImplQuestionData data, int index)
    {
        bool res = (data.direction == impl[index].direction || impl[index].direction == eDirectionType.None)
            && (data.input == impl[index].input || impl[index].input == eInputType.Any)
            && (data.tile == impl[index].tile || impl[index].tile == eTileType.None);

        return res;
    }

    public int GetQuestMax()
    {
        return impl.Count;
    }
}
