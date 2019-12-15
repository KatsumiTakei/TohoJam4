
using System.Collections;
/// <summary>
/// ProgressManager
/// </summary>
public class ProgressManager : UnityDLL.SingletonMonoBehaviour<ProgressManager>
{
    [UnityEngine.SerializeField]
    TMPro.TextMeshProUGUI text = null;

    int missCounter = 0;
    int questionIndex = 0;
    QuestionData implData = new QuestionData();

    public bool IsFinished { get; private set; } = false;

    public void Reset()
    {
        missCounter = 0;
        questionIndex = 0;
    }

    private void Start()
    {
        text.canvasRenderer.SetAlpha(0f);
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
        questionIndex++;
        if (questionIndex < implData.GetQuestMax())
            AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._right);
        NextQuest();
    }

    void OnMissAnswer()
    {
        text.canvasRenderer.SetAlpha(1f);
        text.CrossFadeAlpha(0f, 0.5f, false);

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
        if (questionIndex >= implData.GetQuestMax())
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

    public void SetMissTextureAlpha(float alpha)
    {
        text.canvasRenderer.SetAlpha(alpha);
    }

    public string GetTotalQusetText()
    {
        return $"Quest\n{ questionIndex } / { implData.GetQuestMax()}";
    }

    public void SetQuestData(string setDataName)
    {
        implData = JsonManager.FromJson<QuestionData>(setDataName);
    }

    public bool IsCheckAllSame(eInputType input, eTileType tile, eDirectionType direction)
    {
        return implData.IsCheckAllSame(new QuestionData.ImplQuestionData(input, tile, direction, ePlacementType.Any), questionIndex);
    }

    public QuestionData.ImplQuestionData GetQuestionData()
    {
        return implData.GetQuestionData(questionIndex);
    }
}
