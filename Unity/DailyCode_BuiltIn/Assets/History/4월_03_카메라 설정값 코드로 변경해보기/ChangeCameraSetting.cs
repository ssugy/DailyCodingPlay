using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProLiveCamera;
using System.Configuration;

public class ChangeCameraSetting : MonoBehaviour
{
    public AVProLiveCamera webCam;
    private AVProLiveCameraDevice webCamDevice;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 1. 여기에서 디바이스를 가져오고
            webCamDevice = webCam.Device;

            // 2. 이런식으로 형변환해서 사용하면 된다. - Default 값도 설정 가능
            AVProLiveCameraSettingBase camBrightnessBase = webCamDevice.GetVideoSettingByType(AVProLiveCameraDevice.SettingsEnum.Brightness);
            AVProLiveCameraSettingFloat settingBlightnessFloat = (AVProLiveCameraSettingFloat)camBrightnessBase;    // 핵심
            Debug.Log("blightnes 수치 : " + settingBlightnessFloat.CurrentValue);
            settingBlightnessFloat.CurrentValue = 190;
            settingBlightnessFloat.Update();

            // 3. 인덱스 1은 AVProLiveCameraDevice.SettingsEnum의 두번째 값인 Contrast이다.
            AVProLiveCameraSettingBase camContrastBase = webCamDevice.GetVideoSettingByIndex(1);
            AVProLiveCameraSettingFloat settingBlightnessFloat2 = (AVProLiveCameraSettingFloat)camContrastBase;
            Debug.Log("어떤수치이지? 이름 : " + settingBlightnessFloat2.Name);  // Contrast라고 나온다.
            settingBlightnessFloat2.CurrentValue = 190;
            settingBlightnessFloat2.Update();
        }
    }
}
