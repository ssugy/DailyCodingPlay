using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using System.Text;

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
            OpenCMD("ipconfig");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }

    public void OpenCMD(string commandStr = "")
    {
        UnityEngine.Debug.Log("����");

        //���ڵ� ���� - �⺻�� UTF-8
        //proInfo.StandardOutputEncoding = Encoding.Unicode;
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
        //pro.StandardInput.Write(@"shutdown -r -t 0" + Environment.NewLine);
        //pro.StandardInput.Write("chcp 65001" + Environment.NewLine);    // ���ڵ� �ٲٰ�. �ٲ㵵 ������ �ѱ��� �ȵ�
        pro.StandardInput.Write(commandStr + Environment.NewLine);
        pro.StandardInput.Close();

        //UnityEngine.Debug.Log("���ڵ� : " + pro.StandardOutput.CurrentEncoding);
        string resultValue = pro.StandardOutput.ReadToEnd();
        pro.WaitForExit();  // ���������� ��ٸ��ڴٴ� �ǹ�.
        pro.Close();
        UnityEngine.Debug.Log("����� : " + resultValue);
    }
}
