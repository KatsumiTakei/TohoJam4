using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    static Text text = null;

    Vector3 initPos = Vector3.zero;

    private void Start()
    {
        text = GetComponent<Text>();
        if(text)
            text.text = string.Empty;

        initPos = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(initPos.x, SoftwareKeyboaryArea.Height * 0.5f + initPos.y);
    }

    public static void DrawText(string message)
    {
        text.text = message;
    }




}
