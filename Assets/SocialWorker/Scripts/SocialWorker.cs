/// <summary>
/// 外部アプリ投稿
/// 主にTwitterを利用
/// TwitterPlugin.javaを参照している
/// </summary>

using UnityEngine;
using System;

public class SocialWorker : MonoBehaviour
{
    #region     ネイティブプラグイン定義

#if UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern void postMail(string to, string cc, string bcc, string subject, string message, string imagePath);
        [DllImport("__Internal")]
        private static extern void postTwitterOrFacebook(bool isTwitter, string message, string url, string imagePath);
        [DllImport("__Internal")]
        private static extern void postLine(string message, string imagePath);
        [DllImport("__Internal")]
        private static extern void postInstagram(string imagePath);
        [DllImport("__Internal")]
        private static extern void createChooser(string message, string imagePath);
#elif UNITY_ANDROID
    private static AndroidJavaObject worker = null;
#endif
    #endregion //  ネイティブプラグイン定義

    static Action<string> onResult = null;// !< 結果コールバック

    void Awake()
    {
        Debug.Assert(name.Equals("SocialWorker"), "プラグイン側で固定の名前が必要なので名前をSocialWorkerに変更してください");

        DontDestroyOnLoad(gameObject);

#if !UNITY_EDITOR && UNITY_ANDROID
            worker = new AndroidJavaObject("TwitterPlugin");    //  !<  参照する外部プラグインのクラス名
            Debug.Log(worker);

#endif
    }

    /// <summary>
    /// url無しTweet
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="imagePath">画像パス(PNG/JPGのみ)。空文字の場合は処理されない。</param>
    /// <param name="onResult">結果コールバック</param>
    public static void PostTwitter(string message, string imagePath, Action<string> onResult = null)
    {
        PostTwitter(message, null, imagePath, onResult);
    }

    /// <summary>
    /// Tweet
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="url">URL。空文字の場合は処理されない。</param>
    /// <param name="imagePath">画像パス(PNG/JPGのみ)。空文字の場合は処理されない。</param>
    /// <param name="onResult">結果コールバック</param>
    public static void PostTwitter(string message, string url, string imagePath, Action<string> onResult = null)
    {
        if (message == null)
            message = string.Empty;

        if (url == null)
            url = string.Empty;

        if (imagePath == null)
            imagePath = string.Empty;

        SocialWorker.onResult = onResult;
#if UNITY_IPHONE
            postTwitterOrFacebook(true, message, url, imagePath);
#elif UNITY_ANDROID
        worker.Call("postTwitter", message, url, imagePath);
#endif
    }

    /// <summary>
    /// 結果コールバック。ネイティブプラグイン側から呼ばれるコールバック。
    /// </summary>
    /// <param name="message">結果値</param>
    public void OnSocialWorkerResult(string message)
    {
        onResult?.Invoke(message);
        onResult = null;
    }
    

    public void OnMessage(string message)
    {
        Debug.Log(message);
    }

    public void Test()
    {
        worker.Call("postTwitter", "message", $"#{Application.productName}", $"{Application.persistentDataPath}/Temp.png");
        SocialWorker.onResult = onResult;
        void onResult(string message)
        {
            Debug.Log(message);
        }
    }

}

