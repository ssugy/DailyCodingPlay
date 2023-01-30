using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRReader : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown && Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스 클릭");
        }
        else if (Input.anyKeyDown)
        {
            Debug.Log(Input.inputString);
        }
    }
}
