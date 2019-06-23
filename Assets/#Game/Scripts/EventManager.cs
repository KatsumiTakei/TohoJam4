using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    public static Action<float> OnChangeTime = null;
    public static void BroadcastChangeTime(float currentTime) => OnChangeTime?.Invoke(currentTime);

    public static Action<eInputType, eTileType, eDirectionType> OnCheckAnswer = null;
    public static void BroadcastCheckAnswer(eInputType inputType, eTileType tileType, eDirectionType directionType) => OnCheckAnswer?.Invoke(inputType, tileType, directionType);

    public static Action<eInputType> OnMultipleInput = null;
    public static void BroadcastMultipleInput(eInputType inputType) => OnMultipleInput?.Invoke(inputType);

    public static Action OnChangeQuestion = null;
    public static void BroadcastChangeQuestion() => OnChangeQuestion?.Invoke();

    public static Action OnTimeup = null;
    public static void BroadcastTimeup() => OnTimeup?.Invoke();

    //public static Action OnStartWave = null;
    //public static void BroadcastStartWave() => OnStartWave?.Invoke();


    public static Action OnCorrectAnswer = null;
    public static void BroadcastCorrectAnswer() => OnCorrectAnswer?.Invoke();


    public static Action OnMissAnswer = null;
    public static void BroadcastMissAnswer() => OnMissAnswer?.Invoke();

    public static Action<bool> OnGameResult = null;
    public static void BroadcastGameResult(bool isResult) => OnGameResult?.Invoke(isResult);


}
