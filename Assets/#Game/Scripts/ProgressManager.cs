
using System.Collections;
/// <summary>
/// ProgressManager
/// </summary>
public class ProgressManager : UnityDLL.SingletonMonoBehaviour<ProgressManager>
{

    int missCounter = 0;

    public QuestionData Data { get; set; } = new QuestionData();
    public int QuestionIndex { get; private set; } = 0;
    public bool IsFinished { get; private set; } = false;

    public void Reset()
    {
        missCounter = 0;
        QuestionIndex = 0;
    }

    private void OnEnable()
    {
        EventManager.OnChangeQuestion += OnChangeQuestion;
        EventManager.OnCorrectAnswer += OnCorrectAnswer;
        EventManager.OnMissAnswer += OnMissAnswer;
    }

    private void OnDisable()
    {
        EventManager.OnChangeQuestion -= OnChangeQuestion;
        EventManager.OnCorrectAnswer -= OnCorrectAnswer;
        EventManager.OnMissAnswer -= OnMissAnswer;
    }

    void OnChangeQuestion()
    {
        IsFinished = false;
    }

    void OnCorrectAnswer()
    {
        QuestionIndex++;
        if (QuestionIndex < Data.GetQuestMax())
            AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._right);
        NextQuest();
    }

    void OnMissAnswer()
    {
        missCounter++;

        switch (missCounter)
        {
            case 1:
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._miss);
                NextQuest();
                break;
            case 2:
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._Pinch);
                NextQuest();
                break;
            case 3:
                StartCoroutine(CoGameResult(false));
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._gameover);
                IsFinished = true;
                break;
        }

    }

    void NextQuest()
    {
        if (QuestionIndex >= Data.GetQuestMax())
        {
            StartCoroutine(CoGameResult(true));
            AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._Result);
            IsFinished = true;
        }
        else
        {
            EventManager.BroadcastChangeQuestion();
        }
    }

    IEnumerator CoGameResult(bool isResult)
    {
        yield return new UnityEngine.WaitForSeconds(0.1f);

        EventManager.BroadcastGameResult(isResult);

    }

}
