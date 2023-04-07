using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectCheck : MonoBehaviour
{
    private RawImage loopingRawImg;
    private RectTransform rawImgRectTR;
    private GridLayoutGroup gridLayoutGroup;

    private void Start()
    {
        loopingRawImg = transform.GetChild(0).GetComponent<RawImage>();
        rawImgRectTR = loopingRawImg.transform.GetComponent<RectTransform>();
        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();

        // ���� �������� ����.(�񵿱ⰰ����� - grid�� �����ϱ� ������ �񵿱�ó�� �������� �� ����)
        
        rawImgRectTR.sizeDelta = new Vector2(0, gridLayoutGroup.cellSize.y / 2);   // �� ����, Cellsize�̱� ������
        //rawImgRectTR.localPosition = new Vector3(0, -gridLayoutGroup.cellSize.y / 4, 0);
        rawImgRectTR.anchoredPosition = new Vector3(0, -(gridLayoutGroup.cellSize.y / 2), 0); // Pos Y Z�� �̰ɷ� �����Ѵ�.
    }

}
