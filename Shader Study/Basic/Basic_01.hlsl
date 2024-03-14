// Vertex Shader
struct VSInput {
    float3 position : POSITION;
};

struct VSOutput {
    float4 position : SV_POSITION;
};

VSOutput VS_Main(VSInput input) {
    VSOutput output;
    output.position = float4(input.position, 1.0f);
    return output;
}

// Pixel Shader
float4 main() : SV_TARGET {
    return float4(1.0f, 0.0f, 0.0f, 1.0f); // Red color
}