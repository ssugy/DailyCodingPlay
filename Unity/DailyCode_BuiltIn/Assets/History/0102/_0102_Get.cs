using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _0102_Get : MonoBehaviour
{
    public Camera cam;
    public Camera cam2 => cam;
    public Camera cam3 { get { return cam; } }

    public enum STATE
    {
        INIT = 0,
        Sample_0 = 1,
        Sample_1 = 2,
    }

    STATE sampleState;
    private void Start()
    {
        sampleState = STATE.INIT;
        //STATE obj = (STATE)Enum.Parse(typeof(STATE), "Sample_0"); // 굳이 이렇게 안하고
        object obj = Enum.Parse(typeof(STATE), "Sample_0"); // 이렇게 해도 상관없음.

        Debug.Log((int)obj);    // 이런식으로 바꿔서 사용 가능
        Debug.Log(obj.GetType());

        EnumCheck();
    }

    [Flags]
    enum Colors { Red = 1, Green = 2, Blue = 4, Yellow = 8 };

    public static void EnumCheck()
    {
        Console.WriteLine("The entries of the Colors enumeration are:");
        foreach (string colorName in Enum.GetNames(typeof(Colors)))
        {
            // 1:D는 1번 파라미터를 데시멀 타입으로 표현하라는 의미
            Console.WriteLine("{0} = {1:D}", colorName,
                                         Enum.Parse(typeof(Colors), colorName));
        }
        Console.WriteLine();

        Colors orange = (Colors)Enum.Parse(typeof(Colors), "Red, Yellow");
        Console.WriteLine("The orange value {0:D} has the combined entries of {0}",
                           orange);
    }
}
