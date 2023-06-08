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
        // Josn�� ���ؼ� ������ �о���̱�.
        string dataFolder = $"{Application.dataPath}/DpAssociates/DpPlugins/SerialPort/Demo/JsonDatas/";
        string serialPortDataContainerPath = $"{dataFolder}SerialPortDataContainer.json";
        SerialPortDataContainer containerData = getJsonData<SerialPortDataContainer>(serialPortDataContainerPath);
        string[] protocalDatas = { "ButtonProtocalDataContainer.json", "SimulatorProtocalDataContainer.json" };

        for (int i = 0; i < containerData.Datas.Length; i++)
        {
            // protocalDatas 2���� ������, DataContainer Datas������ ���ƾ��Ѵ�.
            string protocalContainerDataPath = $"{dataFolder}{protocalDatas[i]}";
            SerialPortProtocalDataContainer protocalDataContainer = getJsonData<SerialPortProtocalDataContainer>(protocalContainerDataPath);

            //���̵� Button�̸� ButtonProtocal�� ����
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

    // Json���� �������� ��.
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
