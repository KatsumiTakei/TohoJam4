using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildVersionToGitCommitNum : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 900; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        // gitのコミット番号の確認
        System.Diagnostics.Process p = new System.Diagnostics.Process();

        //git.exeのフルパス
        p.StartInfo.FileName = @"C:\Program Files\Git\cmd\git.exe";

        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = false;
        p.StartInfo.CreateNoWindow = true;

        //gitで行うコマンドを設定
        p.StartInfo.Arguments = @"rev-parse HEAD";

        //起動
        p.Start();

        //出力を読み取る
        string results = p.StandardOutput.ReadToEnd();

        //改行を抜く
        var commitVersionStr = results.Replace("\r", "").Replace("\n", "");
        if (!commitVersionStr.Equals(PlayerSettings.bundleVersion))
            PlayerSettings.Android.bundleVersionCode = 0;

        PlayerSettings.bundleVersion = commitVersionStr;
        PlayerSettings.Android.bundleVersionCode += 1;

        GameObject target = GameObject.Find("CommitVersion");
        if (target)
        {
            target.GetComponent<TMPro.TextMeshProUGUI>().text = (EditorUserBuildSettings.development) ?
              "CommitHash : " + PlayerSettings.bundleVersion + "\n" + "BuildVersion  : " + PlayerSettings.Android.bundleVersionCode :
              string.Empty;
        }

        GameObject debugCanvas = GameObject.Find("DebugCanvas");
        if (debugCanvas)
            debugCanvas.SetActive(EditorUserBuildSettings.development);

        //終了
        p.WaitForExit();
        p.Close();

    }
}
