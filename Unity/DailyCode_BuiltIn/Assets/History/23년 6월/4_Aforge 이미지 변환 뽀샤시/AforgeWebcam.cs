using UnityEngine;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Controls;
using System.Drawing;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;
using System.Drawing.Imaging;

public class AforgeWebcam : MonoBehaviour
{
    private FilterInfoCollection videoDevices;
    private VideoCaptureDevice videoSource;
    private Mirror mirrorFilter;
    private Sepia sepiaFilter;

    public UnityEngine.UI.RawImage img;

    public RawImage display;
    WebCamTexture camTexture;
    private int currentIndex = 0;

    private void Start()
    {
        if (camTexture != null)
        {
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }
        WebCamDevice device = WebCamTexture.devices[currentIndex];
        camTexture = new WebCamTexture(device.name);
        display.texture = camTexture;
        camTexture.Play();

        //Aforge
        mirrorFilter = new Mirror(false, true); // 세로 미러
        sepiaFilter = new Sepia();

        // Initialize video source with the first device
        videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
        videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);

        // Start capturing from the video source
        videoSource.Start();

    }

    private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        // Apply filters to the video frame
        Bitmap filteredFrame = ApplyFilters((Bitmap)eventArgs.Frame.Clone());

        // Display the filtered frame in a PictureBox
        display.texture = ConvertBitmapToTexture2D(filteredFrame);
    }

    private Bitmap ApplyFilters(Bitmap inputFrame)
    {
        // Apply the "gorgeous" filters to the input frame

        // Apply mirror filter (horizontal flip)
        Bitmap mirroredFrame = mirrorFilter.Apply(inputFrame);

        // Apply sepia filter
        Bitmap filteredFrame = sepiaFilter.Apply(mirroredFrame);

        return filteredFrame;
    }

    private void OnApplicationQuit()
    {
        if (videoSource != null && videoSource.IsRunning)
        {
            // Stop capturing and release resources
            videoSource.SignalToStop();
            videoSource.WaitForStop();
            videoSource = null;
        }
    }

    /// 비트맵 이미지를 Texture2d로 변환하는 함수
    public Texture2D ConvertBitmapToTexture2D(Bitmap bitmap)
    {
        // Create a new Texture2D with the same width and height as the Bitmap
        Texture2D texture = new Texture2D(bitmap.Width, bitmap.Height);

        // Lock the texture for writing pixel data
        var rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
        var data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        // Copy the pixel data from the Bitmap to the Texture2D
        byte[] bytes = new byte[data.Stride * data.Height];
        System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
        texture.LoadRawTextureData(bytes);
        texture.Apply();

        // Unlock the Bitmap and release its resources
        bitmap.UnlockBits(data);

        return texture;
    }
}
