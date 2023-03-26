using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using System.Text;

public class DemoCMD : MonoBehaviour
{
    ProcessStartInfo proInfo;
    Process pro;

    private void Start()
    {
        proInfo = new ProcessStartInfo();
        pro = new Process();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OpenCMD("ipconfig");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }

    public void OpenCMD(string commandStr = "")
    {
        UnityEngine.Debug.Log("시작");

        //인코딩 세팅 - 기본은 UTF-8
        //proInfo.StandardOutputEncoding = Encoding.Unicode;
        // 실행할 파일명 입력 -- cmd
        proInfo.FileName = @"cmd";
        // cmd창 띄우기 - true(띄우지 않기), false(띄우기)
        proInfo.CreateNoWindow = false;
        proInfo.UseShellExecute = false;
        // cmd 데이터 받기
        proInfo.RedirectStandardOutput = true;
        // cmd 데이터 보내기
        proInfo.RedirectStandardInput = true;
        // cmd 오류내용 받기
        proInfo.RedirectStandardError = true;

        pro.StartInfo = proInfo;
        pro.Start();

        // 명령어 입력
        //pro.StandardInput.Write(@"shutdown -r -t 0" + Environment.NewLine);
        //pro.StandardInput.Write("chcp 65001" + Environment.NewLine);    // 인코딩 바꾸고. 바꿔도 어차피 한글은 안됨
        pro.StandardInput.Write(commandStr + Environment.NewLine);
        pro.StandardInput.Close();

        //UnityEngine.Debug.Log("인코딩 : " + pro.StandardOutput.CurrentEncoding);
        string resultValue = pro.StandardOutput.ReadToEnd();
        pro.WaitForExit();  // 끝날때까지 기다리겠다는 의미.
        pro.Close();
        UnityEngine.Debug.Log("결과물 : " + resultValue);
    }
}
