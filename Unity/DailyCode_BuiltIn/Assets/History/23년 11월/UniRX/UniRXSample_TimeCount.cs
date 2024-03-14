using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UniRXSample_TimeCount : MonoBehaviour
{
    public Text timerText;
    public int countdownDuration;   // 카운트 다운은 잘 생각해보면 10부터 시작해서 0일때 완료를 한다. 즉 총 11번의 방출이 필요하다.

    private void Start()
    {
        countdownDuration = 0;
        // 카운트 다운 옵저버
        // Take의 의미는 방출의 횟수 지정하는 것. 제로일때 1번
        IObservable<long> CDO = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Take(countdownDuration + 1);

        CDO.Subscribe(time => {
            Debug.Log("타임 " + time);
            int remainingTime = countdownDuration - (int)time;
            timerText.text = "Time : " + remainingTime;

            if (remainingTime == 0)
            {
                timerText.text = "완료";
            }
        }).AddTo(this);
        // 메모리 누수 방지용 AddTo(this)
    }
}
