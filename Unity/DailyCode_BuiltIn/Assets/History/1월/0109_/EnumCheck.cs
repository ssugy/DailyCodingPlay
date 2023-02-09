using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumCheck : MonoBehaviour
{
    public enum STATE
    {
        None,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }

    private STATE stage;

    void Start()
    {
        string[] str = typeof(STATE).GetEnumNames();
        foreach (var item in str)
        {
            Debug.Log("Item : " + item);
        }
        //Debug.Log(typeof(STATE).GetEnumNames());
    }

    
}
