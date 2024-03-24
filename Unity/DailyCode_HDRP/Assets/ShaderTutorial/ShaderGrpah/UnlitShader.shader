Shader "Unlit/UnlitShader" //Unlit 이라는 폴더 안에 UnlitShader라는 이름의 쉐이더라는 뜻
{
    Properties //여기가 쉐이더 랩 코드, 유니티 매터리얼에서 노출되는 프로퍼티들을 선언한다.
    {
        _MainTex ("Texture", 2D) = "white" {}
        //_레퍼런스 이름 ("유니티 프로퍼티에서 보여지는 이름", 자료형) = "자료형에 맞는 기본 값"{}
    }
    SubShader
    {
        Tags {
        "RenderType"="Opaque" //렌더 타입이 불투명이라는 선언
        					  //불투명 = "Opaque", 알파 클립 "TransparentCutout"
                              //반투명 = "Transparent", 배경(스카이박스) = "Background"
                              //오버레이(GUI 텍스쳐, 후광 등 효과) = "Overlay"
        "RenderPipeline" = "UniversalPipeline"
        "Queue" = "Geometry" //배경 = "Background", 불투명 = "Geometry"
        //알파 클립 = "AlphaTest", 반투명 = "Transparent", 오버레이 = "Overlay"
        }
        LOD 100 //Subshader로 여러 사양의 쉐이더를 만들고,
        //그 안의 LOD 값을 이용해 낮은 성능의 기기에서 낮은 사양의 쉐이더가
        //돌아갈 수 있도록 조절할 수 있다고 한다. 낮은 쉐이더 성능으로 돌리려면
        //스크립트에서 Maximum LOD 수치를 따로 정해줘야한다고 한다.

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag //쉐이더가 어떻게 컴파일 될지를 지시하는
            					  //전처리 문법이라고 한다.

            // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting.hlsl"
            //HLSL쉐이더 라이브러리를 사용한다는 뜻

            struct appdata //이 구조에서 사용할 버텍스 정보를 받는다. appdata, Attributes 등으로 주로 이름을 사용
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f //v2f는 vertex to fragment 라는 의미다. 앞에서 받은 버텍스 정보를 프래그먼트로 보내주기 위해 선언하는 영역
            //버텍스에서 픽셀 데이터로 보낼 정보값을 선언하는 거라고 인지했다.
            //보간기 역할을 해주는 듯?
            //v2f나, Varyings라는 이름으로 주로 사용
            
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            sampler2D _MainTex;
            
            //프로퍼티에서 선언한 레퍼런스를 버텍스, 프래그먼트 쉐이더에서 사용하기 위해 변수로 선언해줘야 한다.
            //sampler2D는 샘플러와 텍스쳐를 한번에 선언한 것이고, 구형 플랫폼에서 사용되는 방법이라고 한다.
            //다만 보간기가 많아질 때, 간략하게 사용할 수 없기 때문에 아래처럼 샘플러와 텍스쳐를 따로 선언해주는게 좋다고 한다.
            
            //위 내용을 텍스쳐와 샘플러로 나누면 다음과 같이 표현할 수 있다.
            //Texture2D _MainTex;
            //SamplerState sampler_MainTex;
            
            //같은 내용으로 매크로 사용시
            //TEXTURE2D (_MainTex);
            //SAMPLER(sampler_MainTex);


			CBUFFER_START(UnityPerMatial)
				float4 _MainTex_ST;
            	//텍스쳐_ST; 를 선언해주는 이유: 버텍스나 프래그먼트 쉐이더에서
                //텍스쳐의 타일링과 오프셋을 이용하는 함수가 있는데
            	//거기에 변수로 들어가기 때문에 미리 선언해줘야 한다.
            CBUFFER_END
            
            //CBUFFER 사용하는 이유 : SRP Batcher에 호환되게끔 만들 수 있음
            //책의 요약 : 텍스쳐와 샘플러를 제외한 모든 변수들은 CBUFFER START와 END에 묶어준다.
            
            //위에까지 변수 선언이었는데, 아래 버텍스와 픽셀 쉐이더에서 사용할 변수를
            //미리 선언해야 사용 가능하니 버텍스, 프래그먼트 쉐이더 앞쪽으로 선언해준 것
            
            v2f vert (appdata v) //여기가 버텍스 쉐이더. 매개변수 v를 갖고, o값을 반환
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //TRANSFORM_TEX(v.uv, _MainTex)는 매크로인데, 원형은
                //v.uv.xy * _MainTex_ST.xy +_MainTex_ST.zw;
                
                return o;
            }

            half4 frag (v2f i) : SV_Target //여기가 프래그먼트 쉐이더다.
            //CG언어에서는 void(자료형 정해지지 않음) surf로 선언하고
            //inout SurfaceOutputStandard o를 반환하는 구조였지만,
            //HLSL에서는 half4라는 자료형 함수로 v2f i를 매개변수로 갖고
            //half4값인 rgba값을 반환하게 만든다.
            //아직 안 해봤지만 void를 써서 프래그먼트 쉐이더를 작성할 수 있을 것 같다?
            
            {
                half4 col = tex2D(_MainTex, i.uv);
                //이 tex2D(_MainTex, i.uv);부분은 앞서 샘플러와 텍스쳐로 나눴던
                //두 가지 방법으로 작성할 수 있다.
              
                //half4 color = _MainTex.Sample(sampler_MainTex, uv)
                
                //매크로를 이용하면 다음과 같다.
                //half4 color = SAMPLER_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                //개인적으로 매크로 방식이 간략해서 이쪽을 쓰게되는 것 같다.
                
                return color;
            }
            ENDHLSL
        }
    }
}