using Cysharp.Threading.Tasks;
using System;
using System.Text;
using System.Threading;
using UnityEngine;

namespace DpPlugin
{
    // 실제 최초 실행단에서 해당 객체를 만들어서 사용한다.
    // 이것은 시리얼포트 프토로콜용 프로세서이다.
    public class SerialPortProtocalProcessor<T> : SerialPortListener where T : Enum
    {
        private SerialPortWorker serialPortWorker;  // 시리얼포트 직접 동작을 하는 클래스
        private SerialPortData serialPortData;      // 시리얼포트 데이터 - 시리얼 포트 연결하기위한 Json파일
        private SerialPortProtocalDataContainer protocalDataContainer;  // 프로토콜 데이터를 가지고있는 컨테이너
        private CancellationTokenSource cts;        // UniRX 중간 멈추는 용도 토큰

        public T OldId { get; private set; }
        public T CurrentId { get; private set; }

        public bool IsChangedId => OldId.ToString() != CurrentId.ToString();

        // 생성자. 해당객체를 생성할 때에는 시리얼포트 데이터, 컨테이너 2개가 필요하다.
        public SerialPortProtocalProcessor(SerialPortData serialPortData, SerialPortProtocalDataContainer protocalDataContainer)
        {
            this.protocalDataContainer = protocalDataContainer;
            this.serialPortData = serialPortData;

            serialPortWorker = new SerialPortWorker(this);
            serialPortWorker.Open(serialPortData);          // 여기서 실제로 시리얼포트 연결을 시도한다.
            OldId = CurrentId = (T)Enum.Parse(typeof(T), "None");   // None 상태로 초기화
        }

        public void Start()
        {
            cts = new CancellationTokenSource();

            for (int i = 0; i < protocalDataContainer.Datas.Length; i++)
            {
                sendAfterDelays(protocalDataContainer.Datas[i]);
            }
        }

        public void Stop()
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
        }

        private void sendAfterDelays(SerialPortProtocalData protocalData)
        {
            if (protocalData.IsSendingProtocal)
            {
                for (int i = 0; i < protocalData.Delays.Length; i++)
                {
                    // Enum 명령어에 따라서 send보냄
                    sendAfterDelayAsync(protocalData.GetEnumId<T>(), protocalData.Delays[i]).Forget();
                }
            }
        }

        private async UniTaskVoid sendAfterDelayAsync(T id, int delay)
        {
            try
            {
                // Delay만큼의 시간이 지난 뒤에
                await UniTask.Delay(delay, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                await UniTask.CompletedTask;
                return;
            }

            Send(id);   // T는 사용자 지정한 Enum이다.
        }

        /// <summary>
        /// ID는 Enum으로 지정한 명령. Protocal은 해당 명령을 수행하기 위해 보내는 신호값.
        /// </summary>
        /// <param name="id">Enum의 이름을 가진 명령</param>
        public void Send(T id)
        {
            // id의 이름을 가진 명령어를 가져온다.
            SerialPortProtocalData protocalData = protocalDataContainer.GetProtocalDataById(id.ToString());

            if (serialPortData.IsOriginalResending)
            {
                switch (serialPortData.ByteConvertingTypeEnum)
                {

                    case ByteConvertingType.Hex:
                        {
                            byte[] bytes = convertStringToHex(protocalData.Protocal);
                            sendBytes(bytes);
                            break;
                        }
                    case ByteConvertingType.String:
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(protocalData.Protocal);
                            sendBytes(bytes);
                            break;
                        }
                }
            }
            else
            {
                serialPortWorker.Send(protocalData.Protocal);
            }
        }

        private void sendBytes(byte[] bytes)
        {
            serialPortWorker.Send(bytes);
            //logBytes(100, bytes);
        }

        private static byte[] convertStringToHex(string convertString)
        {
            byte[] convertArr = new byte[convertString.Length / 2];

            for (int i = 0; i < convertArr.Length; i++)
            {
                convertArr[i] = Convert.ToByte(convertString.Substring(i * 2, 2), 16);
            }
            return convertArr;
        }


        public void OnReceivedData(int id, byte[] bytes)
        {
            SerialPortProtocalData protocalData = getProtocalData(bytes);

            if (protocalData == null)
            {
                return;
            }

            //logBytes(id, bytes);

            if (protocalData.Resending)
            {
                if (serialPortData.IsOriginalResending)
                {
                    serialPortWorker.Send(bytes);
                }
                else
                {
                    serialPortWorker.Send(protocalData.ReceivedData);
                }
            }

            CurrentId = protocalData.GetEnumId<T>();
            Debug.Log($"CurrentId : {CurrentId}");
        }

        public void ApplyId()
        {
            OldId = CurrentId;
        }

        public void SetId(T id)
        {
            CurrentId = id;
        }

        private SerialPortProtocalData getProtocalData(byte[] bytes)
        {
            ByteConvertingType byteConvertingType = serialPortData.ByteConvertingTypeEnum;
            string receivedData = ByteConvertor.GetConvertedData(bytes, byteConvertingType);
            SerialPortProtocalData protocalData = protocalDataContainer.GetProtocalDataByProtocal(receivedData);

            if (protocalData == null)
            {
                Debug.Log($"Received protocal \"{receivedData}\" is wrong!");
            }
            else
            {
                protocalData.ReceivedData = receivedData;
            }

            return protocalData;
        }

        public void OnApplicationQuit()
        {
            serialPortWorker?.Close();
        }

        private void logBytes(int id, byte[] bytes)
        {
            string receivedData = string.Empty;

            for (int i = 0; i < bytes.Length; i++)
            {
                receivedData = $"{receivedData}{bytes[i]}";
            }

            Debug.Log($"id : {id}, receivedData : {receivedData}");
        }

    }
}