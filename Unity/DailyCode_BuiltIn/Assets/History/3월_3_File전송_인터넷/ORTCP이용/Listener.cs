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

}
