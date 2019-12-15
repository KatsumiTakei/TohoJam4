using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum eTileType
{
    None,
    White,
    Black,
    Random = 99,
}

public class Tile : MonoBehaviour
    //, IPointerEnterHandler
    //, IPointerExitHandler
{
    [SerializeField]
    eTileType tileType = eTileType.White;

    [SerializeField]
    eDirectionType direction = eDirectionType.None;

    void OnEnable()
    {
        EventManager.OnMultipleInput += OnMultipleInput;
    }

    void OnDisable()
    {
        EventManager.OnMultipleInput -= OnMultipleInput;
    }


    void OnMultipleInput(eInputType inputType)
    {
        if (!InputManager.IsClick(inputType))
            return;

#if UNITY_ANDROID && !UNITY_EDITOR

        inputType = (AccelerationVallue.AccelerationX < 0) ? eInputType.ClickLeft : eInputType.ClickRight;

#endif  //  UNITY_ANDROID && !UNITY_EDITOR

        if (!IsClickedSelf())
            return;

        if (ProgressManager.Instance.IsFinished)
            return;

        EventManager.BroadcastCheckAnswer(inputType, tileType, direction);
    }

    bool IsClickedSelf()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        foreach (var hit in raycastResults)
        {
            if (gameObject.GetHashCode() == hit.gameObject.GetHashCode())
                return true;
        }

        return false;
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    GetComponent<Image>().color = new Color(1f, 0.4f, 0.4f);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    GetComponent<Image>().color = Color.white;
    //}
}
