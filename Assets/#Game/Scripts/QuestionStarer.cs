using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionStarer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text = null;


    WaitForSeconds wait02 = new WaitForSeconds(0.2f);
    WaitForSeconds wait05 = new WaitForSeconds(0.5f);

    //public IEnumerator Start()
    //{
    //    yield return OnClickStartQuest();
    //}

    public void Reset()
    {
        text.color = new Color(0.75f, 0.75f, 0.75f);
        text.rectTransform.localScale = new Vector3(1, 1, 1);
        text.rectTransform.anchoredPosition = new Vector2(25, 25);
        text.fontSize = 96;
        text.text = "Ready";
    }

    private void OnEnable()
    {
        EventManager.OnGameResult += OnGameResult;
        //EventManager.OnChangeQuestion += OnChangeQuestion;
    }

    private void OnDisable()
    {
        EventManager.OnGameResult -= OnGameResult;
        //EventManager.OnChangeQuestion -= OnChangeQuestion;
    }

    //void OnChangeQuestion()
    //{
    //    Reset();

    //    text.text = "Correct!";
    //    text.color = Color.yellow;
    //    text.DOFade(0, 0.5f);
    //    text.rectTransform.DOScale(1.5f, 0.5f);
    //}

    public IEnumerator OnClickStartQuest()
    {

        Reset();

        yield return wait02;

        text.text += ".";

        yield return wait02;

        text.text += ".";

        yield return wait02;

        text.text += ".";

        yield return wait02;

        text.DOFade(0, 0.5f);
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
}
