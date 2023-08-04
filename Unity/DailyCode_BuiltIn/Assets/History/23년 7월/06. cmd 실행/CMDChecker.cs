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
            UnityEngine.Debug.Log("����");

            //string restartCMD = "shutdown -r -f -t 0";
            /**
             * tn : task name (�����ٷ� �̸� - �����ؾߵ�)
             * tr : task run (���� �� ���α׷�)
             * sc : schedule (���� �� ���� - daily, weekly, monthly, once..)
             * st : start time (���۽ð�)
             * ed : end date (���� ��¥, once������ �ϸ�ȵ�.)
             * F : ������ �۾��� �����ϴ� ��� �۾��� ������ ����
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
                                                       // "/c"�� �͹̳ο��� ��ɾ� ġ�� ���� �ʼ� �μ��̴�. (�ȳ����� ���Ŀ� ������ �ȵȴ�.)
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        // ������ ���ú�� ��������Ʈ - ���⿡�� ������� ��µȴ�.
        process.OutputDataReceived += (sender, e) => UnityEngine.Debug.Log(e.Data.ToString()); // Print command output

        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();
        process.Close();
    }
}
