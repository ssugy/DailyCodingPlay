using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure_A : MonoBehaviour
{
    private static Structure_A _instance;
    public static Structure_A instance {  get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private Structure_B[] arrB;  // �ν����� â���� ����
    [HideInInspector]public Structure_B cellingInstance;
    [HideInInspector]public Structure_B dispensorInstance;
    [HideInInspector]public Structure_B smartInstance;
    private void Start()
    {
        // ������ Ÿ�� ����
        for (int i = 0; i < arrB.Length; i++)
        {
            switch (arrB[i].myType)
            {
                case ContentType.None:
                    break;
                case ContentType.CellingLED:
                    cellingInstance = arrB[i];
                    break;
                case ContentType.Dispensor:
                    dispensorInstance = arrB[i];
                    break;
                case ContentType.SmartMirror:
                    smartInstance = arrB[i];
                    break;
                default:
                    Debug.Log("Ÿ�������� ���߽��ϴ�. : " + arrB[i].name);
                    break;
            }
        }
    }

    // �̷��� �ϴ°��� ĳ���ؼ� ������ ���� ����
    public Structure_B GETBType(ContentType ct)
    {
        switch (ct)
        {
            case ContentType.None:
                break;
            case ContentType.CellingLED:
                return cellingInstance;
            case ContentType.Dispensor:
                return dispensorInstance;
            case ContentType.SmartMirror:
                return smartInstance;
            default:
                Debug.Log("��ġ�ϴ� Ÿ���� �����ϴ�.");
                break;
        }

        return null;
    }
}
