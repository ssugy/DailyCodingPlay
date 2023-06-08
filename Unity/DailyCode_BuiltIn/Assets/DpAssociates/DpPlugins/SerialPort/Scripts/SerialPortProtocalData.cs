using System;

// 신호 프로토콜 각각의 데이터 객체
[Serializable]
public class SerialPortProtocalData
{
    public string Id;
    public string Protocal;
    public bool Resending;
    public int[] Delays;
    public bool IsSendingProtocal;      // 여기가 true이면, 비동기로 Delay만큼 시간뒤에, 신호를 보낸다.
    public string ReceivedData;

    // 
    public T GetEnumId<T>() where T : Enum
    {
        // T타입의 enum에서 Id이름을 가진 구성원을 반환한다. 
        return (T)Enum.Parse(typeof(T), Id);
    }
}

