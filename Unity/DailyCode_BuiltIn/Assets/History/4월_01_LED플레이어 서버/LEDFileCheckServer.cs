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
    [Serializable]
    public struct LEDProtocal
    {
        public int originSortNum;       // ����Ʈ�� �ִ� ������ ����(������ ����)
        public int userSortNum;         // ����ڰ� ������ ����
        public string VideoStartTime;   // ������ ���۵Ǵ� �ð�(isRepeat�� false�� �� ����ȴ�.) HH:MM
        public string fileName;         // ���ϸ�
        public FileType fileType;       // ���� Ÿ��(Video, Img)
        public string fileFullPath;     // ������ ���� ���
        public int byteFileSize;        // ������ �뷮(����Ʈ ����)
        public float vertNormal;        // ��Ƽ�� �븻������, �ش簪�� 0.5�̸� �߾� ����, 0�̸� ����, 1�̸� �Ʒ��� ����.

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
        CheckLEDFileList,   // �������� �����Ϸ��� ������ ��� �����ϴ��� üũ�ϴ� �뵵�� ��������
    }
    // Ŭ���̾�Ʈ���� ������ ���� �� ���������� Ÿ��(���Ͽ�û�̳�, �ٸ����̳� ��Ÿ ���.)
    public enum ToServerProtocal
    {
        None,
        RequestFile,    // ������ �޶�� Ŭ���̾�Ʈ�� ��û�� �� �� ���Ǵ� ��������
    }
    // ����Ÿ��
    public enum FileType
    {
        Video,
        Img
    }

    public List<LEDProtocal> originList;
    private List<LEDProtocal> sortedList;
    public bool isFulRepeat;        // ��ü �ݺ��̳�, �� ������ Ư�� �ð��� ������ Ʋ������ �� ���̳Ŀ� ���� ����
    public ORTCPMultiServer multiServer;

    // ��ŸƮ�ܿ��� ������ �߱� ������ ��ť ������ �ȵǴ� �� ó�� ������ ���̴�.
    private void Start()
    {
        // Json���� �������� - ����ڰ� �����ϸ� �ٲ� �� �־�� ��.
        /**
         * �������� �������� ������ struct���ο� �ּ����� �ۼ��Ͽ����ϴ�.
         */
        originList = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "12:00", "test.png"       ,FileType.Img   , Application.dataPath + "/tmp/" + "test.png"           , 10000000, 0.1f),
            new LEDProtocal(2, 2, "11:28", "������ 06.mp4"  ,FileType.Video , Application.dataPath + "/tmp/" + "������ 06.mp4"      , 20000000, 0.2f),
            new LEDProtocal(3, 3, "02:00", "������ 06_1.mp4",FileType.Video , Application.dataPath + "/tmp/" + "������ 06_1.mp4"    , 30000000, 0.3f),
            new LEDProtocal(4, 4, "03:00", "������ 06_2.mp4",FileType.Video , Application.dataPath + "/tmp/" + "������ 06_2.mp4"    , 30000000, 0.4f),
            new LEDProtocal(5, 5, "04:00", "������ 06_3.mp4",FileType.Video , Application.dataPath + "/tmp/" + "������ 06_3.mp4"    , 30000000, 0.5f),
            new LEDProtocal(6, 4, "05:00", "�ڻ� 51@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "�ڻ� 51@2x.png"     , 30000000, 0.6f),
            new LEDProtocal(5, 4, "06:00", "�ڻ� 64@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "�ڻ� 64@2x.png"     , 30000000, 0.7f),
            new LEDProtocal(5, 4, "07:00", "�ڻ� 67@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "�ڻ� 67@2x.png"     , 30000000, 0.8f)
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

    // CheckLEDFileList|true|3|a,s,d|a,s,d|a,s,d �̷������� ���ϵȴ� => Ŭ���̾�Ʈ���� ��������.
    private string GetLEDSendMessage(List<LEDProtocal> resultArr, bool isAllRepeat)
    {
        string result = Enum.GetName(typeof(ToClientProtocal), ToClientProtocal.CheckLEDFileList) + "|"; // ù��°�� ��ȣ�� ���� ������
        result += isAllRepeat + "|";           // �ι�° ���� isAllRepeat
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

    // �׽�Ʈ�� ��ü�ݺ� ��ư
    public void CheckLEDListFile(bool isRepeatAll)
    {
        isFulRepeat = isRepeatAll;
        multiServer.SendAll(GetLEDSendMessage(sortedList, isRepeatAll));
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1��Ű ����");
            originList.Clear();
            originList = new List<LEDProtocal>
            {
                new LEDProtocal(1, 5, "12:00", "test.png"       ,FileType.Img   , Application.dataPath + "/tmp/" + "test.png"           , 10000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(2, 4, "11:28", "������ 06.mp4"  ,FileType.Video , Application.dataPath + "/tmp/" + "������ 06.mp4"      , 20000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(3, 3, "02:00", "������ 06_1.mp4",FileType.Video , Application.dataPath + "/tmp/" + "������ 06_1.mp4"    , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(4, 2, "03:00", "������ 06_2.mp4",FileType.Video , Application.dataPath + "/tmp/" + "������ 06_2.mp4"    , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(5, 1, "04:00", "������ 06_3.mp4",FileType.Video , Application.dataPath + "/tmp/" + "������ 06_3.mp4"    , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(6, 3, "05:00", "�ڻ� 51@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "�ڻ� 51@2x.png"     , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(5, 4, "06:00", "�ڻ� 64@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "�ڻ� 64@2x.png"     , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(5, 5, "07:00", "�ڻ� 67@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "�ڻ� 67@2x.png"     , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f))
            };

            if (isFulRepeat)
                sortedList = originList.OrderBy(x => x.userSortNum).ThenBy(x => x.originSortNum).ToList();
            else
                sortedList = originList.OrderBy(x => x.VideoStartTime).ThenBy(x => x.originSortNum).ToList();
        }
    }
}
