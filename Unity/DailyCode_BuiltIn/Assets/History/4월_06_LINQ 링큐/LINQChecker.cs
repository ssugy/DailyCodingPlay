using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LINQChecker : MonoBehaviour
{
    struct TEST
    {
        public string test;
        public TEST(string str)
        {
            test= str;
        }
    }

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
            userSortNum = userNum;
            VideoStartTime = videoStartTime;
            fileName = mFileName;
            fileType = type;
            fileFullPath = mFileFullPath;
            byteFileSize = mByteFileSize;
            vertNormal = normal;
        }

        public override string ToString() => $"{VideoStartTime},{fileName},{fileType},{byteFileSize},{vertNormal}";
        public void ShowAll() => Debug.Log($"{originSortNum},{userSortNum},{VideoStartTime},{fileName},{fileType},{byteFileSize},{vertNormal}");
    }
    public enum FileType
    {
        Video,
        Img
    }

    List<string> list = new List<string>();
    List<LEDProtocal> list2;    
    void Start()
    {
        list.Add("12:01");
        list.Add("52:01");
        list.Add("22:01");
        list.Add("42:01");
        list.Add("32:01");
        list.Add("62:01");
        list.Add("82:01");
        list.Add("72:01");
        list.Add("92:01");
        list.Add("10:01");

        foreach (string item in list)
        {
            Debug.Log(item);
        }

        Debug.Log("-----------------------");
        List<string> nextList = list.OrderBy(x => x).ToList();
        foreach (string item in nextList) 
        {
            Debug.Log(item);
        }
        Debug.Log("-----------------------");
        list2 = new List<LEDProtocal>
        {
            new LEDProtocal(1, 1, "00:00", "test.png"       ,FileType.Img   , Application.dataPath + "\\tmp\\" + "test.png"           , 10000000, 0.5f),
            new LEDProtocal(2, 2, "11:28", "시퀀스 06.mp4"  ,FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06.mp4"      , 20000000, 0.5f),
            new LEDProtocal(3, 3, "02:00", "시퀀스 06_1.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06_1.mp4"    , 30000000, 0.5f),
            new LEDProtocal(4, 4, "03:00", "시퀀스 06_2.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06_2.mp4"    , 30000000, 0.5f),
            new LEDProtocal(5, 5, "04:00", "시퀀스 06_3.mp4",FileType.Video , Application.dataPath + "\\tmp\\" + "시퀀스 06_3.mp4"    , 30000000, 0.5f),
            new LEDProtocal(6, 4, "05:00", "자산 51@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "자산 51@2x.png"     , 30000000, 0.5f),
            new LEDProtocal(5, 4, "06:00", "자산 64@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "자산 64@2x.png"     , 30000000, 0.5f),
            new LEDProtocal(5, 4, "07:00", "자산 67@2x.png" ,FileType.Img   , Application.dataPath + "\\tmp\\" + "자산 67@2x.png"     , 30000000, 0.5f)
        };
        List<LEDProtocal> list3 = list2.OrderBy(x => x.VideoStartTime).ToList();
        foreach (var item in list3)
        {
            Debug.Log("정렬 후 : " + item.VideoStartTime);
        }
    }
}
