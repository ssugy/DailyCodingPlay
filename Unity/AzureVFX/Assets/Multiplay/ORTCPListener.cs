using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTCPListener : MonoBehaviour
{
    private void OnClientConnect(ORTCPEventParams oRTCPEvent)
    {
        Debug.Log("OnClientConnect : " + oRTCPEvent);
    }
    private void OnClientDisconnect(ORTCPEventParams oRTCPEvent)
    {
        Debug.Log("OnClientDisconnect : " + oRTCPEvent);
    }
    private void OnDataReceived(ORTCPEventParams oRTCPEvent)
    {
        Debug.Log("OnDataReceived : " + oRTCPEvent.message);
    }
    private void OnClientConnectionRefused(ORTCPEventParams oRTCPEvent)
    {
        Debug.Log("OnClientConnectionRefused : " + oRTCPEvent);
    }
}