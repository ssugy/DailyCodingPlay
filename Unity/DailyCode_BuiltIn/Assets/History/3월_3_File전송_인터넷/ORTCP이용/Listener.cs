using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public void OnServerConnect()
    {
        Debug.Log("OnServerConnect ������");
    }

    public void OnServerDisconnect()
    {

        Debug.Log("OnServerDisconnect ������");
    }

    public void OnDataReceived()
    {
        Debug.Log("OnDataReceived ������");
    }

    public void OnClientConnect()
    {
        Debug.Log("OnClientConnect ������");
    }

    public void OnClientDisconnect()
    {
        Debug.Log("OnClientDisconnect ������");
    }

    public void OnClientConnectionRefused()
    {
        Debug.Log("OnClientConnectionRefused ������");
    }

    ///// ORTCP ���Ȯ��
    public ORTCPMultiServer multiServer;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("1��Ű ����");
            //byte[] bytes = new byte[5000];
            //bytes.SetValue((byte)1, 0);
            //Debug.Log(bytes[500]);
            //server.SendBytes(bytes);
        }
    }
}
