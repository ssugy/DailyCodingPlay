using UnityEngine;

public class DemoFileUpload : MonoBehaviour
{
    public string m_Ip = "127.0.0.1";
    public int m_Port = 50001;
    private FileTransfer m_FileTransfer;
    public string[] m_UploadFilePaths; // 업로드 할 파일들

    void Start()
    {
        m_FileTransfer = new FileTransfer(m_Port, FileTransfer.ETransfer.Upload, m_Ip);
        m_UploadFilePaths = new string[1];
        m_UploadFilePaths[0] = Application.dataPath + "/History/3월_3_File전송/UploadFileFolder/TEST.png";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            Upload();
    }

    void OnApplicationQuit()
    {
        if (m_FileTransfer != null)
        {
            m_FileTransfer.Close();
        }
    }

    void Upload()
    {
        m_FileTransfer.Upload(m_UploadFilePaths, '/');
    }
}