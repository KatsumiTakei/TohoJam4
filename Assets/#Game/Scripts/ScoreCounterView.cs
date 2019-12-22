using UnityEngine;
using DG.Tweening;

public class ScoreCounterView
{
    float tweenadjust = 1.0f;
    float tweenspeed = 0.5f;
    float ptsboader = 10f;

    TMPro.TextMeshProUGUI scoretext = null;
    int drawValue = 0;

    const string Digit = "D9";

    public ScoreCounterView(TMPro.TextMeshProUGUI scoretext, float tweenspeed, float ptsboader)
    {
        this.ptsboader = Mathf.Max(ptsboader, 1f);
        this.tweenspeed = Mathf.Clamp(tweenspeed, 0.1f, 5f);
        this.scoretext = scoretext;
        this.scoretext.text = 0.ToString(Digit);
    }


    /// <summary>
    /// スコアを補間しながら加算する関数
    /// </summary>
    /// <param name="pts">変化するバリュー</param>
    /// <param name="score">現在のスコア</param>
    public void AnimChangeScore(int pts, int score)
    {
        if (pts < ptsboader)
        {//   補間補正判定 ptsの大きさに比例した変位をさせるが変位上限ptsboaderを超えたら違う挙動にする
            tweenadjust = Mathf.Max(pts / ptsboader, 0.1f);
        }
        else
        {
            tweenadjust = 1f;
        }

        DOTween
            .To(
            () => drawValue,
            (n) => drawValue = n,
            Mathf.Clamp(score + pts, 0, ScoreManager.LimitScore),
            tweenspeed * tweenadjust)
            .OnUpdate(() => { scoretext.text = drawValue.ToString(Digit); });
        
    }
}