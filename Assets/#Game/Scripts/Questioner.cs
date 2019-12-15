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
        Debug.Log($"Answer is : { input }:{ tile }:{ direction }");

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
        var dataImpl = ProgressManager.Instance.GetQuestionData();
        DrawQuest(dataImpl.input, dataImpl.tile, dataImpl.direction);
        TilesManager.Instance.PlacementTile(dataImpl.placement);
        Debug.Log($"Question is : { dataImpl.input }:{ dataImpl.tile }:{ dataImpl.direction }");
    }

    bool Answer(eInputType input, eTileType tile, eDirectionType direction)
    {
        return ProgressManager.Instance.IsCheckAllSame(input, tile, direction);
    }

    void DrawQuest(eInputType input, eTileType tile, eDirectionType direction)
    {
        view.DrawQuest(FunctionLibrary.ReverseTile(tile), FunctionLibrary.ReverseInput(input), FunctionLibrary.ReverseDirection(direction));
    }
}
