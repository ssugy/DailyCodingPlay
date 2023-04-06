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
    /// LED Protocal 개별 요소는 ,로 구분하고 LED Protocal 자체의 구분은 |로 구분한다.
    /// 최종 순서를 정리해서, Client한테 보낸다.
    /// </summary>
    private struct LEDProtocal
    {
        public int originSortNum;       // 리스트에 있는 영상의 순서(물리적 순서)
        public int userSortNum;         // 사용자가 지정한 순서
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
        // Json으로 가져오기 - 사용자가 선택하면 바꿀 수 있어야 함.
        originList = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "19:00", "test.png"         , Application.dataPath + "\\tmp\\" + "test.png"           , 10000000),
            new LEDProtocal(2, 3, "18:00", "Video.mp4"        , Application.dataPath + "\\tmp\\" + "Video.mp4"          , 20000000),
            new LEDProtocal(3, 2, "15:00", "시퀀스 06_1.mp4"  , Application.dataPath + "\\tmp\\" + "시퀀스 06_1.mp4"    , 30000000),
            new LEDProtocal(4, 6, "13:00", "자산 49@2x.png"   , Application.dataPath + "\\tmp\\" + "자산 49@2x.png"     , 30000000),
            new LEDProtocal(5, 5, "11:00", "자산 50@2x.png"   , Application.dataPath + "\\tmp\\" + "자산 50@2x.png"     , 30000000),
            new LEDProtocal(6, 4, "15:00", "자산 51@2x.png"   , Application.dataPath + "\\tmp\\" + "자산 51@2x.png"     , 30000000),
            new LEDProtocal(5, 4, "15:00", "자산 64@2x.png"   , Application.dataPath + "\\tmp\\" + "자산 64@2x.png"     , 30000000),
            new LEDProtocal(5, 4, "15:00", "자산 67@2x.png"   , Application.dataPath + "\\tmp\\" + "자산 67@2x.png"     , 30000000)
        }; 

        // 정렬은 링큐로 한번에 정렬
        if (isFulRepeat) 
            sortedList = originList.OrderBy(x => x.userSortNum).ThenBy(x => x.originSortNum).ToList();
        else
            sortedList= originList.OrderBy(x => x.VideoStartTime).ThenBy(x => x.originSortNum).ToList();

        foreach (var item in sortedList)
        {
            Debug.Log(item.ToString());
        }
    }

    // LEDFileList|true|3|a,s,d|a,s,d|a,s,d 이런식으로 리턴함.
    private string GetLEDSendMessage(List<LEDProtocal> resultArr)
    {
        string result = Enum.GetName(typeof(ProtocalType), ProtocalType.LEDFileList) + "|"; // 첫번째에 신호의 종류 보내고
        result += isFulRepeat + "|";           // 두번째 블럭은 isfullRepeat
        result += resultArr.Count + "|";           // 세번째 블럭은 리스트의 길이
        for (int i = 0; i < resultArr.Count; i++)  // 네번째 블럭은 각각의 구조체의 요소들
        {
            result += $"{i},{resultArr[i].ToString()}"; // 클라이언트에게 전송 될 때에는 "순서,시작시간,이름.."이렇게 된다.
            if (i != resultArr.Count - 1)
            {
                result += "|";
            }
        }
        return result;
    }

    private void CheckLEDListFile()
    {
        Debug.Log("데이터 전송 : " + GetLEDSendMessage(sortedList));
        multiServer.SendAll(GetLEDSendMessage(sortedList));
    }

    // 파일을 입력하면 전체 경로를 찾아서 알려주는 함수
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
