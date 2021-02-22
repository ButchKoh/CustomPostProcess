Shader "Custom/PostEffect/Depth"
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
            TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
            float4 _MainTex_TexelSize;

            float4 frag(VaryingsDefault i) : SV_TARGET
            {
                float EdgeWidth = _MainTex_TexelSize.x;
                float2 uv = i.texcoord.xy;

                float2 uvRightUp = float2(uv.x + EdgeWidth, uv.y + EdgeWidth);
                float2 uvLeftUp = float2(uv.x - EdgeWidth, uv.y + EdgeWidth);
                float2 uvRightDown = float2(uv.x + EdgeWidth, uv.y - EdgeWidth);
                float2 uvLeftDown = float2(uv.x - EdgeWidth, uv.y - EdgeWidth);

                float depthRU = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvRightUp).r;
                float depthLU = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvLeftUp).r;
                float depthRD = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvRightDown).r;
                float depthLD = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvLeftDown).r;
                float4 depthEdge = (sqrt(pow(depthRU - depthLD, 2) + pow(depthLU - depthRD, 2)) * 100).xxxx;

                return _Edge == 1 ? depthEdge : SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uvLeftDown);
            }
            ENDHLSL
        }
    }
        FallBack "Diffuse"
}
