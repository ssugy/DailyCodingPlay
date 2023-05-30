using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTCPClientListner : MonoBehaviour
{
    public ScrollViewManager manager;

    float vertNormalPos;

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
        vertNormalPos = float.Parse(eventParams.message);
        Debug.Log("OnDataReceived : " + eventParams.message);
        ScrollViewManager.instance.SetVertNormalPos(vertNormalPos);
    }
    private void OnClientConnectionRefused(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientConnectionRefused : " + eventParams.message);
    }
}
