using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    TextMeshProUGUI text = null;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        EventManager.OnChangeQuestion += OnChangeQuestion;
        EventManager.OnChangeTime += OnChangeTime;
        EventManager.OnTimeup += OnTimeup;
    }

    void OnDisable()
    {
        EventManager.OnChangeQuestion -= OnChangeQuestion;
        EventManager.OnChangeTime -= OnChangeTime;
        EventManager.OnTimeup -= OnTimeup;
    }

    public void Reset(float currentTime)
    {
        OnChangeTime(currentTime);
        text.color = new Color(0.75f, 0.75f, 0.75f);
    }

    void OnChangeQuestion()
    {
        Reset(5.0f);
        text.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f), 0.5f).OnComplete(() => text.rectTransform.localScale = Vector3.one);
    }

    void OnChangeTime(float currentTime)
    {
        text.text = ConvertSpecifiedFormat(currentTime);
        ChangeColorText(currentTime);
    }

    void OnTimeup()
    {
        text.text = "00:00";
    }

    void ChangeColorText(float currentTime)
    {
        int second = Mathf.FloorToInt(currentTime);
        if (second == 2)
        {
            text.color = Color.yellow;
        }
        if (second == 1)
        {
            text.color = Color.red;
        }
    }

    string ConvertSpecifiedFormat(float currentTime)
    {
        float few = currentTime % 1.0f;
        int second = Mathf.FloorToInt(currentTime);

        return $"{second:00}:{few.ToString("f2").Replace("0.", "")}";
    }

}
