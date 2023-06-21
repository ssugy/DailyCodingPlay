using UnityEngine;

namespace Akvfx {

public sealed class DeviceController : MonoBehaviour
{
    #region Editable attribute

    [SerializeField] DeviceSettings _deviceSettings = null;

    public DeviceSettings DeviceSettings
      { get => _deviceSettings; set => SetDeviceSettings(value); }

    #endregion

    #region Asset reference

    [SerializeField, HideInInspector] ComputeShader _compute = null;

    #endregion

    #region Public accessor properties

    public RenderTexture ColorMap => _colorMap;
    public RenderTexture PositionMap => _positionMap;

    #endregion

    #region Private members

    ThreadedDriver _driver;
    ComputeBuffer _xyTable;         // computeBuffer는 ComputeShader용 버퍼의 역할을 한다.
    ComputeBuffer _colorBuffer;
    ComputeBuffer _depthBuffer;
    RenderTexture _colorMap;
    RenderTexture _positionMap;

    // 외부에서 가져온 DeviceSettings라는 스크립터블 오브젝트를 가져와서 데이터를 초기화 한다.
    void SetDeviceSettings(DeviceSettings settings)
    {
        _deviceSettings = settings;
        if (_driver != null) _driver.Settings = settings;
    }

    #endregion

    #region Shader property IDs

    static class ID
    {
        public static int ColorBuffer = Shader.PropertyToID("ColorBuffer");
        public static int DepthBuffer = Shader.PropertyToID("DepthBuffer");
        public static int XYTable     = Shader.PropertyToID("XYTable");
        public static int MaxDepth    = Shader.PropertyToID("MaxDepth");
        public static int ColorMap    = Shader.PropertyToID("ColorMap");
        public static int PositionMap = Shader.PropertyToID("PositionMap");
    }

    #endregion

    #region MonoBehaviour implementation

    void Start()
    {
        // Start capturing via the threaded driver.
        _driver = new ThreadedDriver(_deviceSettings);

        // Temporary objects for conversion
        var width = ThreadedDriver.ImageWidth;
        var height = ThreadedDriver.ImageHeight;

        _colorBuffer = new ComputeBuffer(width * height, 4);
        _depthBuffer = new ComputeBuffer(width * height / 2, 4);

        _colorMap = new RenderTexture
          (width, height, 0, RenderTextureFormat.Default);
        _colorMap.enableRandomWrite = true;
        _colorMap.Create();

        _positionMap = new RenderTexture
          (width, height, 0, RenderTextureFormat.ARGBFloat);
        _positionMap.enableRandomWrite = true;
        _positionMap.Create();
    }

    void OnDestroy()
    {
        if (_colorMap    != null) Destroy(_colorMap);
        if (_positionMap != null) Destroy(_positionMap);

        _colorBuffer?.Dispose();
        _depthBuffer?.Dispose();

        _xyTable?.Dispose();
        _driver?.Dispose();
    }

    // unsafe붙은 것은 포인터를 통해서 값을 받는것이라서 안정성을 보장 못하기 때문이다.
    unsafe void Update()
    {
        // Try initializing XY table if it's not ready.
        if (_xyTable == null)
        {
            var data = _driver.XYTable;
            if (data.IsEmpty) return; // Table is not ready.

            // Allocate and initialize the XY table.
            _xyTable = new ComputeBuffer(data.Length, sizeof(float));
            _xyTable.SetData(data);
        }

        // Try retrieving the last frame.
        var (color, depth) = _driver.LockLastFrame();
        if (color.IsEmpty || depth.IsEmpty) return;

        // Load the frame data into the compute buffers.
        _colorBuffer.SetData(color.Span);
        _depthBuffer.SetData(depth.Span);

        // We don't need the last frame any more.
        _driver.ReleaseLastFrame();

        // 컴퓨터 쉐이더에 마지막 프레임의 값을 지정한다.
        // ID.로 지정된 쉐이더와 연결된 값에 마지막 프레임에서 추출한 값들을 지정하는 코드이다.
        // Invoke the unprojection compute shader.
        _compute.SetFloat(ID.MaxDepth, _deviceSettings.maxDepth);
        _compute.SetBuffer(0, ID.ColorBuffer, _colorBuffer);
        _compute.SetBuffer(0, ID.DepthBuffer, _depthBuffer);
        _compute.SetBuffer(0, ID.XYTable, _xyTable);
        _compute.SetTexture(0, ID.ColorMap, _colorMap);
        _compute.SetTexture(0, ID.PositionMap, _positionMap);
        _compute.Dispatch(0, _colorMap.width / 8, _colorMap.height / 8, 1);     // 지정한 값을 실행(Excute)한다.


    }

    #endregion
}

}
