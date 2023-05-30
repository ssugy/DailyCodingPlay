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
                string FileNameOnly = File.Name.Substring(0, File.Name.Length - 4); // Ȯ���� ������ �̸�
                string FullFileName = File.FullName;    // ������� Ǯ����
                Debug.Log(FullFileName + "||" + FileNameExtension + "||" + FileNameOnly + "||���ϻ����� : " + File.Length); // ���ϻ������ ���������� length�� ���ؼ� Ȯ���� �����ϴ�.
            }
        }
    }
}
