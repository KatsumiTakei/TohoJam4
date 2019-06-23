using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{

    [SerializeField]
    GameObject[] ui = null;


    bool[] startQuest = new bool[] { false, true };
    bool[] backHome = new bool[] { true, false };

    private void Start()
    {
        ToggleAll(backHome);
    }

    void Update()
    {
        InputManager.ManualUpdate();
    }

    public void OnClickTutorial()
    {
        ProgressManager.Instance.Data = JsonManager.FromJson<QuestionData>(Constant.TutorialData);
        ToggleAll(startQuest);
    }

    public void OnClickArcade()
    {
        ProgressManager.Instance.Data = JsonManager.FromJson<QuestionData>(Constant.MainData);
        ToggleAll(startQuest);
    }

    public void OnClickRetry()
    {
        ProgressManager.Instance.Reset();
    }

    public void OnClickBackHome()
    {
        ProgressManager.Instance.Reset();
        ToggleAll(backHome);
    }

    void ToggleAll(bool[] active)
    {
        for (int i = 0; i < active.Length; i++)
        {
            ui[i].gameObject.SetActive(active[i]);
        }
    }
}

public enum eDirectionType
{
    None,
    Top,
    Under,
    Right,
    Left,

}
