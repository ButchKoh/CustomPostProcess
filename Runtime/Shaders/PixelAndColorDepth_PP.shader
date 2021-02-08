Shader "Custom/PostEffect/PixelAndColorDepth"
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
            int _PixelX;
            int _PixelY;
            int _Rbit;
            int _Gbit;
            int _Bbit;

            float4 frag(VaryingsDefault i) : SV_Target
            {
                float2 ratio = float2(_PixelX,_PixelY);
                float2 uv = round(i.texcoord.xy * ratio) / ratio;
                
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                int Rpow = _Rbit == 0 ? 256 : pow(2, _Rbit);
                int Gpow = _Gbit == 0 ? 256 : pow(2, _Gbit);
                int Bpow = _Bbit == 0 ? 256 : pow(2, _Bbit);
                col.r = round(col.r * pow(2, _Rbit)) / pow(2, _Rbit);
                col.g = round(col.g * pow(2, _Gbit)) / pow(2, _Gbit);
                col.b = round(col.b * pow(2, _Bbit)) / pow(2, _Bbit);
                return col;
            }
            ENDHLSL
        }
    }
}
