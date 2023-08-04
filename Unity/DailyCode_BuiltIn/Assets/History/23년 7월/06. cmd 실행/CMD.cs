using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public enum Schedule
{
    Disable,        // ������ ������� ����
    Daily,          // 1�ϴ��� �ݺ�
    Weekly,         // �ִ��� �ݺ�
    Once
}

public class CMD : MonoBehaviour
{
    private static void ExcuteCMD(string command)
    {
        Process process = new Process();

        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/c " + command; // "/c" executes the command and then terminates the command prompt 
                                                       // "/c"�� �͹̳ο��� ��ɾ� ġ�� ���� �ʼ� �μ��̴�. (�ȳ����� ���Ŀ� ������ �ȵȴ�.)
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        // ������ ���ú�� ��������Ʈ - ���⿡�� ������� ��µȴ�.
        process.OutputDataReceived += (sender, e) => UnityEngine.Debug.Log(e.Data); // Print command output

        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();
        process.Close();
    }

    //-----------------------------------------------
    // PC Off
    public static void PCOff()
    {
        string restartCMD = "shutdown -s -f -t 0";
        ExcuteCMD(restartCMD);
    }
    
    // PC Restart ����
    public static void PCRestart()
    {
        string restartCMD = "shutdown -r -f -t 0";
        ExcuteCMD(restartCMD);
    }

    // �����ٷ� ���� ����
    /**
    * tn : task name (�����ٷ� �̸� - �����ؾߵ�)
    * tr : task run (���� �� ���α׷�)
    * sc : schedule (���� �� ���� - daily, weekly, monthly, once..)
    * st : start time (���۽ð�)
    * ed : end date (���� ��¥, once������ �ϸ�ȵ�.)
    * F : ������ �۾��� �����ϴ� ��� �۾��� ������ ����
    */
    
}
