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

    private void Init()
    {
        var vidAttr = new VideoTrackAttributes
        {
            bitRateMode = VideoBitrateMode.Medium,
            frameRate = new MediaRational(5),
            width = 1024,
            height = 1024,
            includeAlpha = false
        };

        var audAttr = new AudioTrackAttributes
        {
            sampleRate = new MediaRational(48000),
            channelCount = 2
        };

        var enc = new MediaEncoder("sample.mp4", vidAttr, audAttr);

        for (int i = 0 ; i < textures.Count; i++)
        {
            enc.AddFrame(textures[i]);
        }

        enc.Dispose();
    }

    public List<Texture2D> textures;

}
