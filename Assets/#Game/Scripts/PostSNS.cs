using System.Collections;
using UnityEngine;

public class PostSNS : MonoBehaviour
{
    [SerializeField]
    string url = string.Empty;

    [SerializeField]
    string imagePath = string.Empty;

    static string message = string.Empty;

    private void Awake()
    {
        if (string.IsNullOrEmpty(url))
            url = $"#{Application.productName}";

        if (string.IsNullOrEmpty(imagePath))
            imagePath = $"{Application.persistentDataPath}/Temp.png";

    }

    public static void SetSnsMessage(string message)
    {
        PostSNS.message = message;
    }

    /// <summary>
    /// Twitter投稿
    /// </summary>
    public void OnPostTwitter()
    {
        StartCoroutine(TakeCapture());
    }

    IEnumerator TakeCapture()
    {
        yield return Screenshot.CoWriteFileProcess();
        SocialWorker.PostTwitter(message, url, imagePath, onResult);
        void onResult(string message)
        {
            Debug.Log(message);
            if(message.Equals("ERROR"))
            {// Twitterのクライアントが存在しないダイアログを出す

            }
        }
    }


}
