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
    // 클라이언트에서 서버로 보낼 때 프로토콜의 타입(파일요청이냐, 다른것이냐 기타 등등.)
    public enum ToServerProtocal
    {
        None,
        RequestFile,
    }
    // 파일타입
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

    // 스타트단에서 결정을 했기 때문에 링큐 적용이 안되는 것 처럼 보였던 것이다.
    private void Start()
    {
        btn.onClick.AddListener(CheckLEDListFile);
        // Json으로 가져오기 - 사용자가 선택하면 바꿀 수 있어야 함.
        originList = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "12:00", "test.png"       ,FileType.Img   , Application.dataPath + "\\tmp\\" + "test.png"           , 10000000, 0.5f),
            new LEDProtocal(2, 2, "11:28", "시퀀스 06.mp4"  ,FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06.mp4"      , 20000000, 0.5f),
            new LEDProtocal(3, 3, "02:00", "시퀀스 06_1.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06_1.mp4"    , 30000000, 0.5f),
            new LEDProtocal(4, 4, "03:00", "시퀀스 06_2.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06_2.mp4"    , 30000000, 0.5f),
            new LEDProtocal(5, 5, "04:00", "시퀀스 06_3.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06_3.mp4"    , 30000000, 0.5f),
            new LEDProtocal(6, 4, "05:00", "자산 51@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "자산 51@2x.png"     , 30000000, 0.5f),
            new LEDProtocal(5, 4, "06:00", "자산 64@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "자산 64@2x.png"     , 30000000, 0.5f),
            new LEDProtocal(5, 4, "07:00", "자산 67@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "자산 67@2x.png"     , 30000000, 0.5f)
        };

        // 정렬은 링큐로 한번에 정렬
        if (isFulRepeat)
            sortedList = originList.OrderBy(x => x.userSortNum).ThenBy(x => x.originSortNum).ToList();
        else
            sortedList = originList.OrderBy(x => x.VideoStartTime).ThenBy(x => x.originSortNum).ToList();

        foreach (var item in sortedList)
        {
            Debug.Log(item.ToString());
        }
    }

    // LEDFileList|true|3|a,s,d|a,s,d|a,s,d 이런식으로 리턴함.
    private string GetLEDSendMessage(List<LEDProtocal> resultArr)
    {
        string result = Enum.GetName(typeof(ToClientProtocal), ToClientProtocal.CheckLEDFileList) + "|"; // 첫번째에 신호의 종류 보내고
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
        Debug.Log(result);
        return result;
    }

    private void CheckLEDListFile()
    {
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
