using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DateTime Today : " + DateTime.Today);
        Debug.Log("yyyy : " + DateTime.Now.ToString("yyyy"));
        Debug.Log("yyyy-MM-dd : " + DateTime.Now.ToString("yyyy-MM-dd"));
        Debug.Log("MM : " + DateTime.Now.ToString("MM"));
        Debug.Log("dd : " + DateTime.Now.ToString("dd"));
        Debug.Log("hh : " + DateTime.Now.ToString("hh"));
        Debug.Log("hh-mm : " + DateTime.Now.ToString("hh-mm"));
        Debug.Log("hh:mm : " + DateTime.Now.ToString("hh:mm"));
        Debug.Log("hh-mm-ss : " + DateTime.Now.ToString("hh-mm-ss"));
        Debug.Log("Time.time - ���� �� ����ð� : " + Time.time);

        CheckDictionary();
        CheckCompareTime();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("��ư ����");
            CheckCompareTime();
        }
    }

    private void CheckCompareTime()
    {
        DateTime dateTime= new DateTime(2024,4,11,10,28,0);
        //else if (DateTime.Now.CompareTo(DateTime.Now.ToString("yyyy:MM:dd:HH:mm")) < 0)   // �̷��� ���ϸ� �ƱԸ�Ʈ �ͼ��� ���´�.
        // 1 : ���ϴ� �ð��� �� ������ ���
        // 0 : ������ ���
        // -1 : ���ϴ� �ð��� �̷��� ���
        Debug.Log("�ð� �� ��� : " + DateTime.Now.CompareTo(dateTime));
    }

    private void CheckDictionary()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("asd", "123");
        dic.Add("asd1", "1231");
        dic.Add("asd2", "1232");
        dic.Add("asd3", "1233");
        dic.Add("asd4", "1234");
    }
}
