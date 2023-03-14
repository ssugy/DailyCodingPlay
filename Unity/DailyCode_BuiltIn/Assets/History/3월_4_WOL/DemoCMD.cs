using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

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
            OpenCMD();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }

    private void OpenCMD()
    {
        UnityEngine.Debug.Log("시작");

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
        pro.StandardInput.Write(@"shutdown -r -t 0" + Environment.NewLine);
        pro.StandardInput.Close();
        string resultValue = pro.StandardOutput.ReadToEnd();
        pro.WaitForExit();  // 끝날때까지 기다리겠다는 의미.
        pro.Close();
        UnityEngine.Debug.Log("결과물 : " + resultValue);
    }
}
