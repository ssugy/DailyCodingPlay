using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefOutClient : MonoBehaviour
{
    public RawImage rawImage;

    // ������ ȣ��
    public void CheckRef(RawImage raw)
    {
        raw.texture = rawImage.texture;
    }
}
