Shader "Custom/PostEffect/ReplaceWithTexture"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass{

            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment frag
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

            float4 col1;
            float tolerance1;
            float4 col2;
            float tolerance2;
            float4 col3;
            float tolerance3;
            TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
            TEXTURE2D_SAMPLER2D(_Tex1, sampler_Tex1);
            float scaleX, scaleY;
            float4 frag(VaryingsDefault i) : SV_TARGET
            {
                float2 uv = i.texcoord.xy;
                float2 texuv= i.texcoord.xy * float2(scaleX, scaleY);
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                float col_sum = col.r + col.g + col.b;
                float col1_sum = col1.r + col1.g + col1.b;
                float col2_sum = col2.r + col2.g + col2.b;
                float col3_sum = col3.r + col3.g + col3.b;
                col = (col1_sum + tolerance1 > col_sum) & (col1_sum - tolerance1 < col_sum)
                    ? SAMPLE_TEXTURE2D(_Tex1, sampler_Tex1, texuv) : col;
                col = (col2_sum + tolerance2 > col) & (col2_sum - tolerance2 < col_sum)
                    ? SAMPLE_TEXTURE2D(_Tex1, sampler_Tex1, texuv) : col;
                col = (col3_sum + tolerance3 > col) & (col3_sum - tolerance3 < col_sum)
                    ? SAMPLE_TEXTURE2D(_Tex1, sampler_Tex1, texuv) : col;

                return col;

            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
