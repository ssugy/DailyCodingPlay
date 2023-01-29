using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImmuntableObj : MonoBehaviour
{
    class Sample
    {
        public int val;
        public int val2;

        public Sample(int a, int b)
        {
            this.val= a;
            this.val2= b;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Sample sam1 = new Sample(1, 2);
        Sample sam2 = new Sample(sam1.val +1, sam1.val2 + 2);   // 이렇게 사용하는거 문제없다.

        Debug.Log(sam2.val);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
