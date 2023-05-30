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
        //Debug.Log("�巡�� ���� " + eventData.selectedObject.name);   // �̰� �ȵ�. null ���´�.
        //Debug.Log("�巡�� ���� " + eventData.lastPress.name);   // �̰͵� �ȵ�. �̺�Ʈ �ڵ鷯�� �ٸ����� �پ �׷���..?

        //Debug.Log(eventData.pointerClick.gameObject);           // null
        Debug.Log(eventData.pointerCurrentRaycast.gameObject);  // ������ ��ü
        Debug.Log(eventData.pointerDrag.gameObject);            // scrollview
        Debug.Log(eventData.pointerEnter.gameObject);           // ������ ��ü - �̰ɷ� ��� ����
        Debug.Log(eventData.pointerId);                         // -1
        //Debug.Log(eventData.pointerPress.gameObject);           // null
        Debug.Log(eventData.pointerPressRaycast.gameObject);    // ������ ��ü
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    scRect.OnBeginDrag(eventData);
    //}
}
