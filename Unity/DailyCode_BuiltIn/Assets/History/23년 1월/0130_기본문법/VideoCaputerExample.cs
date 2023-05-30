using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class VideoCaputerExample : MonoBehaviour
{
    static readonly float MaxRecordingTime = 5.0f;

    VideoCapture m_VideoCapture = null;
    float m_stopRecordingTimer = float.MaxValue;    // �ƽ��� �����ؼ� �غ� �ȵǾ��� �� ������� �ʵ��� ����. ���� �غ� �Ǹ� �ݹ��Լ��� ���� �ٲ�

    private void Start()
    {
        StartVideoCaptureTest();
    }

    private void Update()
    {
        if (m_VideoCapture == null || !m_VideoCapture.IsRecording)
        {
            return;
        }

        if(Time.time > m_stopRecordingTimer)
        {
            // ���� �̷������ �ᱹ disposed�ص� ������Ʈ�� �������� �� ����ȴ�.
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        }
    }

    private void StartVideoCaptureTest()
    {
        // Resolution�� ����Ƽ���� �����ϴ� �ػ� ���� ����ü�̴�.
        // ������ �����ϴ� �ػ󵵰� �����Ϳ��� ���������� �����ؼ� �� ù��° ���� ��ȯ�ؼ� ����������� ����ڴٴ� �ǹ��̴�.
        Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending(res => res.width * res.height).FirstOrDefault();
        Debug.Log("ī�޶� ������� : " + cameraResolution);

        // ī�޶��� �����ӷ���Ʈ�� ������������ ���� ������ �����ؼ� ù��° ���� �����´�.
        float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending(x => x).FirstOrDefault();
        Debug.Log("ī�޶� �����ӷ���Ʈ : " + cameraFramerate);

        VideoCapture.CreateAsync(false, (captureObj) =>
        {
            if (captureObj != null)
            {
                m_VideoCapture = captureObj;
                Debug.Log("���� ĸ�� �ν��Ͻ� ����.");

                // ����Ƽ���� �����ϴ� ī�޶�� �Ķ���� ���� ����ü
                CameraParameters cameraParameters= new CameraParameters();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate= cameraFramerate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

                m_VideoCapture.StartVideoModeAsync(cameraParameters
                    , VideoCapture.AudioState.ApplicationAndMicAudio
                    , OnStartVideoCaptureMode);
            }
            else
            {
                Debug.LogError("ī�޶� �ν��Ͻ� ���� ����");
            }
        });
    }

    /// <summary>
    /// ĸ�Ľ���
    /// </summary>
    /// <param name="result"></param>
    private void OnStartVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("���� ĸ�ĸ�� ����");
        string timeStamp = Time.time.ToString().Replace(".", "").Replace(":","");   // ����ð��� ���ڷ� ǥ���ϱ�
        string fileName = string.Format($"TestVideo_{timeStamp}.mp4");
        string filePath = System.IO.Path.Combine(Application.dataPath, fileName);
        filePath = filePath.Replace("/", @"\");                                     // ����������ġ
        m_VideoCapture.StartRecordingAsync(filePath, onStartedRecordingVideoCallback);
    }

    private void onStartedRecordingVideoCallback(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("���� ���ڵ� ����");
        m_stopRecordingTimer = Time.time + MaxRecordingTime;    // ���� �ð� ���� 5�� ���İ� ���ߴ� Ÿ��.
    }

    // �������� �ڵ�
    private void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("���� ������ ����");
        // ���⼭ �߰��۾� ���ϸ� �����߻��Ѵ�. disposed
        
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

    private void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("���� ĸ�� ����");
        m_VideoCapture.Dispose();
        m_VideoCapture = null;
    }
}
