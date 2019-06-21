using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainScene : MonoBehaviour
{

    void Update()
    {
        InputManager.ManualUpdate();
    }

}

public enum eDirectionType
{
    None,
    Top,
    Right,
    Left,
    Under,

}
