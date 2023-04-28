using System;
using System.Collections;
using UnityEngine;

public class CoroutineInnerCoroutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TEST());
    }

    IEnumerator TEST()
    {
        Debug.Log(1);
        yield return new WaitForSeconds(0.5f);
        Debug.Log(2);
        TEST3();
    }

    IEnumerator TEST2()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(3);
    }

    private void TEST3()
    {
        StartCoroutine(TEST2());
    }
}
