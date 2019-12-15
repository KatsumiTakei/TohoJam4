using System.Collections.Generic;
using UnityEngine;
using STLExtensiton;
using UnityEngine.Networking;
using System.IO;

public static class JsonManager
{
    static Dictionary<string, string> jsons = new Dictionary<string, string>();
    static bool isLoaded = false;

    public static T FromJson<T>(string fileName)
    {
        if (!isLoaded)
            Init();

        return JsonUtility.FromJson<T>(jsons[fileName]);
    }

    public static void ToJson<T>(string fileName, T parameter)
    {
        var data = JsonUtility.ToJson(parameter, true);
        jsons[fileName] = data;

#if UNITY_ANDROID && !UNITY_EDITOR
        var writePath = Application.persistentDataPath + "/Jsons/" + fileName + ".json";

#else
        var writePath = Application.streamingAssetsPath + "/Jsons/" + fileName + ".json";
#endif

        using (var writer = new System.IO.StreamWriter(writePath, false))
        {
            writer.Write(data);
            writer.Close();
        }

    }

    static void Init()
    {
        var loadPath = Application.streamingAssetsPath + "/Jsons/";
        string strStream = null;

#if UNITY_ANDROID && !UNITY_EDITOR

        //  androidならば初回読み込み以降はpersistentDataから
        var persistentDataPath = Application.persistentDataPath + "/Jsons/";
        if (File.Exists(persistentDataPath + "AllFilePath.txt"))
            loadPath = persistentDataPath;

        var allFilePath = loadPath + "AllFilePath.txt";

        UnityWebRequest request = UnityWebRequest.Get(allFilePath);
        request.timeout = 60;
        request.SendWebRequest();

        while (!request.isDone)
        {
            Debug.Log("request allpath");
        }

        if (!string.IsNullOrEmpty(request.error))
            Debug.LogError(request.error);

        Debug.Log("downnload jsons : " + request.downloadHandler.text);

        using (var sr = new System.IO.StringReader(request.downloadHandler.text))
        {//ストリームリーダーをstringに変換
            strStream = sr.ReadToEnd();
            sr.Close();
        }

        string tempStream = string.Empty;
        UnityWebRequest tempRequest = null;

        strStream.Split(",").ForEach(fileName =>
        {
            string jsonPath = loadPath + fileName + ".json";

            tempRequest = UnityWebRequest.Get(jsonPath);
            tempRequest.SendWebRequest();

            while (!tempRequest.isDone)
            {
                Debug.Log("request json : " + fileName);
            }

            if (!string.IsNullOrEmpty(tempRequest.error))
            {
                Debug.LogError(tempRequest.error);
                return;
            }

            //  必要であれば文字コードの関係で先頭に3バイト余白ができるので削る(UTF-8:BOMなしにする)
            //  https://forum.unity.com/threads/jsonutility-fromjson-error-invalid-value.421291/
            //  tempStream = System.Text.Encoding.UTF8.GetString(tempRequest.downloadHandler.data, 3, tempRequest.downloadHandler.data.Length - 3);

            tempStream = tempRequest.downloadHandler.text;
            jsons.AddIfNotExists(fileName, tempStream);
            Debug.Log("complete : " + fileName + " : " + tempStream);
        });
#else   //  PC  ||  Editor

        var jsonFiles = System.IO.Directory.GetFiles(loadPath);

        jsonFiles.ForEach(json =>
        {
            var file = new System.IO.FileInfo(json);
            if (file.Extension == ".meta")
                return;

            using (var sr = new System.IO.StreamReader(file.FullName))
            {//ストリームリーダーをstringに変換
                strStream = sr.ReadToEnd();
                sr.Close();
            }

            jsons.AddIfNotExists(file.Name.Replace(file.Extension, ""), strStream);
            Debug.Log("complete : " + file.Name.Replace(file.Extension, "") + " : " + strStream);

        });

#endif

        isLoaded = true;
    }

}
