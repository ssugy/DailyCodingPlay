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
            // 1. ���⿡�� ����̽��� ��������
            webCamDevice = webCam.Device;

            // 2. �̷������� ����ȯ�ؼ� ����ϸ� �ȴ�. - Default ���� ���� ����
            AVProLiveCameraSettingBase camBrightnessBase = webCamDevice.GetVideoSettingByType(AVProLiveCameraDevice.SettingsEnum.Brightness);
            AVProLiveCameraSettingFloat settingBlightnessFloat = (AVProLiveCameraSettingFloat)camBrightnessBase;    // �ٽ�
            Debug.Log("blightnes ��ġ : " + settingBlightnessFloat.CurrentValue);
            settingBlightnessFloat.CurrentValue = 190;
            settingBlightnessFloat.Update();

            // 3. �ε��� 1�� AVProLiveCameraDevice.SettingsEnum�� �ι�° ���� Contrast�̴�.
            AVProLiveCameraSettingBase camContrastBase = webCamDevice.GetVideoSettingByIndex(1);
            AVProLiveCameraSettingFloat settingBlightnessFloat2 = (AVProLiveCameraSettingFloat)camContrastBase;
            Debug.Log("���ġ����? �̸� : " + settingBlightnessFloat2.Name);  // Contrast��� ���´�.
            settingBlightnessFloat2.CurrentValue = 190;
            settingBlightnessFloat2.Update();
        }
    }
}
