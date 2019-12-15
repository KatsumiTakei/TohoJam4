using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using HtmlAgilityPack;
using UnityEngine.Networking;

public class VersionChecker : MonoBehaviour
{
    void Start()
    {
        CurrentVersionCheck();

        if (LoadInvalidVersionUpCheck())
        {
            return;
        }
#if UNITY_IOS
                StartCoroutine(VersionCheckIOS());
#elif UNITY_ANDROID
        StartCoroutine(VersionCheckAndroid());
#endif
    }

    const string INVALID_VERSION_UP_CHECK_KEY = "InvalidVersionUpCheck";

    bool LoadInvalidVersionUpCheck()
    {
        return PlayerPrefs.GetInt(INVALID_VERSION_UP_CHECK_KEY, 0) != 0;
    }

    void SaveInvalidVersionUpCheck(bool invalid)
    {
        PlayerPrefs.SetInt(INVALID_VERSION_UP_CHECK_KEY, invalid ? 1 : 0);
    }

    const string CURRENT_VERSION_CHECK_KEY = "CurrentVersionCheck";

    void CurrentVersionCheck()
    {
        var version = PlayerPrefs.GetString(CURRENT_VERSION_CHECK_KEY, "");
        if (version != Application.version)
        {
            PlayerPrefs.SetString(CURRENT_VERSION_CHECK_KEY, Application.version);
            SaveInvalidVersionUpCheck(false);
        }
    }

    IEnumerator VersionCheckIOS()
    {
        var url = string.Format("https://itunes.apple.com/lookup?bundleId={0}", Application.identifier);
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.timeout = 60;
        yield return request.SendWebRequest();

        while (!request.isDone)
        {
            Debug.Log("request allpath");
        }

        if (string.IsNullOrEmpty(request.error) && !string.IsNullOrEmpty(request.downloadHandler.text))
        {
            var lookupData = JsonUtility.FromJson<AppLookupData>(request.downloadHandler.text);
            if (lookupData.resultCount > 0 && lookupData.results.Length > 0)
            {
                var result = lookupData.results[0];
                if (VersionComparative(result.version))
                {
                    ShowUpdatePopup(result.trackViewUrl);
                }
            }
        }
    }

    IEnumerator VersionCheckAndroid()
    {
        var url = string.Format("https://play.google.com/store/apps/details?id={0}", Application.identifier);

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.timeout = 60;
        yield return request.SendWebRequest();

        while (!request.isDone)
        {
            Debug.Log("request allpath");
        }

        if (string.IsNullOrEmpty(request.error) && !string.IsNullOrEmpty(request.downloadHandler.text))
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(request.downloadHandler.text);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@itemprop=\"softwareVersion\"]");
            if (node != null)
            {
                if (VersionComparative(node.InnerText))
                {
                    ShowUpdatePopup(url);
                }
            }
        }
    }

    bool VersionComparative(string storeVersionText)
    {
        if (string.IsNullOrEmpty(storeVersionText))
        {
            return false;
        }
        try
        {
            var storeVersion = new System.Version(storeVersionText);
            var currentVersion = new System.Version(Application.version);

            if (storeVersion.CompareTo(currentVersion) > 0)
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("{0} VersionComparative Exception caught.", e);
        }

        return false;
    }

    void ShowUpdatePopup(string url)
    {
        if (string.IsNullOrEmpty(url))
            return;

#if !UNITY_EDITOR
        string title = string.Empty;
        string message = string.Empty;

        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            title = "アプリの更新があります";
            message = "更新しますか？";

        }
        else
        {
            title = "There is an update of the application";
            message = "Do you want to update the application?";
        }
        ModalPanel.Instance().MessageBox(null, title, message, () => Application.OpenURL(url), () => SaveInvalidVersionUpCheck(true), null, null, false, "YesNo");
#endif
    }

}


[Serializable]
public class AppLookupData
{
    public int resultCount;
    public AppLookupResult[] results;

}

[Serializable]
public class AppLookupResult
{
    public string version;
    public string trackViewUrl;
}