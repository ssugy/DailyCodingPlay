using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVertHorizon : MonoBehaviour
{
    public ScrollRect scrollView;
    public float vertNormal;
    public float horizonNormal;

    private void Start()
    {
        scrollView.verticalNormalizedPosition = vertNormal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            scrollView.verticalNormalizedPosition = vertNormal;
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            scrollView.horizontalNormalizedPosition = horizonNormal;
        }
    }
}
