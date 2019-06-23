using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questioner : MonoBehaviour
{
    QuestionView view = null;

    private void Start()
    {
        view = GetComponent<QuestionView>();

        //var exportData = new QuestionData();
        //exportData.impl.Add(new QuestionData.ImplQuestionData(eInputType.Any, eTileType.White, eDirectionType.None));
        //JsonManager.ToJson(Constant.TutorialData, exportData);

        ProgressManager.Instance.Data = JsonManager.FromJson<QuestionData>(Constant.TutorialData);
    }

    void OnEnable()
    {
        EventManager.OnCheckAnswer += OnCheckAnswer;
        EventManager.OnChangeQuestion += OnChangeQuestion;
    }

    void OnDisable()
    {
        EventManager.OnCheckAnswer -= OnCheckAnswer;
        EventManager.OnChangeQuestion -= OnChangeQuestion;
    }

    void OnCheckAnswer(eInputType input, eTileType tile, eDirectionType direction)
    {
        print($"Answer is : { input }:{ tile }:{ direction }");

        bool isSame = Answer(input, tile, direction);
        if(isSame)
        {
            EventManager.BroadcastCorrectAnswer();
        }
        else
        {
            EventManager.BroadcastMissAnswer();
        }
    }

    void OnChangeQuestion()
    {
        var dataImpl = ProgressManager.Instance.Data.impl[ProgressManager.Instance.QuestionIndex];
        DrawQuest(dataImpl.input, dataImpl.tile, dataImpl.direction);
        TilesManager.Instance.PlacementTile(dataImpl.placement);
        print($"Question is : { dataImpl.input }:{ dataImpl.tile }:{ dataImpl.direction }");
    }

    bool Answer(eInputType input, eTileType tile, eDirectionType direction)
    {
        return ProgressManager.Instance.Data.IsCheckAllSame(new QuestionData.ImplQuestionData(input, tile, direction, ePlacementType.Any), ProgressManager.Instance.QuestionIndex);
    }

    void DrawQuest(eInputType input, eTileType tile, eDirectionType direction)
    {
        view.DrawQuest(FunctionLibrary.ReverseTile(tile), FunctionLibrary.ReverseInput(input), FunctionLibrary.ReverseDirection(direction));
    }
}
