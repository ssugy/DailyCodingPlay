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
        UnityEngine.Debug.Log("����");

        // ������ ���ϸ� �Է� -- cmd
        proInfo.FileName = @"cmd";
        // cmdâ ���� - true(����� �ʱ�), false(����)
        proInfo.CreateNoWindow = false;
        proInfo.UseShellExecute = false;
        // cmd ������ �ޱ�
        proInfo.RedirectStandardOutput = true;
        // cmd ������ ������
        proInfo.RedirectStandardInput = true;
        // cmd �������� �ޱ�
        proInfo.RedirectStandardError = true;

        pro.StartInfo = proInfo;
        pro.Start();

        // ��ɾ� �Է�
        pro.StandardInput.Write(@"shutdown -r -t 0" + Environment.NewLine);
        pro.StandardInput.Close();
        string resultValue = pro.StandardOutput.ReadToEnd();
        pro.WaitForExit();  // ���������� ��ٸ��ڴٴ� �ǹ�.
        pro.Close();
        UnityEngine.Debug.Log("����� : " + resultValue);
    }
}
