using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure_C : MonoBehaviour
{
    public ContentType CType;
    private Structure_B myBType;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("인트값 : " + myBType.CheckInt); // 이런식으로 사용이 가능하다.
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("지정");
            myBType = Structure_A.instance.GETBType(CType); // 가져오는 것
        }
    }
}
