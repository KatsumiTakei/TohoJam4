using System;
using UniRx;
using UnityEngine;

public class ShortTimer : MonoBehaviour
{
    TimeKeeper timeKeeper = new TimeKeeper();

    void OnEnable()
    {
        EventManager.OnChangeQuestion += OnChangeQuestion;
        EventManager.OnGameResult += OnGameResult;
    }

    void OnDisable()
    {
        EventManager.OnChangeQuestion -= OnChangeQuestion;
        EventManager.OnGameResult -= OnGameResult;
        timeKeeper.Dispose();
    }

    void Update()
    {
        timeKeeper.ManualUpdate();
    }

    void OnGameResult(bool result)
    {
        timeKeeper.StopTimer();
    }

    void OnChangeQuestion()
    {
        timeKeeper.RestartTimer();
    }

}

public class TimeKeeper : IDisposable
{
    public bool IsRunning { get; set; } = false;
    float timeLimit = 5f;

    public TimeKeeper()
    {
        IsRunning = false;
        timeLimit = 5f;
    }

    public void Dispose()
    {
    }

    public void StopTimer()
    {
        ProcessTimer.Stop();
        IsRunning = false;
    }

    public void RestartTimer()
    {
        IsRunning = true;
        ProcessTimer.Restart();
    }

    public void ManualUpdate()
    {
        if (!IsRunning)
            return;

        float currentTime = timeLimit - ProcessTimer.TotalSeconds;
        EventManager.BroadcastChangeTime(currentTime);

        if (currentTime <= 0f)
        {
            EventManager.BroadcastCheckAnswer(eInputType.Any, eTileType.None, eDirectionType.None);
            EventManager.BroadcastTimeup();
        }
    }

}