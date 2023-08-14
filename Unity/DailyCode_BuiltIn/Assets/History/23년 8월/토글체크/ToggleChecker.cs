using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChecker : MonoBehaviour
{
    Toggle tg;


    // Start is called before the first frame update
    void Start()
    {
        tg = GetComponent<Toggle>();
        tg.onValueChanged.AddListener(TESTValueChanged);
    }

    private void TESTValueChanged(bool arg0)
    {
        if (arg0)
        {
            Debug.Log($"{name} : true실행");
        }
        else
        {
            Debug.Log($"{name} : false실행");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
