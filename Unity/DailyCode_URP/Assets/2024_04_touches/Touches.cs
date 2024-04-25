using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Touches : MonoBehaviour
{
    private TESTTouchActions testTouchActions;
    private void Awake()
    {
        testTouchActions = new TESTTouchActions();
    }

    private void OnEnable()
    {
        testTouchActions.Enable();
    }

    private void OnDisable()
    {
        testTouchActions.Disable();
    }

    private void Start()
    {
        testTouchActions.Touch.TouchInput.started += ctx => Test(ctx);

        Touchscreen tc = new Touchscreen();
    }

    private void Test(InputAction.CallbackContext ctx)
    {
        
    }
}
