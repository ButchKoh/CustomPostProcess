Shader "Custom/PostEffect/HSV_ST"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment frag
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

            TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
            float4 _MainTex_ST;
            float _HueScale;
            float _HueOffset;
            float _SaturationScale;
            float _SaturationOffset;
            float _ValueScale;
            float _ValueOffset;
            float3 rgb2hsv(float3 In)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
                float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
                float D = Q.x - min(Q.w, Q.y);
                float  E = 1e-10;
                return float3(abs(Q.z + (Q.w - Q.y) / (6.0 * D + E)), D / (Q.x + E), Q.x);
            }
            float3 hsv2rgb(float3 In)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
                return In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
            }

            float4 frag(VaryingsDefault i) : SV_Target
            {
                float2 uv = i.texcoord;
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                float3 col2 = rgb2hsv(col.rgb);
                col2.x = col2.x * _HueScale + _HueOffset;
                col2.y = col2.y * _SaturationScale + _SaturationOffset;
                col2.z = col2.z * _ValueScale + _ValueOffset;
                return float4(hsv2rgb(col2), 1);
            }
             ENDHLSL
        }
    }
}