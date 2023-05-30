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
                Debug.Log("��� �� : " + paths[0]);
            }
            ,() => { Debug.Log("ĵ��"); }
            ,FileBrowser.PickMode.Files);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FileBrowser.ShowSaveDialog((paths) =>
            {
                Debug.Log("��� �� : " + paths[0]);
            }
            , () => { Debug.Log("ĵ��"); }
            , FileBrowser.PickMode.Files);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ���� ���ϴ� �����, ��θ� �ָ� �ش� ������ �����ϸ� �� ������ �ε��ϴ� ��.
        }
    }
}
