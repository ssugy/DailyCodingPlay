float Sacle;

float4 main(in float2 uv:TEXCOORD0):SV_Target{
    float x = sin(uv.x * Sacle);
    return float4(0,x,0,1);
}