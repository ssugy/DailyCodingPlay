using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Xml;


public enum Schedule
{
    Disable,        // 스케줄 사용하지 않음
    Daily,          // 1일단위 반복
    Weekly,         // 주단위 반복
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
                                                       // "/c"는 터미널에서 명령어 치기 위한 필수 인수이다. (안넣으면 이후에 실행이 안된다.)
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

    // PC Restart 실행
    public static void PCRestart()
    {
        string restartCMD = "shutdown -r -f -t 0";
        ExcuteCMD(restartCMD);
    }

    // 스케줄러 파일 있는지 확인
    public void CheckSchTask()
    {

    }

    // 스케줄러 설정 실행
    /**
    * tn : task name (스케줄러 이름 - 고유해야됨)
    * tr : task run (실행 할 프로그램)
    * sc : schedule (일정 빈도 지정 - daily, weekly, monthly, once..)
    * st : start time (시작시간)
    * ed : end date (종료 날짜, once에서는 하면안됨.)
    * F : 지정된 작업이 존재하는 경우 작업을 강제로 만듬
    */
    public static void MakeScheduler(Schedule type, string OffTime, string day)
    {
        string query = string.Empty;
        switch (type)
        {
            case Schedule.Disable:
                // 삭제는 1개씩 밖에 안된다.
                query = $"chcp 65001 && schtasks /delete /tn \"CMS\" /F";
                break;
            case Schedule.Daily:
                query = $"chcp 65001 && schtasks /create /tn \"CMS\" /tr \"C:\\Windows\\System32\\shutdown.exe  -s -f -t 0\" /sc daily /st {OffTime} /F";
                break;
            case Schedule.Weekly:
                query = $"chcp 65001 && schtasks /create /tn \"CMS\" /tr \"C:\\Windows\\System32\\shutdown.exe  -s -f -t 0\" /sc weekly /d {day} /st {OffTime} /F";
                break;
            case Schedule.Once:
                // once는 특정 날짜를 기준으로 해야하는데, 특정 요일로 되어있음.
                // 요일을 보고 그 차이만큼 날짜를 더해야 한다.
                UnityEngine.Debug.Log((int)Enum.Parse(typeof(DayOfWeekShort), day));
                int today = (int)DateTime.Now.DayOfWeek;
                int OnceDay = (int)Enum.Parse(typeof(DayOfWeekShort), day); // 이게 무조건 뒤에 있는 개념이다.
                int offset = (OnceDay - today) < 0 ? OnceDay - today + 7 : OnceDay - today;
                string onceDate = DateTime.Now.AddDays(offset).ToString("yyyy/MM/dd");

                query = $"chcp 65001 && schtasks /create /tn \"CMS_Once\" /tr \"C:\\Windows\\System32\\shutdown.exe  -s -f -t 0\" /sc once /sd {onceDate} /st {OffTime} /F";
                break;
        }

        ExcuteCMD(query);
    }

    // 테스트용
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
                        CMS_msg += $"{xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[1].Substring(0, 5)}";  //시간추가
                    }
                    else if (schType.Equals("ScheduleByWeek"))
                    {
                        // Weekly|Monday|01:06:00
                        CMS_msg += $"Weekly|";
                        CMS_msg += $"{xml.GetElementsByTagName("DaysOfWeek")[0].ChildNodes[0].Name.Substring(0,3)}|";
                        CMS_msg += $"{xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[1].Substring(0, 5)}";  //시간추가
                    }
                }
                else if (schType.Equals("TimeTrigger"))
                {
                    // once인경우 들어옴 - Once|2023-08-22|01:07:00
                    CMS_msg += "Once|";
                    CMS_msg += xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[0] + "|";
                    CMS_msg += xml.GetElementsByTagName("StartBoundary")[0].InnerText.Split('T')[1].Substring(0, 5);
                }
            }
            catch (Exception e)
            {
                // 루트 엘리멘탈 없다고 뜸 - 파일이 없는 경우
                CMS_msg = string.Empty;
                UnityEngine.Debug.Log("스케줄러 존재하지 않음 : " + e.Message);
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
