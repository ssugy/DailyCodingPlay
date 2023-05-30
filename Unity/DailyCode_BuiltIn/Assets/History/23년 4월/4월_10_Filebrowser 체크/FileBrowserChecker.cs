using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileBrowserChecker : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FileBrowser.ShowLoadDialog((paths) =>
            {
                Debug.Log("경로 등 : " + paths[0]);
            }
            ,() => { Debug.Log("캔슬"); }
            ,FileBrowser.PickMode.Files);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FileBrowser.ShowSaveDialog((paths) =>
            {
                Debug.Log("경로 등 : " + paths[0]);
            }
            , () => { Debug.Log("캔슬"); }
            , FileBrowser.PickMode.Files);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 내가 원하는 기능은, 경로를 주면 해당 파일이 존재하면 그 파일을 로드하는 것.
        }
    }
}
