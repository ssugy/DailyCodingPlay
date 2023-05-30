using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTCPDictionary : MonoBehaviour
{
    private void OnServerConnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnServerConnect ����");
    }

    private void OnServerDisconnect(ORTCPEventParams eventParams) 
    {
        Debug.Log("OnServerDisconnect ����");
    }

    private void OnDataReceived(ORTCPEventParams eventParams)
    {
        Debug.Log("OnDataReceived ����");
    }

    public ORTCPMultiServer server; // ���� ����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            server.Send("127.0.0.1","test");
        }
    }
}
