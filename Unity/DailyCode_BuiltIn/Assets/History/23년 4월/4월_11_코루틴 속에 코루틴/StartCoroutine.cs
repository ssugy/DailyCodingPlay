using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCoroutine : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Debug.Log(123);
        yield return new WaitForSeconds(1);
        Debug.Log(234);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
