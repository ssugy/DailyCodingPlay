using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CMDChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnityEngine.Debug.Log("실행");

            string restartCMD = "shutdown -r -f -t 0";
            ExcuteCMD(restartCMD);
        }  
    }

    private void ExcuteCMD(string command)
    {
        Process process = new Process();

        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/c " + command; // "/c" executes the command and then terminates the command prompt 
                                                       // "/c"는 터미널에서 명령어 치기 위한 필수 인수이다. (안넣으면 이후에 실행이 안된다.)
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        // 데이터 리시브용 델리게이트 - 여기에서 결과물이 출력된다.
        process.OutputDataReceived += (sender, e) => UnityEngine.Debug.Log(e.Data); // Print command output

        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();
        process.Close();
    }
}
