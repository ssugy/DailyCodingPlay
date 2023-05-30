using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadExternalImg : MonoBehaviour
{
    RawImage rawImage;
    byte[] imageData;
    private string imgPath;
    private Texture2D texture;

    private void Start()
    {
        imgPath = Application.dataPath + "/../frame-00011574.png";
        rawImage = GetComponent<RawImage>();
        SystemIOFileLoad();
    }

    private void SystemIOFileLoad()
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes(imgPath);
        if (byteTexture.Length > 0)
        {
            texture = new Texture2D(0,0);
            texture.LoadImage(byteTexture);
            texture.wrapMode= TextureWrapMode.Clamp;
            rawImage.texture= texture;
        }
    }
}
