using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;
using SimpleFileBrowser;
using DpTool;

public class FolderCheck : MonoBehaviour
{
    public Button fileOpenBtn;
    public MediaPlayer mediaPlayer;
    public RawImage preview;

    // Start is called before the first frame update
    void Start()
    {
        fileOpenBtn.onClick.AddListener(FileOpen);
    }

    private void FileOpen()
    {
        //string path = $"D:\\tmp\\test.txt"; // ���������� �ٷ� ����
        //Process proc = Process.Start(path);  // ������ �뵵

        FileBrowser.ShowLoadDialog(
            (success) =>
            {
                if (success.Length != 0)
                {
                    UnityEngine.Debug.Log(success[0]);  // ���� ��δ� ������ �� �ִ�.
                    mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, success[0], false);   // ���⼭ �߸��� ������ ��������,[AVProVideo] Error: Loading failed. ������ ���.
                    mediaPlayer.Play();
                }
            },
            () => { },
            FileBrowser.PickMode.Files);
    }

    // �ѹ��� ����Ǹ� �÷��� ȭ���� �״�� ��� ���´�.
    public void SetPreviewTexture()
    {
        Debug.Log("�̺�Ʈ ����");
        preview.texture = mediaPlayer.TextureProducer.GetTexture();
    }


}
