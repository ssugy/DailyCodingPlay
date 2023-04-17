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
    [Serializable]
    public struct LEDProtocal
    {
        public int originSortNum;       // 리스트에 있는 영상의 순서(물리적 순서)
        public int userSortNum;         // 사용자가 지정한 순서
        public string VideoStartTime;   // 영상이 시작되는 시간(isRepeat가 false일 때 적용된다.) HH:MM
        public string fileName;         // 파일명
        public FileType fileType;       // 파일 타입(Video, Img)
        public string fileFullPath;     // 파일의 최종 경로
        public int byteFileSize;        // 파일의 용량(바이트 기준)
        public float vertNormal;        // 버티컬 노말값으로, 해당값이 0.5이면 중앙 정렬, 0이면 위로, 1이면 아래로 간다.

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
        CheckLEDFileList,   // 서버에서 전송하려는 파일이 모두 존재하는지 체크하는 용도의 프로토콜
    }
    // 클라이언트에서 서버로 보낼 때 프로토콜의 타입(파일요청이냐, 다른것이냐 기타 등등.)
    public enum ToServerProtocal
    {
        None,
        RequestFile,    // 파일을 달라는 클라이언트의 요청을 할 때 사용되는 프로토콜
    }
    // 파일타입
    public enum FileType
    {
        Video,
        Img
    }

    public List<LEDProtocal> originList;
    private List<LEDProtocal> sortedList;
    public bool isFulRepeat;        // 전체 반복이냐, 각 영상을 특정 시간이 지나면 틀어지게 할 것이냐에 대한 선택
    public ORTCPMultiServer multiServer;

    // 스타트단에서 결정을 했기 때문에 링큐 적용이 안되는 것 처럼 보였던 것이다.
    private void Start()
    {
        // Json으로 가져오기 - 사용자가 선택하면 바꿀 수 있어야 함.
        /**
         * 세부적인 데이터의 설명은 struct내부에 주석으로 작성하였습니다.
         */
        originList = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "12:00", "test.png"       ,FileType.Img   , Application.dataPath + "/tmp/" + "test.png"           , 10000000, 0.1f),
            new LEDProtocal(2, 2, "11:28", "시퀀스 06.mp4"  ,FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06.mp4"      , 20000000, 0.2f),
            new LEDProtocal(3, 3, "02:00", "시퀀스 06_1.mp4",FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06_1.mp4"    , 30000000, 0.3f),
            new LEDProtocal(4, 4, "03:00", "시퀀스 06_2.mp4",FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06_2.mp4"    , 30000000, 0.4f),
            new LEDProtocal(5, 5, "04:00", "시퀀스 06_3.mp4",FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06_3.mp4"    , 30000000, 0.5f),
            new LEDProtocal(6, 4, "05:00", "자산 51@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "자산 51@2x.png"     , 30000000, 0.6f),
            new LEDProtocal(5, 4, "06:00", "자산 64@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "자산 64@2x.png"     , 30000000, 0.7f),
            new LEDProtocal(5, 4, "07:00", "자산 67@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "자산 67@2x.png"     , 30000000, 0.8f)
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

    // CheckLEDFileList|true|3|a,s,d|a,s,d|a,s,d 이런식으로 리턴된다 => 클라이언트에게 보내진다.
    private string GetLEDSendMessage(List<LEDProtocal> resultArr, bool isAllRepeat)
    {
        string result = Enum.GetName(typeof(ToClientProtocal), ToClientProtocal.CheckLEDFileList) + "|"; // 첫번째에 신호의 종류 보내고
        result += isAllRepeat + "|";           // 두번째 블럭은 isAllRepeat
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

    // 테스트용 전체반복 버튼
    public void CheckLEDListFile(bool isRepeatAll)
    {
        isFulRepeat = isRepeatAll;
        multiServer.SendAll(GetLEDSendMessage(sortedList, isRepeatAll));
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1번키 누름");
            originList.Clear();
            originList = new List<LEDProtocal>
            {
                new LEDProtocal(1, 5, "12:00", "test.png"       ,FileType.Img   , Application.dataPath + "/tmp/" + "test.png"           , 10000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(2, 4, "11:28", "시퀀스 06.mp4"  ,FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06.mp4"      , 20000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(3, 3, "02:00", "시퀀스 06_1.mp4",FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06_1.mp4"    , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(4, 2, "03:00", "시퀀스 06_2.mp4",FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06_2.mp4"    , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(5, 1, "04:00", "시퀀스 06_3.mp4",FileType.Video , Application.dataPath + "/tmp/" + "시퀀스 06_3.mp4"    , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(6, 3, "05:00", "자산 51@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "자산 51@2x.png"     , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(5, 4, "06:00", "자산 64@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "자산 64@2x.png"     , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f)),
                new LEDProtocal(5, 5, "07:00", "자산 67@2x.png" ,FileType.Img   , Application.dataPath + "/tmp/" + "자산 67@2x.png"     , 30000000, UnityEngine.Random.Range(-0.5f, 1.5f))
            };

            if (isFulRepeat)
                sortedList = originList.OrderBy(x => x.userSortNum).ThenBy(x => x.originSortNum).ToList();
            else
                sortedList = originList.OrderBy(x => x.VideoStartTime).ThenBy(x => x.originSortNum).ToList();
        }
    }
}
