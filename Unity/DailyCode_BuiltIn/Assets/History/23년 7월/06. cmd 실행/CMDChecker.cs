using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CMDChecker : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnityEngine.Debug.Log("실행");

            //string restartCMD = "shutdown -r -f -t 0";
            /**
             * tn : task name (스케줄러 이름 - 고유해야됨)
             * tr : task run (실행 할 프로그램)
             * sc : schedule (일정 빈도 지정 - daily, weekly, monthly, once..)
             * st : start time (시작시간)
             * ed : end date (종료 날짜, once에서는 하면안됨.)
             * F : 지정된 작업이 존재하는 경우 작업을 강제로 만듬
             */
            string restartCMD = "chcp 65001 && schtasks /create /tn \"CMS\" /tr C:\\Users\\DevYH\\Pictures\\test\\BasicProject.exe /sc daily /st 11:59 /ed 2023/08/10 /F";
            ExcuteCMD(restartCMD);
        }  
    }

    private void ExcuteCMD(string command)
    {
        Process process = new Process();
        process.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;

        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/c " + command; // "/c" executes the command and then terminates the command prompt 
                                                       // "/c"는 터미널에서 명령어 치기 위한 필수 인수이다. (안넣으면 이후에 실행이 안된다.)
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        // 데이터 리시브용 델리게이트 - 여기에서 결과물이 출력된다.
        process.OutputDataReceived += (sender, e) => UnityEngine.Debug.Log(e.Data.ToString()); // Print command output

        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();
        process.Close();
    }
}
