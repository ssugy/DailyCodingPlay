Shader "Unlit/UnlitShader" //Unlit �̶�� ���� �ȿ� UnlitShader��� �̸��� ���̴���� ��
{
    Properties //���Ⱑ ���̴� �� �ڵ�, ����Ƽ ���͸��󿡼� ����Ǵ� ������Ƽ���� �����Ѵ�.
    {
        _MainTex ("Texture", 2D) = "white" {}
        //_���۷��� �̸� ("����Ƽ ������Ƽ���� �������� �̸�", �ڷ���) = "�ڷ����� �´� �⺻ ��"{}
    }
    SubShader
    {
        Tags {
        "RenderType"="Opaque" //���� Ÿ���� �������̶�� ����
        					  //������ = "Opaque", ���� Ŭ�� "TransparentCutout"
                              //������ = "Transparent", ���(��ī�̹ڽ�) = "Background"
                              //��������(GUI �ؽ���, �ı� �� ȿ��) = "Overlay"
        "RenderPipeline" = "UniversalPipeline"
        "Queue" = "Geometry" //��� = "Background", ������ = "Geometry"
        //���� Ŭ�� = "AlphaTest", ������ = "Transparent", �������� = "Overlay"
        }
        LOD 100 //Subshader�� ���� ����� ���̴��� �����,
        //�� ���� LOD ���� �̿��� ���� ������ ��⿡�� ���� ����� ���̴���
        //���ư� �� �ֵ��� ������ �� �ִٰ� �Ѵ�. ���� ���̴� �������� ��������
        //��ũ��Ʈ���� Maximum LOD ��ġ�� ���� ��������Ѵٰ� �Ѵ�.

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag //���̴��� ��� ������ ������ �����ϴ�
            					  //��ó�� �����̶�� �Ѵ�.

            // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting.hlsl"
            //HLSL���̴� ���̺귯���� ����Ѵٴ� ��

            struct appdata //�� �������� ����� ���ؽ� ������ �޴´�. appdata, Attributes ������ �ַ� �̸��� ���
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f //v2f�� vertex to fragment ��� �ǹ̴�. �տ��� ���� ���ؽ� ������ �����׸�Ʈ�� �����ֱ� ���� �����ϴ� ����
            //���ؽ����� �ȼ� �����ͷ� ���� �������� �����ϴ� �Ŷ�� �����ߴ�.
            //������ ������ ���ִ� ��?
            //v2f��, Varyings��� �̸����� �ַ� ���
            
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            sampler2D _MainTex;
            
            //������Ƽ���� ������ ���۷����� ���ؽ�, �����׸�Ʈ ���̴����� ����ϱ� ���� ������ ��������� �Ѵ�.
            //sampler2D�� ���÷��� �ؽ��ĸ� �ѹ��� ������ ���̰�, ���� �÷������� ���Ǵ� ����̶�� �Ѵ�.
            //�ٸ� �����Ⱑ ������ ��, �����ϰ� ����� �� ���� ������ �Ʒ�ó�� ���÷��� �ؽ��ĸ� ���� �������ִ°� ���ٰ� �Ѵ�.
            
            //�� ������ �ؽ��Ŀ� ���÷��� ������ ������ ���� ǥ���� �� �ִ�.
            //Texture2D _MainTex;
            //SamplerState sampler_MainTex;
            
            //���� �������� ��ũ�� ����
            //TEXTURE2D (_MainTex);
            //SAMPLER(sampler_MainTex);


			CBUFFER_START(UnityPerMatial)
				float4 _MainTex_ST;
            	//�ؽ���_ST; �� �������ִ� ����: ���ؽ��� �����׸�Ʈ ���̴�����
                //�ؽ����� Ÿ�ϸ��� �������� �̿��ϴ� �Լ��� �ִµ�
            	//�ű⿡ ������ ���� ������ �̸� ��������� �Ѵ�.
            CBUFFER_END
            
            //CBUFFER ����ϴ� ���� : SRP Batcher�� ȣȯ�ǰԲ� ���� �� ����
            //å�� ��� : �ؽ��Ŀ� ���÷��� ������ ��� �������� CBUFFER START�� END�� �����ش�.
            
            //�������� ���� �����̾��µ�, �Ʒ� ���ؽ��� �ȼ� ���̴����� ����� ������
            //�̸� �����ؾ� ��� �����ϴ� ���ؽ�, �����׸�Ʈ ���̴� �������� �������� ��
            
            v2f vert (appdata v) //���Ⱑ ���ؽ� ���̴�. �Ű����� v�� ����, o���� ��ȯ
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //TRANSFORM_TEX(v.uv, _MainTex)�� ��ũ���ε�, ������
                //v.uv.xy * _MainTex_ST.xy +_MainTex_ST.zw;
                
                return o;
            }

            half4 frag (v2f i) : SV_Target //���Ⱑ �����׸�Ʈ ���̴���.
            //CG������ void(�ڷ��� �������� ����) surf�� �����ϰ�
            //inout SurfaceOutputStandard o�� ��ȯ�ϴ� ����������,
            //HLSL������ half4��� �ڷ��� �Լ��� v2f i�� �Ű������� ����
            //half4���� rgba���� ��ȯ�ϰ� �����.
            //���� �� �غ����� void�� �Ἥ �����׸�Ʈ ���̴��� �ۼ��� �� ���� �� ����?
            
            {
                half4 col = tex2D(_MainTex, i.uv);
                //�� tex2D(_MainTex, i.uv);�κ��� �ռ� ���÷��� �ؽ��ķ� ������
                //�� ���� ������� �ۼ��� �� �ִ�.
              
                //half4 color = _MainTex.Sample(sampler_MainTex, uv)
                
                //��ũ�θ� �̿��ϸ� ������ ����.
                //half4 color = SAMPLER_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                //���������� ��ũ�� ����� �����ؼ� ������ ���ԵǴ� �� ����.
                
                return color;
            }
            ENDHLSL
        }
    }
}