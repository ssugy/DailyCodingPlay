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
            Debug.Log("��Ʈ�� : " + myBType.CheckInt); // �̷������� ����� �����ϴ�.
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("����");
            myBType = Structure_A.instance.GETBType(CType); // �������� ��
        }
    }
}
