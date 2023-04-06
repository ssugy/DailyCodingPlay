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
    private static LEDFileCheckServer unique;
    public static LEDFileCheckServer instance { get { return unique; } }
    private void Awake() => unique = this;

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
        public string fileFullPath;
        public int byteFileSize;
        public float vertNormal;

        public LEDProtocal(int originNum, int userNum, string videoStartTime, string mFileName, string mFileFullPath, int mByteFileSize, float normal = 0.5f)
        {
            originSortNum = originNum;
            userSortNum= userNum;
            VideoStartTime= videoStartTime;
            fileName = mFileName;
            fileFullPath = mFileFullPath;
            byteFileSize= mByteFileSize;
            vertNormal= normal;
        }

        public override string ToString() => $"{VideoStartTime},{fileName},{byteFileSize},{vertNormal}";
        public void ShowAll() => Debug.Log($"{originSortNum},{userSortNum},{VideoStartTime},{fileName},{byteFileSize},{vertNormal}");
    }
    private enum ProtocalType
    {
        None,
        LEDFileList,
    }
    public enum ToServerProtocal
    {
        None,
        RequestFile,
    }

    private List<LEDProtocal> originList;
    private List<LEDProtocal> sortedList;
    private bool isFulRepeat;

    public ORTCPMultiServer multiServer;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(CheckLEDListFile);
        // Json���� �������� - ����ڰ� �����ϸ� �ٲ� �� �־�� ��.
        originList = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "19:00", "test.png"         , Application.dataPath + "\\tmp\\" + "test.png"           , 10000000),
            new LEDProtocal(2, 3, "18:00", "Video.mp4"        , Application.dataPath + "\\tmp\\" + "Video.mp4"          , 20000000),
            new LEDProtocal(3, 2, "15:00", "������ 06_1.mp4"  , Application.dataPath + "\\tmp\\" + "������ 06_1.mp4"    , 30000000),
            new LEDProtocal(4, 6, "13:00", "�ڻ� 49@2x.png"   , Application.dataPath + "\\tmp\\" + "�ڻ� 49@2x.png"     , 30000000),
            new LEDProtocal(5, 5, "11:00", "�ڻ� 50@2x.png"   , Application.dataPath + "\\tmp\\" + "�ڻ� 50@2x.png"     , 30000000),
            new LEDProtocal(6, 4, "15:00", "�ڻ� 51@2x.png"   , Application.dataPath + "\\tmp\\" + "�ڻ� 51@2x.png"     , 30000000),
            new LEDProtocal(5, 4, "15:00", "�ڻ� 64@2x.png"   , Application.dataPath + "\\tmp\\" + "�ڻ� 64@2x.png"     , 30000000),
            new LEDProtocal(5, 4, "15:00", "�ڻ� 67@2x.png"   , Application.dataPath + "\\tmp\\" + "�ڻ� 67@2x.png"     , 30000000)
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

    // LEDFileList|true|3|a,s,d|a,s,d|a,s,d �̷������� ������.
    private string GetLEDSendMessage(List<LEDProtocal> resultArr)
    {
        string result = Enum.GetName(typeof(ProtocalType), ProtocalType.LEDFileList) + "|"; // ù��°�� ��ȣ�� ���� ������
        result += isFulRepeat + "|";           // �ι�° ���� isfullRepeat
        result += resultArr.Count + "|";           // ����° ���� ����Ʈ�� ����
        for (int i = 0; i < resultArr.Count; i++)  // �׹�° ���� ������ ����ü�� ��ҵ�
        {
            result += $"{i},{resultArr[i].ToString()}"; // Ŭ���̾�Ʈ���� ���� �� ������ "����,���۽ð�,�̸�.."�̷��� �ȴ�.
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

    // ������ �Է��ϸ� ��ü ��θ� ã�Ƽ� �˷��ִ� �Լ�
    public string GetFileFullPath(string fileName)
    {
        for (int i = 0; i < originList.Count; i++)
        {
            if (originList[i].fileName == fileName)
            {
                return originList[i].fileFullPath;
            }
        }
        return "";
    }
}
