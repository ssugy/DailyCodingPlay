using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RawImageLoader : MonoBehaviour
{
    public RawImage rawImg;
    public Texture2D texture;

    private void Start()
    {
        texture = new Texture2D(0,0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            byte[] tmpBytes = File.ReadAllBytes(Application.dataPath + "/tmp/" + "test.png");
            //texture.LoadRawTextureData(tmpBytes); // ÀÌ°Å´Â ¾ÈµÊ.
            texture.LoadImage(tmpBytes);
            rawImg.texture = texture;
        }
    }
}
