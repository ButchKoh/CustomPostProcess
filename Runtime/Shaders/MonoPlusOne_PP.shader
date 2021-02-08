Shader "Custom/PostEffect/MonoPlusOne"
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
            
            float3 rgb2hsv(float3 In)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
                float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
                float D = Q.x - min(Q.w, Q.y);
                float  E = 1e-10;
                return float3(abs(Q.z + (Q.w - Q.y) / (6.0 * D + E)), D / (Q.x + E), Q.x);
            }

            TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
            float4 frag(VaryingsDefault i) : SV_TARGET
            {
                float2 uv = i.texcoord.xy;
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                float col_h = frac(rgb2hsv(col.rgb).x);
                float col1_h = frac(rgb2hsv(col1.rgb).x);
                float mono = 0.30 * col.r + 0.59 * col.g + 0.11 * col.b;
                float tmpN = frac(col1_h - tolerance1);
                tmpN = tmpN == 0 ? 0.001 : tmpN;
                float tmpP = frac(col1_h + tolerance1);
                tmpP = tmpP == 0 ? 0.001 : tmpP;
                return (tmpN > col_h) & (tmpP < col_h) ? mono : col;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
