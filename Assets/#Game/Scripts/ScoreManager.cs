using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    double currentTime = 0;

    ScoreCounterView view = null;
    public const int LimitScore = 999999999;

    public void Reset()
    {
        score = 0;
        view.AnimChangeScore(0, score);
    }

    private void Start()
    {
        view = new ScoreCounterView(GetComponent<TMPro.TextMeshProUGUI>(), 0.5f, 10f);
    }

    private void OnEnable()
    {
        EventManager.OnGameResult += OnGameResult;
        EventManager.OnMissAnswer += OnMissAnswer;
        EventManager.OnCorrectAnswer += OnCorrectAnswer;
        EventManager.OnChangeTime += OnChangeTime;
    }

    private void OnDisable()
    {
        EventManager.OnGameResult -= OnGameResult;
        EventManager.OnMissAnswer -= OnMissAnswer;
        EventManager.OnCorrectAnswer -= OnCorrectAnswer;
        EventManager.OnChangeTime -= OnChangeTime;
    }

    void OnGameResult(bool isResult)
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
    }

    void OnChangeTime(float currentTime)
    {
        this.currentTime = currentTime;
    }

    void OnCorrectAnswer()
    {
        var addScore = (int)System.Math.Floor(1000 * currentTime);
        view.AnimChangeScore(addScore, score);
        score = Mathf.Min(score + addScore, LimitScore);
    }

    void OnMissAnswer()
    {
        var addScore = -1000;
        view.AnimChangeScore(addScore, score);
        score = Mathf.Max(score + addScore, 0);
    }

}
