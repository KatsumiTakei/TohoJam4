using System;
using UniRx;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField]
    eTileType tileType = eTileType.White;


    [SerializeField]
    eDirectionType direction = eDirectionType.Top;

    void Start()
    {
        if (tileType == eTileType.White)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.black;
        }
    }

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

        if (!IsClickedSelf())
            return;

        EventManager.BroadcastCheckAnswer(inputType, tileType, direction);
    }

    bool IsClickedSelf()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        foreach (var hit in raycastResults)
        {
            if (gameObject.GetHashCode() == hit.gameObject.GetHashCode())
                return true;
        }

        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(1f, 0.4f, 0.4f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tileType == eTileType.White)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.black;
        }
    }
}

public enum eTileType
{
    White,
    Black,
}


