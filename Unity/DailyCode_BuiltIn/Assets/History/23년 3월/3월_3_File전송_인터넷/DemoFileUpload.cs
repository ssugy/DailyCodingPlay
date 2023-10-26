using UnityEngine;

public class DemoFileUpload : MonoBehaviour
{
    public string m_Ip = "127.0.0.1";
    public int m_Port = 50001;
    private FileTransfer m_FileTransfer;
    public string uploadFileName;
    public string[] m_UploadFilePaths; // ���ε� �� ���ϵ�

    void Start()
    {
        m_FileTransfer = new FileTransfer(m_Port, FileTransfer.ETransfer.Upload, m_Ip);
        m_UploadFilePaths = new string[1];
        m_UploadFilePaths[0] = Application.dataPath + "/tmp/img_2.png";

        m_FileTransfer.EvntDownloadUpload += M_FileTransfer_EvntDownloadUpload;
    }

    private void M_FileTransfer_EvntDownloadUpload(object sender, FileTransfer.FileTransferEventArgs e)
    {
        Debug.Log("������Ʈ ���� : " +  sender.ToString());
        Debug.Log("������Ʈ e : " +  e.Message);
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