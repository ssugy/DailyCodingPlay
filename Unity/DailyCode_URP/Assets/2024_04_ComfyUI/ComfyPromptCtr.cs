using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;

[System.Serializable]
public class ResponseData
{
    public string prompt_id;
}
public class ComfyPromptCtr : MonoBehaviour
{
    // 이거는 unity에서 직접 text만들어 놓은 곳에서 btn 함수가 직접 사용함.
    public InputField pInput,nInput,promptJsonInput;
    private void Start()
    {
       // QueuePrompt("pretty man","watermark");
    }

    public void QueuePrompt()
    {
        StartCoroutine(QueuePromptCoroutine(pInput.text,nInput.text));
    }
    private IEnumerator QueuePromptCoroutine(string positivePrompt,string negativePrompt)
    {
        string url = "http://127.0.0.1:8188/prompt";
        string promptText = GeneratePromptJson();
        promptText = promptText.Replace("Pprompt", positivePrompt);
        promptText = promptText.Replace("Nprompt", negativePrompt);
        Debug.Log("변경된 쿼리 : " + promptText);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(promptText);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            request.Dispose();
        }
        else
        {
            Debug.Log("Prompt queued successfully." + request.downloadHandler.text);

            ResponseData data = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
            Debug.Log("Prompt ID: " + data.prompt_id);
            GetComponent<ComfyWebsocket>().promptID = data.prompt_id;
           // GetComponent<ComfyImageCtr>().RequestFileName(data.prompt_id);
        }
    }
    public string promptJson;

private string GeneratePromptJson()
    {
 string guid = Guid.NewGuid().ToString();

    string promptJsonWithGuid = $@"
{{
    ""id"": ""{guid}"",
    ""prompt"": {promptJson}
}}";

    return promptJsonWithGuid;
    }
}
