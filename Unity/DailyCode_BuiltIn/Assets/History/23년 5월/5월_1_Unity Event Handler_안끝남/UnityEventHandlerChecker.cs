using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventHandlerChecker : MonoBehaviour
{
    public event EventHandler<CheckEventArgsClass> someEventHandler;
    public EventArgs someEventArgs;

    private void Start()
    {
        if (someEventHandler != null)
        {
            someEventHandler.Invoke(this, new CheckEventArgsClass("test"));   // �̺�Ʈ �߰��ϱ�.
        }
    }
  
}

public class CheckEventArgsClass
{
    public void ShowShorCut()
    {

    }

    public CheckEventArgsClass(string str)
    {
       
    }
}
