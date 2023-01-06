using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/**
 * Addressable은 꼭 다 사용하고 나면, release를 해줘야 한다.
 * 어드레서블은 레퍼런스 카운트가 존재하는데, 1개라도 메모리에 올라가 있으면 번들자체가 메모리에서 내려가지 않는다.
 * 그렇기 반드시 어드레서블로 사용이 끝난 에셋은, 릴리즈를 통해서 메모리 반환을 해야되고, 모두 반환해서 레퍼런스 카운트가 0이 되면
 * 그 때 번들이 메모리에서 내려간다.
 */
public class AddressableSample : MonoBehaviour
{
    // 방법 1. 어드레서블을 로드하는 방법중 1번 - 에셋 레퍼런스 타입 이용 - 인스펙터창에서 직접 연결
    public AssetReference BaseCube;

    // 방법 3. 핸들러를 이용한 방법, 이렇게 캐싱하는 이유는 나중에 해제 할 때 편해서 그렇다., 실제로 방법은 비슷함
    AsyncOperationHandle myHandle;

    private void Start()
    {
        // 방법 1 - 오브젝트 로드하기 -> 나중에 해제도 해줘야 한다.
        //BaseCube.LoadAssetAsync<GameObject>().Completed += OnLoadDone;  // 이렇게 로드하는 이벤트가 끝나면 이벤트를 붙일 수 있다. -> 거기서 객체생성

        // 방법 2 - Instantiate 방법은 메모리로드 + 객체 생성이 한번에 이루어 진다. 해제는 똑같이 하면 된다.(해제하면 객체 사라짐)
        //BaseCube.InstantiateAsync().Completed += OnLoadObj;

        // 방법3 - 어드레서블 클래스에서 직접호출, 어드레서블 이름 필요하다.
        Addressables.LoadAssetAsync<GameObject>("큐브").Completed += (AsyncOperationHandle<GameObject> obj) => { myHandle = obj; GameObject.Instantiate<GameObject>(obj.Result); };
    }

    private void OnLoadObj(AsyncOperationHandle<GameObject> obj)
    {
        //여기에서는 이미 메모리 로드 및 인스턴스 같이 진행
        // 인스턴스가 끝나면 메모리 해제해줘야되나? 객체 디스트로이되면 해제해줘야하나. 경우에 따라 다를 듯
        // 이거 메모리 해제하면, 객체가 사라진다.
        //BaseCube.ReleaseInstance(obj.Result);
    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log("어드레서블 로드 완료 결과물 : " + obj.Result.name); // 이런식으로 사용 가능하다.
        GameObject go = obj.Result;
        Instantiate(go);    // 이렇게 생성하는게 방법 1이다. - 메모리에 로드 후, 실제 객체를 씬에 올리는 행위를 한다.

        BaseCube.ReleaseAsset();    // 이런식으로 모두 끝나고 메모리를 해제 해줘야 한다.
    }
}
