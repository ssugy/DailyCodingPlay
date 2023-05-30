using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFilesNameAndByte : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string path = Application.dataPath + "\\tmp\\";
            GetFiles(path);
        }
    }

    private void GetFiles(string path)
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
        foreach (System.IO.FileInfo File in di.GetFiles())
        {
            if (File.Extension.ToLower().CompareTo(".png") == 0 || File.Extension.ToLower().CompareTo(".mp4") == 0)
            {
                string FileNameExtension = File.Name;
                string FileNameOnly = File.Name.Substring(0, File.Name.Length - 4); // 확장자 제거한 이름
                string FullFileName = File.FullName;    // 경로포함 풀네임
                Debug.Log(FullFileName + "||" + FileNameExtension + "||" + FileNameOnly + "||파일사이즈 : " + File.Length); // 파일사이즈는 파일인포의 length를 통해서 확인이 가능하다.
            }
        }
    }
}
