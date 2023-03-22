using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringProcessing : MonoBehaviour
{
    public Text sample;
    public Image bgImg;
    public float speed =1;
    private void Start()
    {
        Debug.Log(sample.text.Replace('\n',' '));   // �̷��� �ϸ��. ���� ��ư� ���ص���. ÷���� �̰� ���� ������ؼ� �׷�����
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            sample.transform.Translate(Vector2.left * Time.deltaTime * speed);
            Debug.Log(bgImg.rectTransform.rect.Contains((Vector2)sample.rectTransform.localPosition + sample.rectTransform.rect.max));
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            sample.transform.Translate(Vector2.right * Time.deltaTime * speed);
            Debug.Log(bgImg.rectTransform.rect.Contains((Vector2)sample.rectTransform.localPosition + sample.rectTransform.rect.max));
        }
    }
}
