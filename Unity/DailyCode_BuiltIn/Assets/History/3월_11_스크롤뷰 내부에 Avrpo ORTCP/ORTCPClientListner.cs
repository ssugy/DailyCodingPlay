using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTCPClientListner : MonoBehaviour
{
    public ScrollViewManager manager;

    private void OnClientConnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientConnect : " + eventParams.message);
    }
    private void OnClientDisconnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientDisconnect : " + eventParams.message);
    }
    private void OnDataReceived(ORTCPEventParams eventParams)
    {
        Debug.Log("OnDataReceived : " + eventParams.message);
    }
    private void OnClientConnectionRefused(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientConnectionRefused : " + eventParams.message);
    }
}
