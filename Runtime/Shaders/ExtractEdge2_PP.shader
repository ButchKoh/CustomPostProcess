Shader "Custom/PostEffect/ExtractEdge2"
{
        SubShader
        {
            Cull Off ZWrite Off ZTest Always

            Pass{

                HLSLPROGRAM
                #pragma vertex VertDefault
                #pragma fragment frag
                #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

                float _EdgeSize;
                float _Threshold;
                float _Param;
                float _Multiply;
                int _DrawBG;
                float4 _BaseColor;
                TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
                TEXTURE2D_SAMPLER2D(_CameraNormalsTexture, sampler_CameraNormalsTexture);
                TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
                float4 _MainTex_TexelSize;
                
                float4 frag(VaryingsDefault i) : SV_TARGET
                {
                    float EdgeWidth = _EdgeSize * _MainTex_TexelSize.x;
                    float2 uv = i.texcoord.xy;

                    float2 uvRightUp   = float2(uv.x + EdgeWidth, uv.y + EdgeWidth);
                    float2 uvLeftUp    = float2(uv.x - EdgeWidth, uv.y + EdgeWidth);
                    float2 uvRightDown = float2(uv.x + EdgeWidth, uv.y - EdgeWidth);
                    float2 uvLeftDown  = float2(uv.x - EdgeWidth, uv.y - EdgeWidth);

                    float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                    float4 colRU   = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvRightUp);
                    float4 colLU    = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvLeftUp);
                    float4 colRD = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvRightDown);
                    float4 colLD  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvLeftDown);
                    float4 colEdge = sqrt(pow(colRU - colLD, 2) + pow(colLU - colRD, 2));

                    float depthRU = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvRightUp).r;
                    float depthLU = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvLeftUp).r;
                    float depthRD = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvRightDown).r;
                    float depthLD = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvLeftDown).r;
                    float4 depthEdge = (sqrt(pow(depthRU - depthLD, 2) + pow(depthLU - depthRD, 2)) * 100).xxxx;

                    float3 normalRU = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvRightUp).rgb;
                    float3 normalLU = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvLeftUp).rgb;
                    float3 normalRD = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvRightDown).rgb;
                    float3 normalLD = SAMPLE_TEXTURE2D(_CameraNormalsTexture, sampler_CameraNormalsTexture, uvLeftDown).rgb;
                    float4 normalEdge = float4((sqrt(pow(normalRU - normalLD, 2) + pow(normalLU - normalRD, 2)) ), 1);

                    float4 tmp = saturate((depthEdge * _Param + (1 - _Param) * normalEdge) * _Multiply);
                    float outline = step(_Threshold, max(tmp.r, max(tmp.g, tmp.b)));
                    float4 color = outline == float4(0, 0, 0, 1) ? col : outline * _BaseColor;

                    return _DrawBG == 1 ? color : outline * _BaseColor;
                }
                ENDHLSL
            }
        }
        FallBack "Diffuse"
}
