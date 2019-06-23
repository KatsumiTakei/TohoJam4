﻿using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultView : MonoBehaviour
{
    [SerializeField]
    Image heart = null;

    [SerializeField]
    TextMeshProUGUI totalQuset = null;

    public void Reset()
    {
        totalQuset.text = $"Quest\n{ProgressManager.Instance.QuestionIndex} / {ProgressManager.Instance.Data.GetQuestMax()}";
        heart.rectTransform.DOSizeDelta(new Vector2(48, 16), 0.5f);
    }

    private void OnEnable()
    {
        EventManager.OnChangeQuestion += OnChangeQuestion;
        EventManager.OnGameResult += OnGameResult;
        EventManager.OnMissAnswer += OnMissAnswer;
    }

    private void OnDisable()
    {
        EventManager.OnChangeQuestion -= OnChangeQuestion;
        EventManager.OnGameResult -= OnGameResult;
        EventManager.OnMissAnswer -= OnMissAnswer;
    }

    void OnGameResult(bool res)
    {
        totalQuset.text = $"Quest\n{ProgressManager.Instance.QuestionIndex} / {ProgressManager.Instance.Data.GetQuestMax()}";
    }

    void OnChangeQuestion()
    {
        totalQuset.text = $"Quest\n{ProgressManager.Instance.QuestionIndex} / {ProgressManager.Instance.Data.GetQuestMax()}";
        totalQuset.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f), 0.5f).OnComplete(() => totalQuset.rectTransform.localScale = Vector3.one);
    }

    void OnMissAnswer()
    {
        heart.rectTransform.DOSizeDelta(heart.rectTransform.sizeDelta - new Vector2(16, 0), 0.5f);
    }

}
