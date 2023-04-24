using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollEventChecker : MonoBehaviour, IEndDragHandler
{
    ScrollRect scRect;
    private void Start()
    {
        scRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scRect.OnEndDrag(eventData);
        //Debug.Log("드래그 종료 " + eventData.selectedObject.name);   // 이거 안됨. null 나온다.
        //Debug.Log("드래그 종료 " + eventData.lastPress.name);   // 이것도 안됨. 이벤트 핸들러가 다른데에 붙어서 그런가..?

        //Debug.Log(eventData.pointerClick.gameObject);           // null
        Debug.Log(eventData.pointerCurrentRaycast.gameObject);  // 선택한 객체
        Debug.Log(eventData.pointerDrag.gameObject);            // scrollview
        Debug.Log(eventData.pointerEnter.gameObject);           // 선택한 객체 - 이걸로 사용 예정
        Debug.Log(eventData.pointerId);                         // -1
        //Debug.Log(eventData.pointerPress.gameObject);           // null
        Debug.Log(eventData.pointerPressRaycast.gameObject);    // 선택한 객체
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    scRect.OnBeginDrag(eventData);
    //}
}
