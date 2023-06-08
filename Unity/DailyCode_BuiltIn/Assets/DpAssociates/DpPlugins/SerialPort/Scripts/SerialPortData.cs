using DpPlugin;
using System;

// Json 세부 구성요소 객체
[Serializable]
public class SerialPortData
{
    public int Id;
    public string PortName;
    public int BaudRate;
    public string Parity;
    public int DataBits;
    public string StopBits;
    public int BufferMaxNum;
    public string ByteConvertingType;
    public int ReadTimeout;
    public int WriteTimeout;
    public bool IsResending;
    public bool IsAutoFlushBuffer;
    public bool IsOriginalResending;

    // Json에서 어떤 타입으로 들어올지 확인하기.
    public ByteConvertingType ByteConvertingTypeEnum => (ByteConvertingType)Enum.Parse(typeof(ByteConvertingType), ByteConvertingType);
}