using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class _0103_AppDomain : MonoBehaviour
{
    void Start()
    {
        Debug.Log("AppDomain.CurrentDomain : " + AppDomain.CurrentDomain);
        Debug.Log("AppDomain.CurrentDomain : " + AppDomain.CreateDomain("TestDomain"));

        AppDomain testDomain = AppDomain.CreateDomain("TestDomain");
        Debug.Log("AppDomain.CurrentDomain : " + AppDomain.CurrentDomain.GetAssemblies());
        Assembly[] assembly = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var item in testDomain.GetAssemblies())
        {
            Debug.Log(item);
        }

        // 어셈블리 타입
        Debug.Log("어셈블리 타입 : " + assembly.GetType());
        var types = AppDomain.CurrentDomain
                        .GetAssemblies()
                        .SelectMany(x => x.GetTypes());
        foreach (var type in types) { Debug.Log(type); }
    }
}
