using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// 自動ビルドを行うクラス
/// </summary>
public class BuildClass
{

    public static void Build()
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
        p.StartInfo.Arguments = @"rev-parse --short HEAD";

        //起動
        p.Start();

        //出力を読み取る
        string results = p.StandardOutput.ReadToEnd();

        //改行を抜く
        results = results.Replace("\r", "").Replace("\n", "");

        //終了
        p.WaitForExit();
        p.Close();

        // プラットフォーム設定
        BuildTarget platform = BuildTarget.StandaloneWindows64;

        // 出力ファイル名及びパス
        string outputfile = "exe-file/" + Application.productName + ".exe";

        // ターゲットプラットフォーム
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, platform);

        // ビルド対象シーンリスト
        List<string> allScene = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                allScene.Add(scene.path);
            }
        }

        //PlayerSettingsの変更
        PlayerSettings.SplashScreen.showUnityLogo = false;
        PlayerSettings.SplashScreen.show = false;
        PlayerSettings.bundleVersion = results;

        // 実行
        var errorMessage = BuildPipeline.BuildPlayer(
                allScene.ToArray(),
                outputfile,
                platform,
                BuildOptions.None
        );
    }
}