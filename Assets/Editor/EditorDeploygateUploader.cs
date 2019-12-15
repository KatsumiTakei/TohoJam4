using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using UnityEditor.Build.Reporting;

public class EditorDeploygateUploader : EditorWindow, IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
    public class Param : ScriptableSingleton<Param>
    {
        public IEnumerator opFunc = null;   //保存されないのでここにある意味は特にない
        public int state = 0;
        public string path;
        public string productName = null;
        public Texture2D[] icons = null;
    }

    [MenuItem("Tools/Deploygate/Open")]
    static void OnMenuOpen()
    {
        var window = EditorWindow.GetWindow<EditorDeploygateUploader>(nameof(EditorDeploygateUploader));
        window.ShowPopup();
    }

    private void OnGUI()
    {
        using (var verticalScope = new EditorGUILayout.VerticalScope("Common"))
        {
            EditorGUILayout.LabelField("AccountName");
            var prevAccountName = EditorPrefs.GetString("DeploygateAccountName", "");
            var accountName = EditorGUILayout.TextField(prevAccountName);
            if (prevAccountName != accountName)
                EditorPrefs.SetString("DeploygateAccountName", accountName);

            EditorGUILayout.LabelField("ApiKey");
            var prevKey = EditorPrefs.GetString("DeploygateApiKey", "");
            var key = EditorGUILayout.TextField(prevKey);
            if (prevKey != key)
                EditorPrefs.SetString("DeploygateApiKey", key);

            EditorGUILayout.LabelField("use send deploygate");
            var sendKey = EditorPrefs.GetBool("DeploygateSendKey", true);
            var isUse = EditorGUILayout.Toggle(sendKey);
            if (sendKey != isUse)
                EditorPrefs.SetBool("DeploygateSendKey", isUse);
        }
    }


    [DidReloadScripts]
    private static void OnReloadScripts()
    {
        if (ScriptableSingleton<Param>.instance.state == 0)
            return;

        Debug.Log("OnReloadScripts");
        EditorApplication.update += OnUpdate;
        ScriptableSingleton<Param>.instance.opFunc = OpUpload(ScriptableSingleton<Param>.instance.path);
    }

    private static void OnUpdate()
    {
        if (ScriptableSingleton<Param>.instance.opFunc == null)
            return;
        if (ScriptableSingleton<Param>.instance.opFunc.MoveNext() == false)
        {
            ScriptableSingleton<Param>.instance.state = 0;
            ScriptableSingleton<Param>.instance.opFunc = null;
            EditorApplication.update -= OnUpdate;
            Debug.Log("End Update");
        }
    }

    private static void Upload(string path)
    {
        ScriptableSingleton<Param>.instance.state = 1;
        ScriptableSingleton<Param>.instance.path = path;
        OnReloadScripts();
    }

    private static IEnumerator OpUpload(string path)
    {
        if (!EditorPrefs.GetBool("DeploygateSendKey", true))
            yield break;

        var accountName = EditorPrefs.GetString("DeploygateAccountName", "");
        var apikey = EditorPrefs.GetString("DeploygateApiKey", "");
        if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(apikey))
        {
            Debug.Log("Deploygate NoSetting!");
            yield break;
        }

        Debug.Log("Begin Upload");
        var bytes = File.ReadAllBytes(path);
        var form = new WWWForm();
        var fileName = Path.GetFileName(path); //"dummy.apk";
        form.AddBinaryData("file", bytes, fileName, "application/octet-stream");

        var url = string.Format("https://deploygate.com/api/users/{0}/apps", accountName);

        var getData = new Dictionary<string, string>();
        getData["token"] = apikey;
        getData["message"] = PlayerSettings.productName + DateTime.Now.ToString("yyyyMMddHHmmss");
        var strFormData = "?";
        foreach (var pair in getData)
        {
            strFormData += pair.Key + "=" + pair.Value + "&";
        }
        var request = UnityWebRequest.Post(url + strFormData, form);
        request.SendWebRequest();
        while (!request.isDone)
        {
            Debug.Log("send web request...");
            yield return null;
        }

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log(request.error);
        }
        var responseJson = request.downloadHandler.text;
        Debug.Log(responseJson);
        
        request.Dispose();
        Debug.Log("End Upload");
        PlayFinishSE();

        yield break;
    }

    static void PlayFinishSE()
    {
        var clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Editor/Go!.wav");

        if (!clip)
        {
            Debug.LogError($"not found {clip.name}");
            return;
        }
        Debug.Log($"found {clip.name}");

        var unityEditorAssembly = typeof(AudioImporter).Assembly;
        var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        var instance = System.Activator.CreateInstance(audioUtilClass);

        var method = audioUtilClass.GetMethod("PlayClip");
        method.Invoke(instance, new object[] { clip, 0, false });
    }

    int IOrderedCallback.callbackOrder { get { return 1000; } }
    void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("IPreprocessBuildWithReport.OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);

        ScriptableSingleton<Param>.instance.productName = PlayerSettings.productName;
        ScriptableSingleton<Param>.instance.icons = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown);
        //PlayerSettings.productName = "dep_test";
        var icons = new Texture2D[ScriptableSingleton<Param>.instance.icons.Length];
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
    }

    void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report)
    {
        Debug.Log("IPostprocessBuildWithReport.OnPostprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
        Upload(report.summary.outputPath);

        PlayerSettings.productName = ScriptableSingleton<Param>.instance.productName;
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, ScriptableSingleton<Param>.instance.icons);
    }
}