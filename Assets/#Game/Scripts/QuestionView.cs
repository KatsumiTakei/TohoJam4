using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionView : MonoBehaviour
{
    [SerializeField]
    [field: RenameField(nameof(imgs))]
    Image[] imgs = null;

    [SerializeField]
    [field: RenameField(nameof(gyroImgs))]
    Image[] gyroImgs = null;

    enum eImgType
    {
        White,
        Black,

        RChar,
        LChar,

        RArraw,
        LArraw,
        TArraw,
        BArraw,

    }


    private void Awake()
    {
        HideAll();


#if     UNITY_ANDROID && !UNITY_EDITOR

        for (int i = 0; i < gyroImgs.Length; i++)
        {
            gyroImgs[i].gameObject.SetActive(false);
            imgs[(int)(eImgType.RChar) + i] = gyroImgs[i];
        }
#endif  //  UNITY_ANDROID && !UNITY_EDITOR


    }

    void DrawTile(eTileType tileType)
    {
        switch (tileType)
        {
            case eTileType.None:
                imgs[(int)(eImgType.White)].gameObject.SetActive(false);
                imgs[(int)(eImgType.Black)].gameObject.SetActive(false);
                break;
            case eTileType.White:
                imgs[(int)(eImgType.White)].gameObject.SetActive(true);
                imgs[(int)(eImgType.Black)].gameObject.SetActive(false);
                break;
            case eTileType.Black:
                imgs[(int)(eImgType.White)].gameObject.SetActive(false);
                imgs[(int)(eImgType.Black)].gameObject.SetActive(true);
                break;
        }
    }


    void DrawChar(eInputType inputType)
    {
        switch (inputType)
        {
            case eInputType.Any:
                imgs[(int)(eImgType.LChar)].gameObject.SetActive(false);
                imgs[(int)(eImgType.RChar)].gameObject.SetActive(false);
                break;
            case eInputType.ClickLeft:
                imgs[(int)(eImgType.LChar)].gameObject.SetActive(true);
                imgs[(int)(eImgType.RChar)].gameObject.SetActive(false);
                break;
            case eInputType.ClickRight:
                imgs[(int)(eImgType.LChar)].gameObject.SetActive(false);
                imgs[(int)(eImgType.RChar)].gameObject.SetActive(true);
                break;
        }

    }


    void DrawDir(eDirectionType directionType)
    {
        switch (directionType)
        {
            case eDirectionType.None:
                imgs[(int)(eImgType.TArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.BArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.LArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.RArraw)].gameObject.SetActive(false);
                break;
            case eDirectionType.Top:
                imgs[(int)(eImgType.TArraw)].gameObject.SetActive(true);
                imgs[(int)(eImgType.BArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.LArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.RArraw)].gameObject.SetActive(false);
                break;
            case eDirectionType.Left:
                imgs[(int)(eImgType.TArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.BArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.LArraw)].gameObject.SetActive(true);
                imgs[(int)(eImgType.RArraw)].gameObject.SetActive(false);
                break;
            case eDirectionType.Right:
                imgs[(int)(eImgType.TArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.BArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.LArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.RArraw)].gameObject.SetActive(true);
                break;
            case eDirectionType.Under:
                imgs[(int)(eImgType.TArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.BArraw)].gameObject.SetActive(true);
                imgs[(int)(eImgType.LArraw)].gameObject.SetActive(false);
                imgs[(int)(eImgType.RArraw)].gameObject.SetActive(false);
                break;
        }

    }

    public void DrawQuest(eTileType tileType, eInputType inputType, eDirectionType directionType)
    {
        DrawTile(tileType);
        DrawChar(inputType);
        DrawDir(directionType);

        var actives = Array.FindAll(imgs, (img => img.isActiveAndEnabled));
        foreach (var img in actives)
        {
            img.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f), 0.5f, 2).OnComplete(() => img.rectTransform.localScale = Vector3.one);
        }

    }

    public void HideAll()
    {
        DrawQuest(eTileType.None, eInputType.Any, eDirectionType.None);
    }

}
