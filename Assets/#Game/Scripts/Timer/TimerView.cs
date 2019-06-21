using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerView : MonoBehaviour
{
    TextMeshProUGUI text = null;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        EventManager.OnChangeTime += OnChangeTime;
        EventManager.OnTimeup += OnTimeup;
    }

    void OnDisable()
    {
        EventManager.OnChangeTime -= OnChangeTime;
        EventManager.OnTimeup -= OnTimeup;
    }

    public void Reset(float currentTime)
    {
        OnChangeTime(currentTime);
        text.color = Color.white;
    }

    void OnChangeTime(float currentTime)
    {
        text.text = ConvertSpecifiedFormat(currentTime);
    }

    void OnTimeup()
    {
        text.color = Color.red;
        text.text = "00:00";
    }

    string ConvertSpecifiedFormat(float currentTime)
    {
        float few = currentTime % 1.0f;
        int second = Mathf.FloorToInt(currentTime);

        return $"{second:00}:{few.ToString("f2").Replace("0.", "")}";
    }

}
