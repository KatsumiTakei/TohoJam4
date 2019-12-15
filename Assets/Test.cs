using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    static Text text = null;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = string.Empty;
    }

    public static void DrawText(string message)
    {
        text.text = message;
    }




}
