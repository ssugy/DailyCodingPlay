using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefOutChecker : MonoBehaviour
{
    public RawImage rawImage;
    public Button btn;
    public RefOutClient client;

    private void Start()
    {
        btn.onClick.AddListener(CheckRef);
    }

    /// <summary>
    /// 체크해보니까, ref를 달던지, 안달던지 동일하게 동작을 한다.
    /// out의 경우, RefOutClient에서 초기화를 해줘야된다. (초기화를 null로하면 안된다.)
    /// </summary>
    private void CheckRef()
    {
        client.CheckRef(rawImage);
    }
}
