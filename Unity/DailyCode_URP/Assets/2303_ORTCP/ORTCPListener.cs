using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTCPListener : MonoBehaviour
{
    private void OnClientConnect()
    {
        Debug.Log("OnClientConnect ¿¬°áµÊ");
    }

    private void OnClientDisconnect()
    {
        Debug.Log("OnClientDisconnect ¿¬°áµÊ");
    }

    private void OnDataReceived()
    {
        Debug.Log("OnDataReceived ¿¬°áµÊ");
    }

    private void OnClientConnectionRefused()
    {
        Debug.Log("OnClientConnectionRefused ¿¬°áµÊ");
    }
}
