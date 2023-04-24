using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameworkConstants : MonoBehaviour
{
    public static string AppSourceFileRoot => $"{Application.dataPath}/../AppSourceFiles/";
    public static string JsonFileRoot => $"{AppSourceFileRoot}JsonDatas/";
    public static string SoundFileRoot => $"{AppSourceFileRoot}Sounds/";
    public static string TextureFileRoot => $"{AppSourceFileRoot}Textures/";
    public static string ErrorLogFileRoot => $"{Application.dataPath}/../ErrorLogs/";
    public static string VideoFileRoot => $"{AppSourceFileRoot}Videos/";

    // 스마트미러 전달
    public static string MediaFileRoot => $"{AppSourceFileRoot}Medias/";
}
