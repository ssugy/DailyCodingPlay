using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    private FileTransfer fileTransfer;

    public void OnServerConnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnServerConnect 데이터");
    }

    public void OnServerDisconnect(ORTCPEventParams eventParams)
    {

        Debug.Log("OnServerDisconnect 데이터");
    }

    public void OnDataReceived(ORTCPEventParams eventParams)
    {
        Debug.Log($"OnDataReceived 데이터 : {eventParams.message}");

        string msg = eventParams.message;
        string msgType = msg.Split('|')[0];
        switch (msgType)
        {
            case "None":
                break;
            case "RequestMissingFile":
                Debug.Log($"파일요청이 들어옴 {eventParams.socket.Client.RemoteEndPoint.ToString()}");
                fileTransfer = new FileTransfer(50001,FileTransfer.ETransfer.Upload, eventParams.socket.Client.RemoteEndPoint.ToString().Split(':')[0]);

                string[] tmpList = msg.Split('|');
                string[] fileList = new string[tmpList.Length -1];  // 맨앞제거한 리스트를 얻기 위해서 크기 1개 줄임
                Array.Copy(tmpList, 1, fileList, 0, fileList.Length);
                for (int i = 0; i < fileList.Length; i++)
                {
                    fileList[i] = LEDFileCheckServer.instance.GetFileFullPath(fileList[i]);
                }

                fileTransfer.Upload(fileList, '\\');
                break;
        }
    }

    public void OnClientConnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientConnect 데이터");
    }

    public void OnClientDisconnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientDisconnect 데이터");
    }

    public void OnClientConnectionRefused(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientConnectionRefused 데이터");
    }

    ///// ORTCP 기능확인
    public ORTCPMultiServer multiServer;
    

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("1번키 누름");
            //byte[] bytes = new byte[5000];
            //bytes.SetValue((byte)1, 0);
            //Debug.Log(bytes[500]);
            //server.SendBytes(bytes);
        }
    }
}
