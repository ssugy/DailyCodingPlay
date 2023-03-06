using System.IO;
using UnityEngine;

public class DemoFileDownload : MonoBehaviour
{
    public string m_Ip = "192.168.0.26";
    public int m_Port = 50001;
    private DirectoryInfo m_DownloadDirectory;
    //public string m_DownloadFilePath;
    private FileTransfer m_FileTransfer; // ���� �ٿ� ���� ���丮

    void Start()
    {
        //m_DownloadDirectory = new DirectoryInfo(m_DownloadFilePath);
        m_DownloadDirectory = new DirectoryInfo(Application.dataPath + "/2303_FileDownloader/download/");
        m_FileTransfer = new FileTransfer(m_Port, FileTransfer.ETransfer.Download);
        m_FileTransfer.Download(m_DownloadDirectory.FullName);
    }

    void OnApplicationQuit()
    {
        if (m_FileTransfer != null)
        {
            m_FileTransfer.Close();
        }
    }
}
