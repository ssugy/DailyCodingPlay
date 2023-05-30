using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/**
 * Addressable�� �� �� ����ϰ� ����, release�� ����� �Ѵ�.
 * ��巹������ ���۷��� ī��Ʈ�� �����ϴµ�, 1���� �޸𸮿� �ö� ������ ������ü�� �޸𸮿��� �������� �ʴ´�.
 * �׷��� �ݵ�� ��巹����� ����� ���� ������, ����� ���ؼ� �޸� ��ȯ�� �ؾߵǰ�, ��� ��ȯ�ؼ� ���۷��� ī��Ʈ�� 0�� �Ǹ�
 * �� �� ������ �޸𸮿��� ��������.
 */
public class AddressableSample : MonoBehaviour
{
    // ��� 1. ��巹������ �ε��ϴ� ����� 1�� - ���� ���۷��� Ÿ�� �̿� - �ν�����â���� ���� ����
    public AssetReference BaseCube;

    // ��� 3. �ڵ鷯�� �̿��� ���, �̷��� ĳ���ϴ� ������ ���߿� ���� �� �� ���ؼ� �׷���., ������ ����� �����
    AsyncOperationHandle myHandle;

    private void Start()
    {
        // ��� 1 - ������Ʈ �ε��ϱ� -> ���߿� ������ ����� �Ѵ�.
        //BaseCube.LoadAssetAsync<GameObject>().Completed += OnLoadDone;  // �̷��� �ε��ϴ� �̺�Ʈ�� ������ �̺�Ʈ�� ���� �� �ִ�. -> �ű⼭ ��ü����

        // ��� 2 - Instantiate ����� �޸𸮷ε� + ��ü ������ �ѹ��� �̷�� ����. ������ �Ȱ��� �ϸ� �ȴ�.(�����ϸ� ��ü �����)
        //BaseCube.InstantiateAsync().Completed += OnLoadObj;

        // ���3 - ��巹���� Ŭ�������� ����ȣ��, ��巹���� �̸� �ʿ��ϴ�.
        Addressables.LoadAssetAsync<GameObject>("ť��").Completed += (AsyncOperationHandle<GameObject> obj) => { myHandle = obj; GameObject.Instantiate<GameObject>(obj.Result); };
    }

    private void OnLoadObj(AsyncOperationHandle<GameObject> obj)
    {
        //���⿡���� �̹� �޸� �ε� �� �ν��Ͻ� ���� ����
        // �ν��Ͻ��� ������ �޸� ��������ߵǳ�? ��ü ��Ʈ���̵Ǹ� ����������ϳ�. ��쿡 ���� �ٸ� ��
        // �̰� �޸� �����ϸ�, ��ü�� �������.
        //BaseCube.ReleaseInstance(obj.Result);
    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log("��巹���� �ε� �Ϸ� ����� : " + obj.Result.name); // �̷������� ��� �����ϴ�.
        GameObject go = obj.Result;
        Instantiate(go);    // �̷��� �����ϴ°� ��� 1�̴�. - �޸𸮿� �ε� ��, ���� ��ü�� ���� �ø��� ������ �Ѵ�.

        BaseCube.ReleaseAsset();    // �̷������� ��� ������ �޸𸮸� ���� ����� �Ѵ�.
    }
}
