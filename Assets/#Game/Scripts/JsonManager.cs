using System.Collections.Generic;
using UnityEngine;
using STLExtensiton;
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

        using (StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/Jsons/" + fileName + ".json", false))
        {
            writer.Write(data);
            writer.Close();
        }

    }

    static void Init()
    {
        var jsonFiles = Directory.GetFiles(Application.streamingAssetsPath + "/Jsons/");

        jsonFiles.ForEach(json =>
        {
            string strStream = null;
            FileInfo file = new FileInfo(json);
            if (file.Extension == ".meta")
                return;

            using (StreamReader sr = new StreamReader(file.FullName))
            {
                //ストリームリーダーをstringに変換
                strStream = sr.ReadToEnd();
                sr.Close();
            }

            jsons.AddIfNotExists(file.Name.Replace(file.Extension, ""), strStream);
        });

        isLoaded = true;
    }

}
