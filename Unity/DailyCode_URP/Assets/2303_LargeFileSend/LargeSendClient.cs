using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class LargeSendClient : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Init();
        }
    }

    private void Init()
    {
        string IPAddress = "192.168.0.26";
        int Port = 500;

        //string Filename = @"C:\TestVideo.mp4";
        string Filename = Application.dataPath + "/2303_LargeFileSend/SendFolder/" + "TestVideo.mp4";


        int bufferSize = 1024;
        byte[] buffer = null;
        byte[] header = null;


        FileStream fs = new FileStream(Filename, FileMode.Open);
        bool read = true;

        int bufferCount = Convert.ToInt32(Math.Ceiling((double)fs.Length / (double)bufferSize));



        TcpClient tcpClient = new TcpClient(IPAddress, Port);
        
        tcpClient.SendTimeout = 600000; // 10Ка
        tcpClient.ReceiveTimeout = 600000;

        //string headerStr = "Content-length:" + fs.Length.ToString() + "\r\nFilename:" + @"C:\Users\Administrator\Desktop\" + "test.mp4\r\n";
        string headerStr = "Content-length:" + fs.Length.ToString() + "\r\nFilename:" + Filename + "\r\n";
        header = new byte[bufferSize];
        Array.Copy(Encoding.ASCII.GetBytes(headerStr), header, Encoding.ASCII.GetBytes(headerStr).Length);

        tcpClient.Client.Send(header);

        for (int i = 0; i < bufferCount; i++)
        {
            buffer = new byte[bufferSize];
            int size = fs.Read(buffer, 0, bufferSize);

            tcpClient.Client.Send(buffer, size, SocketFlags.Partial);

        }

        tcpClient.Client.Close();

        fs.Close();
    }
}
