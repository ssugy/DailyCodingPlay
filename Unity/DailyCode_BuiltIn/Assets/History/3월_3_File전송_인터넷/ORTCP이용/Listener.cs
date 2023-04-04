using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public void OnServerConnect()
    {
        Debug.Log("OnServerConnect 데이터");
    }

    public void OnServerDisconnect()
    {

        Debug.Log("OnServerDisconnect 데이터");
    }

    public void OnDataReceived()
    {
        Debug.Log("OnDataReceived 데이터");
    }

    public void OnClientConnect()
    {
        Debug.Log("OnClientConnect 데이터");
    }

    public void OnClientDisconnect()
    {
        Debug.Log("OnClientDisconnect 데이터");
    }

    public void OnClientConnectionRefused()
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
