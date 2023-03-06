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

    private void OnDataReceived()
    {
        Debug.Log("OnDataReceived �����");
    }

    private void OnClientConnectionRefused()
    {
        Debug.Log("OnClientConnectionRefused �����");
    }
}
