using System.IO;
using UnityEngine;

public class DemoFileDownload : MonoBehaviour
{
    //public string m_Ip = "127.0.0.1";
    public int m_Port = 50001;
    private DirectoryInfo m_DownloadDirectory;
    //public string m_DownloadFilePath;
    private FileTransfer m_FileTransfer; // 파일 다운 받을 디렉토리

    // 다운로드는 아이피가 필요없다. 그냥 자기한테 온 것 받는 개념이다.
    // 한번만 이렇게 다운로드 지정해두면 보내는대로 알아서 받는다.
    void Start()
    {
        //m_DownloadDirectory = new DirectoryInfo(m_DownloadFilePath);
        //m_DownloadDirectory = new DirectoryInfo(Application.dataPath +"/History/3월_3_File전송/DownloadFolder/");
        m_DownloadDirectory = new DirectoryInfo(Application.dataPath +"/History/3월_3_File전송/DownloadFolder/");
        m_FileTransfer = new FileTransfer(m_Port, FileTransfer.ETransfer.Download);
        m_FileTransfer.Download(m_DownloadDirectory.FullName);
    }

    void OnApplicationQuit()
    {
        if (m_FileTransfer != null)
        {
            Debug.Log("파일전송 멈춤");
            m_FileTransfer.Close();
        }
    }
}
