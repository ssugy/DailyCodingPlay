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
    /// üũ�غ��ϱ�, ref�� �޴���, �ȴ޴��� �����ϰ� ������ �Ѵ�.
    /// out�� ���, RefOutClient���� �ʱ�ȭ�� ����ߵȴ�. (�ʱ�ȭ�� null���ϸ� �ȵȴ�.)
    /// </summary>
    private void CheckRef()
    {
        client.CheckRef(rawImage);
    }
}
