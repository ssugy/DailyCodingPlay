using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Xml;


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
    static string result;

    private static void ExcuteCMD(string command)
    {
        result = string.Empty;
        Process process = new Process();

        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/c " + command; // "/c" executes the command and then terminates the command prompt 
                                                       // "/c"�� �͹̳ο��� ��ɾ� ġ�� ���� �ʼ� �μ��̴�. (�ȳ����� ���Ŀ� ������ �ȵȴ�.)
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.OutputDataReceived += (sender, e) => result += e.Data;
        
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

    // �����ٷ� ���� �ִ��� Ȯ��
    public void CheckSchTask()
    {

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
            string CMS_msg = string.Empty;
            try
            {
                ExcuteCMD("schtasks /query /tn cms /xml");
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(result);
                string schType = xml.GetElementsByTagName("Triggers")[0].ChildNodes[0].Name;
                if (schType.Equals("CalendarTrigger"))
                {
                    schType = xml.GetElementsByTagName("CalendarTrigger")[0].ChildNodes[1].Name;
                    if (schType.Equals("ScheduleByDay"))
                    {
                        // Daily|none|23:10:00
                        CMS_msg += $"Daily|";
                        CMS_msg += "none|";
                        CMS_msg += $"{xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[1].Substring(0, 5)}";  //�ð��߰�
                    }
                    else if (schType.Equals("ScheduleByWeek"))
                    {
                        // Weekly|Monday|01:06:00
                        CMS_msg += $"Weekly|";
                        CMS_msg += $"{xml.GetElementsByTagName("DaysOfWeek")[0].ChildNodes[0].Name.Substring(0,3)}|";
                        CMS_msg += $"{xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[1].Substring(0, 5)}";  //�ð��߰�
                    }
                }
                else if (schType.Equals("TimeTrigger"))
                {
                    // once�ΰ�� ���� - Once|2023-08-22|01:07:00
                    CMS_msg += "Once|";
                    CMS_msg += xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[0] + "|";
                    CMS_msg += xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[1].Substring(0, 5);
                }
            }
            catch (Exception e)
            {
                // ��Ʈ ������Ż ���ٰ� �� - ������ ���� ���
                CMS_msg = string.Empty;
                UnityEngine.Debug.Log("�����ٷ� �������� ���� : " + e.Message);
            }
            UnityEngine.Debug.Log(CMS_msg);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //CMD.MakeScheduler(Schedule.Daily, "10:12", "Mon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //CMD.MakeScheduler(Schedule.Once, "14:12", "Fri");
        }
    }
}
