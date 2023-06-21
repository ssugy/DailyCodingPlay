using UnityEngine;

namespace Akvfx {
    
    // 쉐이더 그래프와 스크립트의 값(장비의 값)을 연결해주는 클래스
public sealed class MaterialPropertyBinder : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] DeviceController _device = null;

    #endregion

    #region MonoBehaviour implementation

    Renderer _renderer;             // 비주얼이펙트그래프의 Renderer
    MaterialPropertyBlock _block;   // material의 값을 변경 할 때, 이렇게 블록을 통해서 수정해야지 batch가 적용되어서 연산량이 확 줄어든다.

    void Update()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();
        if (_block == null) _block = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_block);                     // 렌더러(VFX)안에 있는 프로퍼티 블록을 가져와서 _block에 저장한다.
        _block.SetTexture("_ColorMap", _device.ColorMap);       // 가져온 _block에 Device의 ColorMap값을 넣는다.
        _block.SetTexture("_PositionMap", _device.PositionMap); // 가져온 _block에 Device의 PositionMap값을 넣는다.
        _renderer.SetPropertyBlock(_block);                     // 실행/적용(Excute)한다.
    }

    #endregion
}

}
