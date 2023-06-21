using UnityEngine;

namespace Akvfx {
    
    // ���̴� �׷����� ��ũ��Ʈ�� ��(����� ��)�� �������ִ� Ŭ����
public sealed class MaterialPropertyBinder : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] DeviceController _device = null;

    #endregion

    #region MonoBehaviour implementation

    Renderer _renderer;             // ���־�����Ʈ�׷����� Renderer
    MaterialPropertyBlock _block;   // material�� ���� ���� �� ��, �̷��� ����� ���ؼ� �����ؾ��� batch�� ����Ǿ ���귮�� Ȯ �پ���.

    void Update()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();
        if (_block == null) _block = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_block);                     // ������(VFX)�ȿ� �ִ� ������Ƽ ����� �����ͼ� _block�� �����Ѵ�.
        _block.SetTexture("_ColorMap", _device.ColorMap);       // ������ _block�� Device�� ColorMap���� �ִ´�.
        _block.SetTexture("_PositionMap", _device.PositionMap); // ������ _block�� Device�� PositionMap���� �ִ´�.
        _renderer.SetPropertyBlock(_block);                     // ����/����(Excute)�Ѵ�.
    }

    #endregion
}

}
