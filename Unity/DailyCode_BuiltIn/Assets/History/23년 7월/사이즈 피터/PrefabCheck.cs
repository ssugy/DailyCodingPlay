using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCheck : MonoBehaviour
{
    public GameObject pref;
    public Transform parentTrans;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject go = Instantiate(pref, parentTrans);
            go.name = "test1";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 자식노드 모두 삭제하기
            foreach (Transform t in parentTrans)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
