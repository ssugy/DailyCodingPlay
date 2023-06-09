using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingText : MonoBehaviour
{
    public Transform firstTrans;
    public Transform lastTrans;
    public float speed;

    float pivot;

    private void Start()
    {
        pivot = firstTrans.parent.GetComponent<RectTransform>().rect.width;
        lastTrans.position = firstTrans.position + new Vector3(pivot, 0, 0);
    }

    private void Update()
    {
        if (firstTrans.localPosition.x > -pivot)
        {
            Debug.Log($"{firstTrans.localPosition.x} : {-pivot}");
            firstTrans.Translate(Vector2.left * Time.deltaTime * speed);
        }
        else
        {
            firstTrans.localPosition += new Vector3(pivot * 2, 0, 0);
        }

        if (lastTrans.localPosition.x > -pivot)
        {
            lastTrans.Translate(Vector2.left * Time.deltaTime * speed);
        }
        else
        {
            lastTrans.localPosition += new Vector3(pivot * 2, 0, 0);
        }
    }
}
