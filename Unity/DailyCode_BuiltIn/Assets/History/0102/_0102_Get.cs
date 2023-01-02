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
        //STATE obj = (STATE)Enum.Parse(typeof(STATE), "Sample_0"); // ���� �̷��� ���ϰ�
        object obj = Enum.Parse(typeof(STATE), "Sample_0"); // �̷��� �ص� �������.

        Debug.Log((int)obj);    // �̷������� �ٲ㼭 ��� ����
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
            // 1:D�� 1�� �Ķ���͸� ���ø� Ÿ������ ǥ���϶�� �ǹ�
            Console.WriteLine("{0} = {1:D}", colorName,
                                         Enum.Parse(typeof(Colors), colorName));
        }
        Console.WriteLine();

        Colors orange = (Colors)Enum.Parse(typeof(Colors), "Red, Yellow");
        Console.WriteLine("The orange value {0:D} has the combined entries of {0}",
                           orange);
    }
}
