using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollEventItemChecker : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    // �̷��� ������ ���������� �ȵ�.
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("item �巡�� ����");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("item �巡�� ����");
    }
}
