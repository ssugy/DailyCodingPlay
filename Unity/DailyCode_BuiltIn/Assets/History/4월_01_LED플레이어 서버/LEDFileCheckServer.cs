using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LEDFileCheckServer : MonoBehaviour
{
    /// <summary>
    /// LED Protocal ���� ��Ҵ� ,�� �����ϰ� LED Protocal ��ü�� ������ |�� �����Ѵ�.
    /// ���� ������ �����ؼ�, Client���� ������.
    /// </summary>
    private struct LEDProtocal
    {
        public int originSortNum;       // ����Ʈ�� �ִ� ������ ����(������ ����)
        public int userSortNum;         // ����ڰ� ������ ����
        public string VideoStartTime;   // HH:MM
        public string fileName;
        public int byteFileSize;
        public float vertNormal;

        public LEDProtocal(int originNum, int userNum, string videoStartTime, string mFileName, int mByteFileSize, float normal = 0.5f)
        {
            originSortNum = originNum;
            userSortNum= userNum;
            VideoStartTime= videoStartTime;
            fileName = mFileName;
            byteFileSize= mByteFileSize;
            vertNormal= normal;
        }

        public override string ToString() => $"{originSortNum},{userSortNum},{VideoStartTime},{fileName},{byteFileSize},{vertNormal}";
    }
    private enum ProtocalType
    {
        None,
        LEDFileList,
    }

    private List<LEDProtocal> originList;
    private List<LEDProtocal> sortedList;
    private bool isFulRepeat;

    public ORTCPMultiServer multiServer;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(CheckLEDListFile);
        // Json���� ��������
        originList = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "19:00", "test.mp4", 10000000),
            new LEDProtocal(2, 3, "18:00", "test1.mp4", 20000000),
            new LEDProtocal(3, 2, "15:00", "test2.mp4", 30000000),
            new LEDProtocal(4, 6, "13:00", "test3.mp4", 30000000),
            new LEDProtocal(5, 5, "11:00", "test4.mp4", 30000000),
            new LEDProtocal(6, 4, "15:00", "test5.mp4", 30000000),
            new LEDProtocal(5, 4, "15:00", "test6.mp4", 30000000)
        };

        // ������ ��ť�� �ѹ��� ����
        if (isFulRepeat) 
            sortedList = originList.OrderBy(x => x.userSortNum).ThenBy(x => x.originSortNum).ToList();
        else
            sortedList= originList.OrderBy(x => x.VideoStartTime).ThenBy(x => x.originSortNum).ToList();

        foreach (var item in sortedList)
        {
            Debug.Log(item.ToString());
        }
    }

    // LEDFileList|3|1|a,s,d|a,s,d|a,s,d �̷������� ������.
    private string GetLEDSendMessage(List<LEDProtocal> resultArr)
    {
        string result = Enum.GetName(typeof(ProtocalType), ProtocalType.LEDFileList) + "|"; // ù��°�� ��ȣ�� ���� ������
        result += isFulRepeat + "|";           // �ι�° ���� isfullRepeat
        result += resultArr.Count + "|";           // ����° ���� ����Ʈ�� ����
        for (int i = 0; i < resultArr.Count; i++)  // �׹�° ���� ������ ����ü�� ��ҵ�
        {
            result += resultArr[i].ToString();
            if (i != resultArr.Count - 1)
            {
                result += "|";
            }
        }
        return result;
    }

    private void CheckLEDListFile()
    {
        Debug.Log("������ ���� : " + GetLEDSendMessage(sortedList));
        multiServer.SendAll(GetLEDSendMessage(sortedList));
    }
}
