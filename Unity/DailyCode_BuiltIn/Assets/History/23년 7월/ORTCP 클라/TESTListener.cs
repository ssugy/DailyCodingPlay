using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTListener : MonoBehaviour
{
    public ORTCPClient oRTCPClient;

    private void OnClientConnect(ORTCPEventParams @params)
    {
        Debug.Log("OnClientConnect " + @params.message);
    }
    private void OnClientDisconnect(ORTCPEventParams @params)
    {
        Debug.Log("OnClientDisconnect " + @params.message);
    }
    private void OnDataReceived(ORTCPEventParams @params)
    {
        Debug.Log("OnDataReceived " + @params.message);
    }
    private void OnClientConnectionRefused(ORTCPEventParams @params)
    {
        Debug.Log("OnClientConnectionRefused " + @params.message);
    }
}
