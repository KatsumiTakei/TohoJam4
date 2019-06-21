using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShortTimer : MonoBehaviour
{
    TimeKeeper timeKeeper = new TimeKeeper();

    void OnEnable()
    {
        EventManager.OnStartQuestion += OnStartQuestion;
    }
    void OnDisable()
    {
        EventManager.OnStartQuestion -= OnStartQuestion;
        timeKeeper.Dispose();
    }

    void OnStartQuestion()
    {
        timeKeeper.RestartTimer();
    }

    void Update()
    {
        timeKeeper.ManualUpdate();
    }

}

public class TimeKeeper : IDisposable
{
    public bool IsRunning { get; set; } = false;
    float timeLimit = 5f;

    public TimeKeeper()
    {
    }

    public void Dispose()
    {
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
        EventManager.BroadcastProgressTime(currentTime);

        if (currentTime <= 0f)
        {
            EventManager.BroadcastTiemup();
        }
    }

}