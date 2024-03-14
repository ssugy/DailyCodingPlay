using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class UniRXSample : MonoBehaviour
{
    Subject<int> mSubject;
    public Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        // �������� - emits items or sends notifications to observers when tehre is a change or when new data is available.
        // ���������� ������ ������.
        // Unit  == void (�ǹ̰� �����)
        IObservable<Unit> clickStream = myButton.OnClickAsObservable(); // ���⼭ OnClickAsObservable()�̰Ŵ� UniRX���� ��ü������ ����� ���� ��.

        // subscribe�� �ϸ� ���������� ������ �̺�Ʈ���� Ư�� �ൿ�� �� �� ����.
        clickStream.Subscribe(x => { Debug.Log("��ư Ŭ����." + x); });
    }
}
