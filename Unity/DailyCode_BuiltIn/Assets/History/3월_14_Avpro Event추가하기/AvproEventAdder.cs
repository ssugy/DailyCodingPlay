using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using System;

public class AvproEventAdder : MonoBehaviour
{
    public MediaPlayer media;

    private void Start()
    {
        media.Events.AddListener(EventChecker);
    }

    /// <summary>
    /// �̺�Ʈ�� �޸�, �ش� ������ �̺�Ʈ�� ȣ���� �Ǹ鼭, ȣ��Ǵ� �̵���÷��̾ ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="arg0">ȣ��Ǵ� �̵���÷��̾� ��ü</param>
    /// <param name="arg1">�̺�Ʈ Ÿ��</param>
    /// <param name="arg2"></param>
    private void EventChecker(MediaPlayer arg0, MediaPlayerEvent.EventType arg1, ErrorCode arg2)
    {
        switch (arg1)
        {
            case MediaPlayerEvent.EventType.MetaDataReady:
                Debug.Log($"{name} MetaDataReady");
                break;
            case MediaPlayerEvent.EventType.ReadyToPlay:
                Debug.Log($"{name} ReadyToPlay");
                break;
            case MediaPlayerEvent.EventType.Started:
                Debug.Log($"{name} Started");
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                Debug.Log($"{name} {arg0.name} FirstFrameReady");
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log($"{name} FinishedPlaying");
                break;
            case MediaPlayerEvent.EventType.Closing:
                Debug.Log($"{name} Closing");
                break;
            case MediaPlayerEvent.EventType.Error:
                Debug.Log($"{name} Error");
                break;
            case MediaPlayerEvent.EventType.SubtitleChange:
                Debug.Log($"{name} SubtitleChange");
                break;
            case MediaPlayerEvent.EventType.Stalled:
                Debug.Log($"{name} Stalled");
                break;
            case MediaPlayerEvent.EventType.Unstalled:
                Debug.Log($"{name} Unstalled");
                break;
            case MediaPlayerEvent.EventType.ResolutionChanged:
                Debug.Log($"{name} ResolutionChanged");
                break;
            case MediaPlayerEvent.EventType.StartedSeeking:
                Debug.Log($"{name} StartedSeeking");
                break;
            case MediaPlayerEvent.EventType.FinishedSeeking:
                Debug.Log($"{name} FinishedSeeking");
                break;
            case MediaPlayerEvent.EventType.StartedBuffering:
                Debug.Log($"{name} StartedBuffering");
                break;
            case MediaPlayerEvent.EventType.FinishedBuffering:
                Debug.Log($"{name} FinishedBuffering");
                break;
            case MediaPlayerEvent.EventType.PropertiesChanged:
                Debug.Log($"{name} PropertiesChanged");
                break;
            case MediaPlayerEvent.EventType.PlaylistItemChanged:
                Debug.Log($"{name} PlaylistItemChanged");
                break;
            case MediaPlayerEvent.EventType.PlaylistFinished:
                Debug.Log($"{name} PlaylistFinished");
                break;
        }
    }
}
