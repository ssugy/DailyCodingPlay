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
        Debug.Log(sample.text.Replace('\n',' '));   // 이렇게 하면됨. 굳이 어렵게 안해도됨. 첨부터 이거 따로 저장안해서 그런거임
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
