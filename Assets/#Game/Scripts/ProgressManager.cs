public class ProgressManager : UnityDLL.SingletonMonoBehaviour<ProgressManager>
{

    int missCounter = 0;

    int currentLevel = 0;

    public QuestionData Data { get; set; } = new QuestionData();
    public int QuestionIndex { get; set; } = 0;
    public bool IsFinished { get; set; } = false;


    public void Reset()
    {
        missCounter = 0;
        QuestionIndex = 0;
        currentLevel = 0;
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
        AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._right);
        QuestionIndex++;
        NextQuest();
    }

    void OnMissAnswer()
    {
        missCounter++;
        QuestionIndex++;
        NextQuest();

        switch (missCounter)
        {
            case 1:
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._miss);
                break;
            case 2:
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._Pinch);
                break;
            case 3:
                EventManager.BroadcastGameResult(false);
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._gameover);
                IsFinished = true;
                break;
        }

    }

    void NextQuest()
    {
        if (QuestionIndex >= Data.GetQuestMax())
        {
            EventManager.BroadcastGameResult(true);
            IsFinished = true;
        }
        else
        {
            EventManager.BroadcastChangeQuestion();
        }
    }
}
