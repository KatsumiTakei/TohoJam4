using System.Collections;
using System.IO;
using UnityEngine;

public static class Screenshot
{
    static readonly string CaptutreFileName = "/Temp.png";

    public static IEnumerator CoWriteFileProcess()
    {
        yield return CoCaptureScreenshotProcess();
        yield return CoMediaDirWriteFileProcess();
    }

    static IEnumerator CoCaptureScreenshotProcess()
    {
        Debug.Log("CaptureScreenshotProcess");
        yield return new WaitForEndOfFrame();
        string path = null;

#if UNITY_EDITOR
        path = CaptutreFileName;
#elif UNITY_ANDROID
        path = Application.persistentDataPath + "/" + CaptutreFileName;
#endif

        Debug.Log("BeginCaptureScreenshot:" + path);
        ScreenCapture.CaptureScreenshot(CaptutreFileName);
        Debug.Log("AfterCaptureScreenshot:" + path);

        yield return CoCheckExistFile(path);

        Debug.Log("CaptureOK:" + path);
        ScanFile(path, null);
    }

    static IEnumerator CoMediaDirWriteFileProcess()
    {
        Debug.Log("MediaDirWriteFileProcess");
        if (Application.platform != RuntimePlatform.Android)
            yield return null;

#if UNITY_ANDROID
        var path = Application.persistentDataPath + "/" + CaptutreFileName;
        yield return CoCheckExistFile(path);

        // 保存パスを取得
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
        using (AndroidJavaObject joPublicDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", jcEnvironment.GetStatic<string>("DIRECTORY_PICTURES"/*"DIRECTORY_DCIM"*/ )))
        {
            var pngBytes = File.ReadAllBytes(path);

            var outputPath = joPublicDir.Call<string>("toString") + "/Screenshots/" + CaptutreFileName;
            Debug.Log("MediaDir:" + outputPath);

            File.WriteAllBytes(outputPath, pngBytes);
            yield return CoCheckExistFile(outputPath);

            Debug.Log("MediaDirWriteFileOK:" + outputPath);
            ScanFile(outputPath, null);
        }
#endif
    }

    static IEnumerator CoCheckExistFile(string path)
    {
        while (!File.Exists(path))
        {
            Debug.Log("NoFile:" + path);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }


    /// <summary>
    /// インデックス情報にファイル名を登録する
    /// これをしないとPC から内部ストレージを参照した時にファイルが見えない
    /// </summary>
    /// <param name="path"></param>
    /// <param name="mimeType"></param>
    static void ScanFile(string path, string mimeType)
    {
#if UNITY_ANDROID
        using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
        using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass("android.media.MediaScannerConnection"))
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
        using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
        {
            Debug.Log("scanFile:" + path);
            var mimeTypes = (mimeType != null) ? new string[] { mimeType } : null;
            jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] { path }, mimeTypes, null);
        }
        Handheld.StopActivityIndicator();
#endif
    }

}
