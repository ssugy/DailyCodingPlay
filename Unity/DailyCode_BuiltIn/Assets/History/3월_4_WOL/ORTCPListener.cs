using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTCPListener : MonoBehaviour
{
    public void OnClientConnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientConnect : " + eventParams.message);
    }

    public void OnClientDisconnect(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientDisconnect : " + eventParams.message);
    }

    public void OnDataReceived(ORTCPEventParams eventParams)
    {
        Debug.Log("OnDataReceived : " + eventParams.message);
        cmd.OpenCMD(eventParams.message);
    }

    public void OnClientConnectionRefused(ORTCPEventParams eventParams)
    {
        Debug.Log("OnClientConnectionRefused : " + eventParams.message);
    }

    public DemoCMD cmd;
    
    
}
