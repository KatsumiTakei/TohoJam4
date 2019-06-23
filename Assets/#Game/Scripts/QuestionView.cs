using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionView : MonoBehaviour
{
    [field: RenameField(nameof(Imgs))]
    public List<Image> Imgs = null;

    private void Awake()
    {
        HideAll();
    }

    private void OnEnable()
    {
        EventManager.OnChangeQuestion += OnChangeQuestion;
    }

    private void OnDisable()
    {
        EventManager.OnChangeQuestion -= OnChangeQuestion;
    }

    void OnChangeQuestion()
    {

    }

    void DrawTile(eTileType tileType)
    {
        switch (tileType)
        {
            case eTileType.None:
                Imgs[ConvType(eImgType.White)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.Black)].gameObject.SetActive(false);
                break;
            case eTileType.White:
                Imgs[ConvType(eImgType.White)].gameObject.SetActive(true);
                Imgs[ConvType(eImgType.Black)].gameObject.SetActive(false);
                break;
            case eTileType.Black:
                Imgs[ConvType(eImgType.White)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.Black)].gameObject.SetActive(true);
                break;
        }
    }


    void DrawChar(eInputType inputType)
    {
        switch (inputType)
        {
            case eInputType.Any:
                Imgs[ConvType(eImgType.LChar)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.RChar)].gameObject.SetActive(false);
                break;
            case eInputType.ClickLeft:
                Imgs[ConvType(eImgType.LChar)].gameObject.SetActive(true);
                Imgs[ConvType(eImgType.RChar)].gameObject.SetActive(false);
                break;
            case eInputType.ClickRight:
                Imgs[ConvType(eImgType.LChar)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.RChar)].gameObject.SetActive(true);
                break;
        }

    }


    void DrawDir(eDirectionType directionType)
    {
        switch (directionType)
        {
            case eDirectionType.None:
                Imgs[ConvType(eImgType.TArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.BArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.LArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.RArraw)].gameObject.SetActive(false);
                break;
            case eDirectionType.Top:
                Imgs[ConvType(eImgType.TArraw)].gameObject.SetActive(true);
                Imgs[ConvType(eImgType.BArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.LArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.RArraw)].gameObject.SetActive(false);
                break;
            case eDirectionType.Left:
                Imgs[ConvType(eImgType.TArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.BArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.LArraw)].gameObject.SetActive(true);
                Imgs[ConvType(eImgType.RArraw)].gameObject.SetActive(false);
                break;
            case eDirectionType.Right:
                Imgs[ConvType(eImgType.TArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.BArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.LArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.RArraw)].gameObject.SetActive(true);
                break;
            case eDirectionType.Under:
                Imgs[ConvType(eImgType.TArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.BArraw)].gameObject.SetActive(true);
                Imgs[ConvType(eImgType.LArraw)].gameObject.SetActive(false);
                Imgs[ConvType(eImgType.RArraw)].gameObject.SetActive(false);
                break;
        }

    }

    public void DrawQuest(eTileType tileType, eInputType inputType, eDirectionType directionType)
    {
        DrawTile(tileType);
        DrawChar(inputType);
        DrawDir(directionType);

        var actives = Imgs.FindAll(img => img.isActiveAndEnabled);
        foreach (var img in actives)
        {
            img.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f), 0.5f, 2).OnComplete(() => img.rectTransform.localScale = Vector3.one);
        }

    }

    public void HideAll()
    {
        DrawQuest(eTileType.None, eInputType.Any, eDirectionType.None);
    }

    public int ConvType(eImgType imgType)
    {
        return (int)imgType;
    }

}

public enum eImgType
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
