Shader "Custom/PostEffect/Normal"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass{

            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment frag
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

            int _Edge;
                TEXTURE2D_SAMPLER2D(_CameraNormalsTexture, sampler_CameraNormalsTexture);
                float4 _MainTex_TexelSize;

                float4 frag(VaryingsDefault i) : SV_TARGET
                {
                    float EdgeWidth = _MainTex_TexelSize.x;
                    float2 uv = i.texcoord.xy;

                    float2 uvRightUp = float2(uv.x + EdgeWidth, uv.y + EdgeWidth);
                    float2 uvLeftUp = float2(uv.x - EdgeWidth, uv.y + EdgeWidth);
                    float2 uvRightDown = float2(uv.x + EdgeWidth, uv.y - EdgeWidth);
                    float2 uvLeftDown = float2(uv.x - EdgeWidth, uv.y - EdgeWidth);

                    float3 normalRU = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvRightUp).rgb;
                    float3 normalLU = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvLeftUp).rgb;
                    float3 normalRD = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvRightDown).rgb;
                    float3 normalLD = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvLeftDown).rgb;
                    float4 normalEdge = float4((sqrt(pow(normalRU - normalLD, 2) + pow(normalLU - normalRD, 2))), 1);

                    return _Edge == 1 ? normalEdge : SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uv);
                }
                ENDHLSL
        }
    }
    FallBack "Diffuse"
}
