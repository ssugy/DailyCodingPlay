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

        // 값이 일정하지 않음.(비동기같은기분 - grid로 제어하기 때문에 비동기처럼 느껴지는 것 같다)
        
        rawImgRectTR.sizeDelta = new Vector2(0, gridLayoutGroup.cellSize.y / 2);   // 이 값은, Cellsize이기 때문에
        //rawImgRectTR.localPosition = new Vector3(0, -gridLayoutGroup.cellSize.y / 4, 0);
        rawImgRectTR.anchoredPosition = new Vector3(0, -(gridLayoutGroup.cellSize.y / 2), 0); // Pos Y Z는 이걸로 변경한다.
    }

}
