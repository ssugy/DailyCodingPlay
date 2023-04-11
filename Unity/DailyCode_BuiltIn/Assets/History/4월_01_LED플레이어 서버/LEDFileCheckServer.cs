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
        public FileType fileType;
        public string fileFullPath;
        public int byteFileSize;
        public float vertNormal;

        public LEDProtocal(int originNum, int userNum, string videoStartTime, string mFileName, FileType type, string mFileFullPath, int mByteFileSize, float normal = 0.5f)
        {
            originSortNum = originNum;
            userSortNum= userNum;
            VideoStartTime= videoStartTime;
            fileName = mFileName;
            fileType = type;
            fileFullPath = mFileFullPath;
            byteFileSize= mByteFileSize;
            vertNormal= normal;
        }

        public override string ToString() => $"{VideoStartTime},{fileName},{fileType},{byteFileSize},{vertNormal}";
        public void ShowAll() => Debug.Log($"{originSortNum},{userSortNum},{VideoStartTime},{fileName},{fileType},{byteFileSize},{vertNormal}");
    }
    // 
    private enum ToClientProtocal
    {
        None,
        CheckLEDFileList,
    }
    // Ŭ���̾�Ʈ���� ������ ���� �� ���������� Ÿ��(���Ͽ�û�̳�, �ٸ����̳� ��Ÿ ���.)
    public enum ToServerProtocal
    {
        None,
        RequestFile,
    }
    // ����Ÿ��
    public enum FileType
    {
        Video,
        Img
    }

    private List<LEDProtocal> originList;
    private List<LEDProtocal> sortedList;
    public bool isFulRepeat;

    public ORTCPMultiServer multiServer;
    public Button btn;

    // ��ŸƮ�ܿ��� ������ �߱� ������ ��ť ������ �ȵǴ� �� ó�� ������ ���̴�.
    private void Start()
    {
        btn.onClick.AddListener(CheckLEDListFile);
        // Json���� �������� - ����ڰ� �����ϸ� �ٲ� �� �־�� ��.
        originList = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "12:00", "test.png"       ,FileType.Img   , Application.dataPath + "\\tmp\\" + "test.png"           , 10000000, 0.5f),
            new LEDProtocal(2, 2, "11:28", "������ 06.mp4"  ,FileType.Video , Application.dataPath + "\\tmp\\" + "������ 06.mp4"      , 20000000, 0.5f),
            new LEDProtocal(3, 3, "02:00", "������ 06_1.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "������ 06_1.mp4"    , 30000000, 0.5f),
            new LEDProtocal(4, 4, "03:00", "������ 06_2.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "������ 06_2.mp4"    , 30000000, 0.5f),
            new LEDProtocal(5, 5, "04:00", "������ 06_3.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "������ 06_3.mp4"    , 30000000, 0.5f),
            new LEDProtocal(6, 4, "05:00", "�ڻ� 51@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "�ڻ� 51@2x.png"     , 30000000, 0.5f),
            new LEDProtocal(5, 4, "06:00", "�ڻ� 64@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "�ڻ� 64@2x.png"     , 30000000, 0.5f),
            new LEDProtocal(5, 4, "07:00", "�ڻ� 67@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "�ڻ� 67@2x.png"     , 30000000, 0.5f)
        };

        // ������ ��ť�� �ѹ��� ����
        if (isFulRepeat)
            sortedList = originList.OrderBy(x => x.userSortNum).ThenBy(x => x.originSortNum).ToList();
        else
            sortedList = originList.OrderBy(x => x.VideoStartTime).ThenBy(x => x.originSortNum).ToList();

        foreach (var item in sortedList)
        {
            Debug.Log(item.ToString());
        }
    }

    // LEDFileList|true|3|a,s,d|a,s,d|a,s,d �̷������� ������.
    private string GetLEDSendMessage(List<LEDProtocal> resultArr)
    {
        string result = Enum.GetName(typeof(ToClientProtocal), ToClientProtocal.CheckLEDFileList) + "|"; // ù��°�� ��ȣ�� ���� ������
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
        Debug.Log(result);
        return result;
    }

    private void CheckLEDListFile()
    {
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
