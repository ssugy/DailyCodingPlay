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
            UnityEngine.Debug.Log("����");

            string restartCMD = "shutdown -r -f -t 0";
            ExcuteCMD(restartCMD);
        }  
    }

    private void ExcuteCMD(string command)
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
}
