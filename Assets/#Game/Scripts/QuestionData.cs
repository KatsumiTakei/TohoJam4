using System.Collections;
using UnityDLL;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData
{
    //  Jsonのシリアライズのため[SerializeField]
    [SerializeField]
    List<ImplQuestionData> impl = new List<ImplQuestionData>();

    [System.Serializable]
    public class ImplQuestionData
    {
        public eInputType input = eInputType.Any;
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

    public int GetQuestMax()
    {
        return impl.Count;
    }
    public ImplQuestionData GetQuestionData(int index)
    {
        return impl[index];
    }

    public void AddQuestionData(ImplQuestionData questionData)
    {
        impl.Add(questionData);
    }

    public bool IsCheckAllSame(ImplQuestionData data, int index)
    {
        bool res = (data.direction == impl[index].direction || impl[index].direction == eDirectionType.None)
            && (data.input == impl[index].input || impl[index].input == eInputType.Any)
            && (data.tile == impl[index].tile || impl[index].tile == eTileType.None);

        return res;
    }
}
