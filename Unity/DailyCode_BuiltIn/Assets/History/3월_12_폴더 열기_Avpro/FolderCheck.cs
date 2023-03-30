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
    public RawImage preview2;

    private Texture2D previewTexture;
    private Texture2D previewTexture2;

    // Start is called before the first frame update
    void Start()
    {
        fileOpenBtn.onClick.AddListener(FileOpen);
    }

    private void FileOpen()
    {
        //string path = $"D:\\tmp\\test.txt"; // 파일찍으면 바로 실행
        //Process proc = Process.Start(path);  // 실행의 용도

        FileBrowser.ShowLoadDialog(
            (success) =>
            {
                if (success.Length != 0)
                {
                    UnityEngine.Debug.Log(success[0]);  // 절대 경로는 가져올 수 있다.
                    mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, success[0], false);   // 여기서 잘못된 파일을 가져오면,[AVProVideo] Error: Loading failed. 에러가 뜬다.
                    mediaPlayer.Play();
                   
                }
            },
            () => { },
            FileBrowser.PickMode.Files);
    }

    // 한번만 적용되면 플레이 화면이 그대로 계속 나온다.
    public void SetPreviewTexture()
    {
        Debug.Log("이벤트 시작");
        //preview.texture = mediaPlayer.TextureProducer.GetTexture(); - 이거는 실시간으로 변경된다.
        // Target Texture는 null일 때 나타내는 용도의 텍스쳐이다. - 그대로 간다.
        previewTexture2 = mediaPlayer.ExtractFrame(previewTexture, 1);

        preview.texture = previewTexture;
        preview2.texture = previewTexture2;
    }

    // 영상 들어가있는지 확인하는 용도.
    public void CanPlayCheck()
    {
        Debug.Log(mediaPlayer.Control.CanPlay());   // 이걸로 체크 가능하다.
    }
}
