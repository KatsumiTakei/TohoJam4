using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class CompileFinishChecker
{
    [InitializeOnLoadMethod]
    private static void Init()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            return;

        EditorApplication.delayCall += OnPlay;
    }

    private static void OnPlay()
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
        method.Invoke(instance, new object[] { clip, 0 ,false });
    }
}