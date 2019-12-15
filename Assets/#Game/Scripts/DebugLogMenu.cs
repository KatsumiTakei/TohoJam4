using UnityEngine;
using UnityEngine.UI;

public class DebugLogMenu : MonoBehaviour
{
    private DebugGui debugGui = null;

    private void Awake()
    {
        Application.logMessageReceived += OnLogMessage;
        debugGui = GetComponent<DebugGui>();
    }


    private void OnDestroy()
    {
        Application.logMessageReceived -= OnLogMessage;
    }

    private void OnLogMessage(string i_logText, string i_stackTrace, LogType i_type)
    {
        if (string.IsNullOrEmpty(i_logText))
            return;

        switch (i_type)
        {
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                debugGui.LogError(i_logText + System.Environment.NewLine);
                break;
            case LogType.Warning:
                debugGui.LogWarning(i_logText + System.Environment.NewLine);
                break;
            default:
                debugGui.Log(i_logText + System.Environment.NewLine);
                break;
        }

    }

}