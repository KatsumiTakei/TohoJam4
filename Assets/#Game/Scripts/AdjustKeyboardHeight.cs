using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustKeyboardHeight : MonoBehaviour
{
    Vector3 initPos = Vector3.zero;

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(initPos.x, SoftwareKeyboaryArea.Height * 0.5f + initPos.y);
    }
}
