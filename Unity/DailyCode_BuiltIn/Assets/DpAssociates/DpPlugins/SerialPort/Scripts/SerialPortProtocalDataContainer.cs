using System;
using System.Linq;

// 신호보내는 프로토콜 용도 컨테이너
[Serializable]
public class SerialPortProtocalDataContainer
{
    public SerialPortProtocalData[] Datas;

    public SerialPortProtocalData GetProtocalDataByProtocal(string protocal)
    {
        return Datas.Where(x => x.Protocal == protocal).FirstOrDefault();
    }

    // Datas내부에서 id와 ID가 같은 Data를 반환한다.
    public SerialPortProtocalData GetProtocalDataById(string id)
    {
        return Datas.Where(x => x.Id == id).FirstOrDefault();
    }
}

