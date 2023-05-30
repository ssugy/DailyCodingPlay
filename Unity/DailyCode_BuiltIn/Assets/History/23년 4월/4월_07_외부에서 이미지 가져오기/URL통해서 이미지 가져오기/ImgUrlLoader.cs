using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public static class ImgUrlLoader
{
    public static IEnumerator DownloadAndSaveImages(string[] urls, string savePath, Action<float> callback = null)
    {
        // ������ ������ ����
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        float progressPerImg = 1f / urls.Length;

        for (int i = 0; i < urls.Length; i++)
        {
            yield return downloadAndSaveImg(urls[i], savePath, i);

            if (callback != null)
            {
                float downloadProgress = (i + 1) * progressPerImg;
                callback.Invoke(downloadProgress);
            }
        }
    }

    private static IEnumerator downloadAndSaveImg(string url, string savePath, int index)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("@@ error message : " + request.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                byte[] bytes = texture.EncodeToPNG();

                // �� �κ� ��Ȳ�� �°� �����ؾ� ��
                string filePath = savePath + "img_" + index + ".png";
                File.WriteAllBytes(filePath, bytes);

                Debug.Log("Saved: " + filePath);
            }
        }
    }
}
