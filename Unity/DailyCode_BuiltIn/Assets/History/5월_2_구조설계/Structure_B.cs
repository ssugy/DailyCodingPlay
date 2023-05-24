using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContentType
{
    None = 0,
    CellingLED,
    Dispensor,
    SmartMirror
}
public class Structure_B : Structure_B_Mother
{
    
    public int CheckInt;

    public void Somefunc()
    {
        Debug.Log("B에 포함된 함수 실행");
    }
}
