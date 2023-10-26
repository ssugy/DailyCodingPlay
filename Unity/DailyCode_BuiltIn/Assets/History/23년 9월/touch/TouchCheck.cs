using AForge.Genetic;
using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchCheck : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (Vector2.Distance(touch.position, img.rectTransform.position) < img.rectTransform.sizeDelta.x + 200)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        Debug.Log("1 ����");
                        //img.transform.position = touch.position;
                        break;
                    case TouchPhase.Moved:
                        Debug.Log("1 move");
                        //img.transform.position = touch.position;
                        img.rectTransform.anchoredPosition += touch.deltaPosition;
                        break;
                    //case TouchPhase.Stationary:
                    //    Debug.Log("1 stationary");
                    //    break;
                    case TouchPhase.Ended:
                        Debug.Log("1 ended");
                        break;
                    case TouchPhase.Canceled:
                        Debug.Log("1 calceled");
                        break;
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);       // �ߺ��ɰŰ�����?
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                Debug.Log("2��ġ �����ϳ� ����");
                //img.transform.position = touch0.position; //2��ġ�� �̵��� ������ �ƴ�
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                // ���� �ϳ��� �հ����� �����̸� -> ũ�⸦ �����ؾ� ��. 
                Debug.Log("�� �� �ϳ��� ������");
                img.rectTransform.sizeDelta = Vector2.one * Vector2.Distance(touch0.position, touch1.position);
            }


            //switch (touch0.phase)
            //{
            //    case TouchPhase.Began:
            //        Debug.Log("2-1 ����");
            //        break;
            //    case TouchPhase.Moved:
            //        Debug.Log("2-1 move");
            //        break;
            //    //case TouchPhase.Stationary:
            //    //    Debug.Log("2-1 stationary");
            //    //    break;
            //    case TouchPhase.Ended:
            //        Debug.Log("2-1 ended");
            //        break;
            //    case TouchPhase.Canceled:
            //        Debug.Log("2-1 calceled");
            //        break;
            //}

            //switch (touch1.phase)
            //{
            //    case TouchPhase.Began:
            //        Debug.Log("2-2 ����");
            //        break;
            //    case TouchPhase.Moved:
            //        Debug.Log("2-2 move");
            //        break;
            //    //case TouchPhase.Stationary:
            //    //    Debug.Log("2-2 stationary");
            //    //    break;
            //    case TouchPhase.Ended:
            //        Debug.Log("2-2 ended");
            //        break;
            //    case TouchPhase.Canceled:
            //        Debug.Log("2-2 calceled");
            //        break;
            //}
        }
    }
}
