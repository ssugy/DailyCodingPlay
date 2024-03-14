using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UniRXSample_TimeCount : MonoBehaviour
{
    public Text timerText;
    public int countdownDuration;   // ī��Ʈ �ٿ��� �� �����غ��� 10���� �����ؼ� 0�϶� �ϷḦ �Ѵ�. �� �� 11���� ������ �ʿ��ϴ�.

    private void Start()
    {
        countdownDuration = 0;
        // ī��Ʈ �ٿ� ������
        // Take�� �ǹ̴� ������ Ƚ�� �����ϴ� ��. �����϶� 1��
        IObservable<long> CDO = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Take(countdownDuration + 1);

        CDO.Subscribe(time => {
            Debug.Log("Ÿ�� " + time);
            int remainingTime = countdownDuration - (int)time;
            timerText.text = "Time : " + remainingTime;

            if (remainingTime == 0)
            {
                timerText.text = "�Ϸ�";
            }
        }).AddTo(this);
        // �޸� ���� ������ AddTo(this)
    }
}
