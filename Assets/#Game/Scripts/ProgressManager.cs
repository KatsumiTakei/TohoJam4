public class ProgressManager : UnityDLL.SingletonMonoBehaviour<ProgressManager>
{
    public QuestionData Data { get; set; } = new QuestionData();

    public int MissCounter { get; set; } = 0;

    public int CurrentLevel { get; set; } = 0;
    public int QuestionIndex { get; set; } = 0;

    public bool IsFinished { get; set; } = false;


    public void Reset()
    {
        MissCounter = 0;
        QuestionIndex = 0;
        CurrentLevel = 0;
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
        MissCounter++;

        switch (MissCounter)
        {
            case 1:
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._miss);
                break;
            case 2:
                AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._Pinch);
                break;
            case 3:
                EventManager.BroadcastGameResult(false);
                //AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._Pinch);
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
            //Observable
            //    .Timer(TimeSpan.FromSeconds(1))
            //    .Subscribe(ob => EventManager.BroadcastChangeQuestion());
            //IsFinished = true;
        }
    }
}
