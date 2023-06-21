using System;
using System.Collections.Generic;
//using System.IO.Ports;
using System.Threading;
using UnityEngine;

namespace DpPlugin
{
    public interface SerialPortListener
    {
        void OnReceivedData(int id, byte[] bytes);
    }

    public class SerialPortWorker
    {
        //    private SerialPort serialPort;
        //    private SerialPortData serialPortData;
        //    private SerialPortListener portListener;
        //    public List<byte> BufferList { get; private set; }
        //    public bool IsOpen => serialPort != null ? serialPort.IsOpen : false;
        //    public int BufferCount => BufferList.Count;

        //    public SerialPortWorker(SerialPortListener listener)
        //    {
        //        portListener = listener;
        //        serialPort = new SerialPort();
        //        BufferList = new List<byte>();
        //    }

        //    public void Open(SerialPortData data)
        //    {
        //        try
        //        {
        //            serialPortData = data;
        //            serialPort.PortName = data.PortName;
        //            serialPort.BaudRate = data.BaudRate;
        //            serialPort.DataBits = data.DataBits;
        //            serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), data.Parity);
        //            serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), data.StopBits);
        //            serialPort.ReadTimeout = serialPortData.ReadTimeout;
        //            serialPort.WriteTimeout = serialPortData.WriteTimeout;
        //            serialPort.Open();

        //            Thread readThread = new Thread(read);
        //            readThread.Start();
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.Log($"Comport Open Error - message : {ex.Message}");
        //        }
        //    }

        //    private void read()
        //    {
        //        while (serialPort != null)
        //        {
        //            try
        //            {
        //                if (serialPort.IsOpen)
        //                {
        //                    int nbyte = serialPort.BytesToRead;

        //                    byte[] rbuff = new byte[nbyte];

        //                    if (nbyte > 0)
        //                    {
        //                        serialPort.Read(rbuff, 0, nbyte);
        //                    }

        //                    for (int i = 0; i < nbyte; i++)
        //                    {
        //                        BufferList.Add(rbuff[i]);
        //                    }

        //                    if (serialPortData.BufferMaxNum <= BufferList.Count)
        //                    {
        //                        byte[] buff = BufferList.GetRange(0, serialPortData.BufferMaxNum).ToArray();
        //                        portListener.OnReceivedData(serialPortData.Id, buff);

        //                        if (serialPortData.IsAutoFlushBuffer)
        //                        {
        //                            ClearBuffer();
        //                        }
        //                    }
        //                }
        //            }
        //            catch (TimeoutException ex)
        //            {
        //                Debug.Log($"{ex.Message}");
        //            }
        //        }
        //    }

        //    public void ClearBuffer()
        //    {
        //        BufferList.Clear();
        //    }

        //    public void Send(byte[] bytes)
        //    {
        //        try
        //        {
        //            if (serialPort.IsOpen)
        //                serialPort.Write(bytes, 0, bytes.Length);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.Log($"Send Exception - message : {ex.Message}");
        //            throw ex;
        //        }
        //    }

        //    public void Send(string sendingMessage)
        //    {
        //        try
        //        {
        //            if (serialPort.IsOpen)
        //                serialPort.Write(sendingMessage);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.Log($"Send Exception - message : {ex.Message}");
        //            throw ex;
        //        }
        //    }

        //    public void Close()
        //    {
        //        try
        //        {
        //            if (serialPort != null)
        //            {
        //                serialPort.Close();
        //                serialPort.Dispose();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.Log($"Close Error - message : {ex.Message}");
        //            throw ex;
        //        }
        //    }
    }
}
