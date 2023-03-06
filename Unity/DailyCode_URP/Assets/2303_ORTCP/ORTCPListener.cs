using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTCPListener : MonoBehaviour
{
    private void OnClientConnect()
    {
        Debug.Log("OnClientConnect �����");
    }

    private void OnClientDisconnect()
    {
        Debug.Log("OnClientDisconnect �����");
    }

    public void OnDataReceived(ORTCPEventParams eventParams)
    {
        Debug.Log("OnDataReceived ����� : " + eventParams.packet.bytesCount);
    }

    private void OnClientConnectionRefused()
    {
        Debug.Log("OnClientConnectionRefused �����");
    }
}
