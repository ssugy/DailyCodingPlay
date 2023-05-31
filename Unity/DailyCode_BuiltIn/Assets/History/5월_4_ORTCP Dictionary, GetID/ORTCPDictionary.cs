using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ORTCPDictionary : MonoBehaviour
{
    private void OnServerConnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnServerConnect 角青");
    }

    private void OnServerDisconnect(ORTCPEventParams eventParams) 
    {
        Debug.Log("OnServerDisconnect 角青");
    }

    private void OnDataReceived(ORTCPEventParams eventParams)
    {
        Debug.Log("OnDataReceived 角青");
    }

    public ORTCPMultiServer server; // 流立 瘤沥

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            server.Send("127.0.0.1", "test");
            //DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/../");
            //foreach (var item in dir.GetFiles())
            //{
            //    Debug.Log(item);
            //}
        }
    }
}
