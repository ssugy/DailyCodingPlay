using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaChecker : MonoBehaviour
{
    public MediaPlayer media;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log($"{media.Info.GetVideoWidth()} || {media.Info.GetVideoHeight()}"); 
        }
    }
}
