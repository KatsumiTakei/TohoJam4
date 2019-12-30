using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionStarer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text = null;

    [SerializeField]
    bool PlayOnAwake = true;


    [SerializeField]
    GameObject [] resultUi = null;

    WaitForSeconds wait02 = new WaitForSeconds(0.2f);
    WaitForSeconds wait05 = new WaitForSeconds(0.5f);

    IEnumerator Start()
    {
        if (PlayOnAwake)
            yield return CoStartQuest();
    }

    public void Reset()
    {
        text.color = new Color(0.75f, 0.75f, 0.75f);
        text.rectTransform.localScale = Vector3.one;
        text.rectTransform.anchoredPosition = new Vector2(25, 25);
        text.fontSize = 76;
        text.text = "Ready";
    }

    private void OnEnable()
    {
        EventManager.OnGameResult += OnGameResult;
    }

    private void OnDisable()
    {
        EventManager.OnGameResult -= OnGameResult;
    }

    public void OnClickStartQuest()
    {
        StartCoroutine(CoStartQuest());
    }

    IEnumerator CoStartQuest()
    {

        Reset();

        yield return wait02;

        text.text += ".";

        yield return wait02;

        text.text += ".";

        yield return wait02;

        text.text += ".";

        yield return wait02;

        text.DOColor(Color.clear, 0.5f);
        text.rectTransform.DOScale(1.5f, 0.5f);
        text.rectTransform.anchoredPosition = new Vector2(125, 25);
        text.text = "Go!";

        yield return wait05;

        EventManager.BroadcastChangeQuestion();
        print("Go!");
    }

    void OnGameResult(bool result)
    {
        Reset();
        OnClickResultButton(true);
        if (result)
        {
            text.color = Color.yellow;
            text.text = "Clear!";
        }
        else
        {
            text.text = "Game over...";
            text.fontSize = 48;
        }

    }

    public void OnClickResultButton(bool active)
    {
        foreach(var ui in resultUi)
        {
            ui.SetActive(active);
        }
        if (!active)
            text.color = Color.clear;
    }
}
