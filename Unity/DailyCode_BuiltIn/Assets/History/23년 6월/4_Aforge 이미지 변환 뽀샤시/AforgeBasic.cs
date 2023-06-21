using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AForge.Video;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;

public class AforgeBasic : MonoBehaviour
{
    public Texture2D mainTexture;
    public BitmapData imageData;

    private void Start()
    {
        AdaptiveSmoothing filter = new AdaptiveSmoothing();
        filter.ApplyInPlace(imageData);
    }
}
