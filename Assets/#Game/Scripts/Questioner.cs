using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questioner : MonoBehaviour
{
    int currentLevel = 0;
    int correctAnswerCounter = 0;
    int questionsNumCounter = 0;
    int missCounter = 0;

    List<List<Func<eInputType, eTileType, eDirectionType, bool>>> questions = new List<List<Func<eInputType, eTileType, eDirectionType, bool>>>()
    {
        new List<Func<eInputType, eTileType, eDirectionType, bool>>()
        {// Level1
            QWhite,
            QBlack,
        },
        new List<Func<eInputType, eTileType, eDirectionType, bool>>()
        {// Level2
        },
        new List<Func<eInputType, eTileType, eDirectionType, bool>>()
        {// Level3
        },
    };

    void OnEnable()
    {
        EventManager.OnCheckAnswer += OnCheckAnswer;
    }

    void OnDisable()
    {
        EventManager.OnCheckAnswer -= OnCheckAnswer;
    }

    void OnCheckAnswer(eInputType input, eTileType tile, eDirectionType direction)
    {
        print(input);
    }
    void OnChangeQuestion()
    {

    }
    #region     Questions

    static bool QWhite(eInputType input, eTileType tile, eDirectionType direction)
    {
        return true;
    }

    static bool QBlack(eInputType input, eTileType tile, eDirectionType direction)
    {
        return true;
    }

    #endregion  Questions
}
