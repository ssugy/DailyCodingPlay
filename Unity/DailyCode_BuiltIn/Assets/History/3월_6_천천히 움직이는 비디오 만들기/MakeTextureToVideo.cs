using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Media;
using System;

public class MakeTextureToVideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    /// <summary>
    /// 느리게하는 방법
    /// 1. 웹캠 + UI(스티커, 텍스트 등) 화면을
    /// 2. 새로운 카메라로 PNG시퀀스로 저장한다. (Avpro - 미디어 저장인가)
    /// 3. 아래에 있는 순서를 통해서, 영상으로 다시 재조립한다.
    /// </summary>
    private void Init()
    {
        // 1. 영상을 담는 틀을 제작 - 영상의 해상도
        var vidAttr = new VideoTrackAttributes
        {
            bitRateMode = VideoBitrateMode.Medium,
            frameRate = new MediaRational(5),   // 프레임
            width = 1024,
            height = 1024,
            includeAlpha = false
        };
        
        // 2. 오디오 틀
        var audAttr = new AudioTrackAttributes
        {
            sampleRate = new MediaRational(48000),
            channelCount = 2
        };

        // 3. 틀의 조합(비디오 + 오디오) => 현재까지는 내용물이 없음.
        var enc = new MediaEncoder("sample.mp4", vidAttr, audAttr);

        // 4. 틀(frame)에 이미지를 넣는다.
        for (int i = 0 ; i < textures.Count; i++)
        {
            enc.AddFrame(textures[i]);
        }

        // 5. 마무리(메모리 반환)
        enc.Dispose();
    }

    // 테스트용도로 이미지를 직접 넣기 위해서 생성한 변수(제대로 만들때에는 이미지를 폴더에서 가져와야한다.
    // 이미지를 4. 틀에 넣기 위해서는, 이미지 옵션을 변경할 수 있다. (Cursor와, Read/write옵션이 활성화 되어 있어야지 frame에 넣을 수 있다.)
    public List<Texture2D> textures;
}
