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
    float m_stopRecordingTimer = float.MaxValue;    // 맥스로 지정해서 준비가 안되었을 때 실행되지 않도록 만듬. 실제 준비가 되면 콜백함수로 값이 바뀜

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
            // 여기 이런방식은 결국 disposed해도 업데이트라서 한프레임 더 실행된다.
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        }
    }

    private void StartVideoCaptureTest()
    {
        // Resolution은 유니티에서 제공하는 해상도 관련 구조체이다.
        // 비디오가 제공하는 해상도가 높은것에서 낮은순으로 정렬해서 맨 첫번째 것을 반환해서 레졸루션으로 만들겠다는 의미이다.
        Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending(res => res.width * res.height).FirstOrDefault();
        Debug.Log("카메라 레졸루션 : " + cameraResolution);

        // 카메라의 프레임레이트도 높은순서에서 낮은 순서로 정렬해서 첫번째 것을 가져온다.
        float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending(x => x).FirstOrDefault();
        Debug.Log("카메라 프레임레이트 : " + cameraFramerate);

        VideoCapture.CreateAsync(false, (captureObj) =>
        {
            if (captureObj != null)
            {
                m_VideoCapture = captureObj;
                Debug.Log("비디오 캡쳐 인스턴스 생성.");

                // 유니티에서 제공하는 카메라용 파라미터 모음 구조체
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
                Debug.LogError("카메라 인스턴스 생성 실패");
            }
        });
    }

    /// <summary>
    /// 캡쳐시작
    /// </summary>
    /// <param name="result"></param>
    private void OnStartVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("비디오 캡쳐모드 실행");
        string timeStamp = Time.time.ToString().Replace(".", "").Replace(":","");   // 실행시간을 숫자로 표기하기
        string fileName = string.Format($"TestVideo_{timeStamp}.mp4");
        string filePath = System.IO.Path.Combine(Application.dataPath, fileName);
        filePath = filePath.Replace("/", @"\");                                     // 파일저장위치
        m_VideoCapture.StartRecordingAsync(filePath, onStartedRecordingVideoCallback);
    }

    private void onStartedRecordingVideoCallback(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("비디오 레코딩 시작");
        m_stopRecordingTimer = Time.time + MaxRecordingTime;    // 현재 시간 기준 5초 이후가 멈추는 타임.
    }

    // 정리관련 코드
    private void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("비디오 레코팅 멈춤");
        // 여기서 추가작업 안하면 에러발생한다. disposed
        
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

    private void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("비디오 캡쳐 멈춤");
        m_VideoCapture.Dispose();
        m_VideoCapture = null;
    }
}
