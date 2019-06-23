using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityDLL;
using STLExtensiton;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    #region  Variables 

    [System.Serializable]
    public class AudioData
    {
        public float VolumeBGM = 0;
        public float VolumeSE = 0;
    }

    // Time to take though BGM performs fading.
    public const float BgmFadeSpeedRateHigh = 0.9f;
    public const float BgmFadeSpeedRateLow = 0.3f;
    static float bgmFadeSpeedRate = BgmFadeSpeedRateHigh;

    // Audio name to flow next
    static string nextBGMName;
    static bool isFadeOut = false;

    static AudioSource bgmSource;
    static List<AudioSource> _seSourceList = new List<AudioSource>();
    const int SeSourceNum = 20;

    static Dictionary<string, AudioClip> bgmDic = new Dictionary<string, AudioClip>();
    static Dictionary<string, AudioClip> seDic = new Dictionary<string, AudioClip>();

    AudioData audioData;
    #endregion

    private new void Awake()
    {
        base.Awake();

        #region Add components
        // Making Audiolistener and AudioSource.
        if (!Camera.main?.GetComponent<AudioListener>())
            gameObject.AddComponent<AudioListener>();

        for (int i = 0; i < SeSourceNum + 1; i++)
            gameObject.AddComponent<AudioSource>();

        #endregion

        //  R/W Json
        audioData = JsonManager.FromJson<AudioData>(nameof(AudioData));

        // Getting audio source and setting to each variable, then setting that volume
        AudioSource[] audioSourceArray = GetComponents<AudioSource>();

        //  audioSource[0] to bgmSource
        bgmSource = audioSourceArray[0];
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.volume = audioData.VolumeBGM;

        for (int i = 0 + 1; i < audioSourceArray.Length; i++)
        {// Init SE
            AddSeAudioLisner(audioSourceArray[i]);
        }

        AudioClip[] bgmList = Resources.LoadAll<AudioClip>(ResourcesPath.Audio.BGM.Path);
        AudioClip[] seList = Resources.LoadAll<AudioClip>(ResourcesPath.Audio.SE.Path);

        foreach (AudioClip bgm in bgmList)
        {
            bgmDic[bgm.name] = bgm;
        }
        foreach (AudioClip se in seList)
        {
            seDic[se.name] = se;
        }
    }

    private void Update()
    {
        if (!isFadeOut)
            return;

        // Its volume downs gradually and its volume restores defaults if it becomes 0 then flow next bgm.

        bgmSource.volume -= Time.deltaTime * bgmFadeSpeedRate;
        if (bgmSource.volume > 0)
            return;

        bgmSource.Stop();
        bgmSource.volume = audioData.VolumeBGM;
        isFadeOut = false;

        if (!string.IsNullOrEmpty(nextBGMName))
            PlayBGM(nextBGMName);

    }

    #region For SE

    public void PlaySE(string seName)
    {
        Debug.Assert(seDic.ContainsKey(seName), seName + " is nothing.");

        var se = _seSourceList.Find(seSources => !seSources.isPlaying);
        if (!se)
        {// TODO    :   Add SourceList 
            var audioSource = gameObject.AddComponent<AudioSource>();
            se = audioSource;
            AddSeAudioLisner(audioSource);
            //Debug.LogWarning("_seSourceList no vacancy...");
            //return;
        }
        se.PlayOneShot(seDic[seName]);
    }

    void AddSeAudioLisner(AudioSource source)
    {
        _seSourceList.Add(source);
        source.playOnAwake = false;
        source.volume = audioData.VolumeSE;
    }

    //  TODO    :   delete candidate
    //public static void PlaySE(string seName, float delay)
    //{
    //    Debug.Assert(_seDic.ContainsKey(seName), seName + "is nothing.");

    //    Observable.Timer(System.TimeSpan.FromSeconds(delay))
    //        .Subscribe(_ =>
    //    {
    //        var se = _seSourceList.Find(seSources => !seSources.isPlaying);
    //        if (!se)
    //            return;

    //        se.PlayOneShot(_seDic[seName] as AudioClip);
    //    });
    //}

    #endregion

    #region For BGM

    public static void PlayBGM(string bgmName, float fadeSpeedRate = BgmFadeSpeedRateHigh)
    {
        Debug.Assert(bgmDic.ContainsKey(bgmName), bgmName + " is nothing.");

        if (!bgmSource.isPlaying)
        {//  It flows when bgm doesn't flow.
            nextBGMName = null;
            bgmSource.clip = bgmDic.TryGetValueEx(bgmName, null);
            bgmSource.Play();
        }
        else if (bgmSource.clip.name != bgmName)
        {// if any bgm flows already, flow next bgm after it fade out.(If next bgm and currently bgm are same, it is through.)
            nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }
    }

    public static void StopBGM()
    {
        bgmSource.Stop();
    }

    public static void FadeOutBGM(float fadeSpeedRate = BgmFadeSpeedRateLow)
    {
        bgmFadeSpeedRate = fadeSpeedRate;
        isFadeOut = true;
    }

    #endregion

    #region For volume

    public static void ChangeBothVolume(float BGMVolume, float SEVolume)
    {
        bgmSource.volume = BGMVolume;
        foreach (AudioSource seSources in _seSourceList)
        {
            seSources.volume = SEVolume;
        }

    }

    public static void ChangeBGMVolume(float BGMVolume)
    {
        bgmSource.volume = BGMVolume;
    }

    public static void ChangeSEVolume(float SEVolume)
    {
        foreach (AudioSource seSources in _seSourceList)
        {
            seSources.volume = SEVolume;
        }
    }


    #endregion


}