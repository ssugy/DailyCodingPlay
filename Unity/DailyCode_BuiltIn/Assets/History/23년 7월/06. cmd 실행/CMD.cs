using AForge.Genetic;
using System;
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

public enum DayOfWeekShort
{
    Sun = 0,
    Mon,
    Tue,
    Wed,
    Thu,
    Fri,
    Sat,
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
    public static void MakeScheduler(Schedule type, string OffTime, string day)
    {
        string query = string.Empty;
        switch (type)
        {
            case Schedule.Disable:
                // ������ 1���� �ۿ� �ȵȴ�.
                query = $"chcp 65001 && schtasks /delete /tn \"CMS\" /F";
                break;
            case Schedule.Daily:
                query = $"chcp 65001 && schtasks /create /tn \"CMS\" /tr \"C:\\Windows\\System32\\shutdown.exe  -s -f -t 0\" /sc daily /st {OffTime} /F";
                break;
            case Schedule.Weekly:
                query = $"chcp 65001 && schtasks /create /tn \"CMS\" /tr \"C:\\Windows\\System32\\shutdown.exe  -s -f -t 0\" /sc weekly /d {day} /st {OffTime} /F";
                break;
            case Schedule.Once:
                // once�� Ư�� ��¥�� �������� �ؾ��ϴµ�, Ư�� ���Ϸ� �Ǿ�����.
                // ������ ���� �� ���̸�ŭ ��¥�� ���ؾ� �Ѵ�.
                UnityEngine.Debug.Log((int)Enum.Parse(typeof(DayOfWeekShort), day));
                int today = (int)DateTime.Now.DayOfWeek;
                int OnceDay = (int)Enum.Parse(typeof(DayOfWeekShort), day); // �̰� ������ �ڿ� �ִ� �����̴�.
                int offset = (OnceDay - today) < 0 ? OnceDay - today + 7 : OnceDay - today;
                string onceDate = DateTime.Now.AddDays(offset).ToString("yyyy/MM/dd");

                query = $"chcp 65001 && schtasks /create /tn \"CMS_Once\" /tr \"C:\\Windows\\System32\\shutdown.exe  -s -f -t 0\" /sc once /sd {onceDate} /st {OffTime} /F";
                break;
        }

        ExcuteCMD(query);
    }

    // �׽�Ʈ��
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CMD.MakeScheduler(Schedule.Disable, "10:12", "Mon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CMD.MakeScheduler(Schedule.Daily, "10:12", "Mon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CMD.MakeScheduler(Schedule.Once, "14:12", "Fri");
        }
    }
}
