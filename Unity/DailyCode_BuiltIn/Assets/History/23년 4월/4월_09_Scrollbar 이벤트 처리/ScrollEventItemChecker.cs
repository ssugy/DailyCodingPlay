using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollEventItemChecker : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    // 이렇게 아이템 직접연결은 안됨.
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("item 드래그 시작");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("item 드래그 종료");
    }
}
