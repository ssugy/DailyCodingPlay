using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRReader : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown && Input.GetMouseButtonDown(0))
        {
            Debug.Log("���콺 Ŭ��");
        }
        else if (Input.anyKeyDown)
        {
            Debug.Log(Input.inputString);
        }
    }
}
