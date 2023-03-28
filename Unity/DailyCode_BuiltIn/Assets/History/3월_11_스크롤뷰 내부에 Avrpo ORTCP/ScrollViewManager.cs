using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewManager : MonoBehaviour
{
    public float dataY;
    ScrollRect scroll;

    private void Start()
    {
        scroll= GetComponent<ScrollRect>();
    }

    public void OnValueChange(Vector2 vect2)
    {
        dataY= vect2.y;
        Debug.Log(dataY);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1)) 
        {
            Debug.Log(scroll.verticalNormalizedPosition);
            scroll.verticalNormalizedPosition = dataY;  // 이렇게 적용가능.
        }
    }
}
