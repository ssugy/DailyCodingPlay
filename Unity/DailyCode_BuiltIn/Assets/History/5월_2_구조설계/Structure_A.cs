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

    [SerializeField] private Structure_B[] arrB;  // 인스펙터 창에서 지정
    [HideInInspector]public Structure_B cellingInstance;
    [HideInInspector]public Structure_B dispensorInstance;
    [HideInInspector]public Structure_B smartInstance;
    private void Start()
    {
        // 각각의 타입 지정
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
                    Debug.Log("타입지정을 못했습니다. : " + arrB[i].name);
                    break;
            }
        }
    }

    // 이렇게 하는것이 캐싱해서 빠르게 쓰기 위함
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
                Debug.Log("일치하는 타입이 없습니다.");
                break;
        }

        return null;
    }
}
