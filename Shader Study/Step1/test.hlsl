float iTime;    // built-in
float Intensity;
float Scale;

float TimeSpeed;
float2 Offset;

float4 main(in float2 uv:TEXCOORD0) :SV_Target
{
    uv.x += iTime + TimeSpeed;
    return float4(1,1,1,1);
}