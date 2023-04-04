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
    /// �������ϴ� ���
    /// 1. ��ķ + UI(��ƼĿ, �ؽ�Ʈ ��) ȭ����
    /// 2. ���ο� ī�޶�� PNG�������� �����Ѵ�. (Avpro - �̵�� �����ΰ�)
    /// 3. �Ʒ��� �ִ� ������ ���ؼ�, �������� �ٽ� �������Ѵ�.
    /// </summary>
    private void Init()
    {
        // 1. ������ ��� Ʋ�� ���� - ������ �ػ�
        var vidAttr = new VideoTrackAttributes
        {
            bitRateMode = VideoBitrateMode.Medium,
            frameRate = new MediaRational(5),   // ������
            width = 1024,
            height = 1024,
            includeAlpha = false
        };
        
        // 2. ����� Ʋ
        var audAttr = new AudioTrackAttributes
        {
            sampleRate = new MediaRational(48000),
            channelCount = 2
        };

        // 3. Ʋ�� ����(���� + �����) => ��������� ���빰�� ����.
        var enc = new MediaEncoder("sample.mp4", vidAttr, audAttr);

        // 4. Ʋ(frame)�� �̹����� �ִ´�.
        for (int i = 0 ; i < textures.Count; i++)
        {
            enc.AddFrame(textures[i]);
        }

        // 5. ������(�޸� ��ȯ)
        enc.Dispose();
    }

    // �׽�Ʈ�뵵�� �̹����� ���� �ֱ� ���ؼ� ������ ����(����� ���鶧���� �̹����� �������� �����;��Ѵ�.
    // �̹����� 4. Ʋ�� �ֱ� ���ؼ���, �̹��� �ɼ��� ������ �� �ִ�. (Cursor��, Read/write�ɼ��� Ȱ��ȭ �Ǿ� �־���� frame�� ���� �� �ִ�.)
    public List<Texture2D> textures;
}
