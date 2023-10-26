using UnityEngine;

public class DemoFileUpload : MonoBehaviour
{
    public string m_Ip = "127.0.0.1";
    public int m_Port = 50001;
    private FileTransfer m_FileTransfer;
    public string uploadFileName;
    public string[] m_UploadFilePaths; // 업로드 할 파일들

    void Start()
    {
        m_FileTransfer = new FileTransfer(m_Port, FileTransfer.ETransfer.Upload, m_Ip);
        m_UploadFilePaths = new string[1];
        m_UploadFilePaths[0] = Application.dataPath + "/tmp/img_2.png";

        m_FileTransfer.EvntDownloadUpload += M_FileTransfer_EvntDownloadUpload;
    }

    private void M_FileTransfer_EvntDownloadUpload(object sender, FileTransfer.FileTransferEventArgs e)
    {
        Debug.Log("오브젝트 센더 : " +  sender.ToString());
        Debug.Log("오브젝트 e : " +  e.Message);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Upload();
            //m_FileTransfer.
        }
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