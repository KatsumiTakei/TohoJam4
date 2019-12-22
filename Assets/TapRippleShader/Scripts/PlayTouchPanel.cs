using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayTouchPanel : MonoBehaviour
{
    [SerializeField]
    GameObject tapEffectPrefab = null;

    void Update()
    {
        //タップ時
        CheckTap();
    }

    void CheckTap()
    {
        if (GodTouches.GodTouch.GetPhase() == GodTouches.GodPhase.Began)
        {
            GenerateTapEffect(GodTouches.GodTouch.GetPosition());
        }
    }

    void GenerateTapEffect(Vector2 pos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Object.Instantiate(tapEffectPrefab, worldPos, Quaternion.identity, transform);
    }

}
