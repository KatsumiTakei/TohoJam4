using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    public static Action<float> OnChangeTime = null;
    public static void BroadcastProgressTime(float currentTime) => OnChangeTime?.Invoke(currentTime);


    public static Action<eInputType, eTileType, eDirectionType> OnCheckAnswer = null;
    public static void BroadcastCheckAnswer(eInputType inputType, eTileType tileType, eDirectionType directionType) => OnCheckAnswer?.Invoke(inputType, tileType, directionType);

    public static Action<eInputType> OnMultipleInput = null;
    public static void BroadcastMultipleInput(eInputType inputType) => OnMultipleInput?.Invoke(inputType);

    public static Action OnStartQuestion = null;
    public static void BroadcastStartQuestion() => OnStartQuestion?.Invoke();

    public static Action OnTimeup = null;
    public static void BroadcastTiemup() => OnTimeup?.Invoke();

    //public static Action OnStartWave = null;
    //public static void BroadcastStartWave() => OnStartWave?.Invoke();




}
