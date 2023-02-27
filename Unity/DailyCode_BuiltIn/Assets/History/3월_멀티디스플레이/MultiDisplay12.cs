using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplay12 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Debug.Log("활성화 디스플레이 : " + (i+1));
            Display.displays[i].Activate();
        }
    }
}
