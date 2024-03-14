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
        // 옵저버블 - emits items or sends notifications to observers when tehre is a change or when new data is available.
        // 옵저버에게 데이터 전달함.
        // Unit  == void (의미가 비슷함)
        IObservable<Unit> clickStream = myButton.OnClickAsObservable(); // 여기서 OnClickAsObservable()이거는 UniRX에서 자체적으로 만들어 놓은 것.

        // subscribe를 하면 옵저버블이 지정한 이벤트마다 특정 행동을 할 수 있음.
        clickStream.Subscribe(x => { Debug.Log("버튼 클릭함." + x); });
    }
}
