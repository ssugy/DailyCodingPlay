using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryCheck : MonoBehaviour
{
    public Dictionary<string, int> mydict= new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        mydict.Add("zero", 0);
        mydict.Add("one", 1);
        mydict.Add("two", 2);

        // foreach���� dictionary�� Ȯ�θ� ��������, ���� �����ϸ� ������ ����.(�׷��� ���������.)
        foreach (KeyValuePair<string, int> item in mydict)
        {
            if (item.Value != 1)
                Debug.Log("->" + item.Key + ": " + item.Value);
            else
                mydict.Remove(item.Key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
