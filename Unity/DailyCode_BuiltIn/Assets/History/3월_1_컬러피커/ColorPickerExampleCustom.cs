using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerExampleCustom : MonoBehaviour
{
    private Renderer r;
    void Start()
    {
        r = GetComponent<Renderer>();
        r.sharedMaterial = r.material;

        // 최초 생성할 때, setColor로 값을 전달함.
        ColorPicker.Create(Color.red, "Choose the cube's color!", SetColor, ColorFinished, true);
        ColorPicker.Done(); // 닫기 버튼 실행(꺼두기)
    }
    public void ChooseColorButtonClick()
    {
        //ColorPicker.Create(r.sharedMaterial.color, "Choose the cube's color!", SetColor, ColorFinished, true);
        ColorPicker.Create(r.sharedMaterial.color, "Choose the cube's color!", SetColor, ColorFinished, true);

    }
    private void SetColor(Color currentColor)
    {
        Debug.Log("셋 컬러 실행");
        r.sharedMaterial.color = currentColor;
    }

    private void ColorFinished(Color finishedColor)
    {
        Debug.Log("컬러 피니시드");
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
    }
}
