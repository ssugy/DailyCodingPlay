using DpPlugin;
using System.IO;
using UnityEngine;

public enum ButtonProtocalId
{
    None,
    Start,
    Stop,
    Finished
}

public enum SimulatorProtocalId
{
    None,
    OccuredStrangeSignal,
    LaySeat,
    StandUpSeat,
    StopSeat,
    StartVibration,
    StopVibration,
    TurnOffLight,
    TurnOnLight
}




public enum SerialPortId
{
    Button,
    SerialPort
}

public class SerialPortDemo : MonoBehaviour
{
    SerialPortProtocalProcessor<ButtonProtocalId> buttonProtocalProcessor;
    SerialPortProtocalProcessor<SimulatorProtocalId> simulatorProtocalProcessor;

    public void Awake()
    {
        string dataFolder = $"{Application.dataPath}/DpAssociates/DpPlugins/SerialPort/Demo/JsonDatas/";
        string serialPortDataContainerPath = $"{dataFolder}SerialPortDataContainer.json";

        SerialPortDataContainer containerData = getJsonData<SerialPortDataContainer>(serialPortDataContainerPath);

        string[] protocalDatas = { "ButtonProtocalDataContainer.json", "SimulatorProtocalDataContainer.json" };

        for (int i = 0; i < containerData.Datas.Length; i++)
        {
            string protocalContainerDataPath = $"{dataFolder}{protocalDatas[i]}";
            SerialPortProtocalDataContainer protocalDataContainer = getJsonData<SerialPortProtocalDataContainer>(protocalContainerDataPath);
            if ((int)SerialPortId.Button == i)
            {
                buttonProtocalProcessor = new SerialPortProtocalProcessor<ButtonProtocalId>(containerData.Datas[i], protocalDataContainer);
            }
            else
            {
                simulatorProtocalProcessor = new SerialPortProtocalProcessor<SimulatorProtocalId>(containerData.Datas[i], protocalDataContainer);
            }
        }
    }

    void Update()
    {
        actionByButtonProtocal();
        actionBySimulatorProtocal();
    }

    private void actionByButtonProtocal()
    {
        if (buttonProtocalProcessor.IsChangedId)
        {
            Debug.Log("actionByButtonProtocal 실행");
            buttonProtocalProcessor.ApplyId();

            switch (buttonProtocalProcessor.CurrentId)
            {
                case ButtonProtocalId.Start:
                    simulatorProtocalProcessor.Start();
                    simulatorProtocalProcessor.Send(SimulatorProtocalId.TurnOffLight);
                    break;

                case ButtonProtocalId.Stop:
                    simulatorProtocalProcessor.Stop();
                    simulatorProtocalProcessor.Send(SimulatorProtocalId.OccuredStrangeSignal);
                    simulatorProtocalProcessor.Send(SimulatorProtocalId.TurnOnLight);
                    break;
            }
        }
    }

    private void actionBySimulatorProtocal()
    {
        if (simulatorProtocalProcessor.IsChangedId)
        {
            Debug.Log("actionBySimulatorProtocal 실행");
            simulatorProtocalProcessor.ApplyId();

            switch (simulatorProtocalProcessor.CurrentId)
            {
                case SimulatorProtocalId.OccuredStrangeSignal:
                    Debug.Log($"{SimulatorProtocalId.OccuredStrangeSignal}");
                    break;
            }
        }
    }

    private T getJsonData<T>(string path)
    {
        T data = default(T);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = (T)JsonUtility.FromJson(json, typeof(T));
        }
        else
        {
            throw new FileNotFoundException($"Path : {path}");
        }

        return data;
    }

    private void OnApplicationQuit()
    {
        simulatorProtocalProcessor.OnApplicationQuit();
        buttonProtocalProcessor.OnApplicationQuit();
    }
}
