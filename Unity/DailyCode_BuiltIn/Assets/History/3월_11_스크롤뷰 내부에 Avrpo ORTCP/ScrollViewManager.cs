using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewManager : MonoBehaviour
{
    private static ScrollViewManager unique;
    public static ScrollViewManager instance
    {
        get { return unique; }
    }
    private void Awake()
    {
        unique = this;
    }

    public float dataY;
    ScrollRect scroll;

    private void Start()
    {
        scroll= GetComponent<ScrollRect>();
        scroll.verticalNormalizedPosition = 0.5f;
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

    public void SetVertNormalPos(float vertPos)
    {
        scroll.verticalNormalizedPosition = vertPos;
    }
}
