using DpPlugin;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class SerialPortManager : MonoBehaviour
{
    SerialPortProtocalProcessor<ButtonProtocalId> buttonProtocalProcessor;
    SerialPortProtocalProcessor<SimulatorProtocalId> simulatorProtocalProcessor;

    private void Start()
    {
        // Josn을 통해서 데이터 읽어들이기.
        string dataFolder = $"{Application.dataPath}/DpAssociates/DpPlugins/SerialPort/Demo/JsonDatas/";
        string serialPortDataContainerPath = $"{dataFolder}SerialPortDataContainer.json";
        SerialPortDataContainer containerData = getJsonData<SerialPortDataContainer>(serialPortDataContainerPath);
        string[] protocalDatas = { "ButtonProtocalDataContainer.json", "SimulatorProtocalDataContainer.json" };

        for (int i = 0; i < containerData.Datas.Length; i++)
        {
            // protocalDatas 2개의 갯수와, DataContainer Datas갯수가 같아야한다.
            string protocalContainerDataPath = $"{dataFolder}{protocalDatas[i]}";
            SerialPortProtocalDataContainer protocalDataContainer = getJsonData<SerialPortProtocalDataContainer>(protocalContainerDataPath);

            //아이디가 Button이면 ButtonProtocal로 제작
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

    // Json파일 가져오는 것.
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
}
