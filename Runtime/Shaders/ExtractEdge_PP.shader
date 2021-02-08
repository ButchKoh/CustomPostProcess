Shader "Custom/PostEffect/ExtractEdge"
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
                int mode;
                int repeat;
                float _Threshold;
                int Monocolor;
                float4 MainColor;
                int DrawBG;
                TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
                float4 _MainTex_TexelSize;

                float4 frag(VaryingsDefault i) : SV_TARGET
                {
                    float EdgeWidth = _EdgeSize * _MainTex_TexelSize.x;
                    float2 uv = i.texcoord.xy;

                    float2 uvRightUp   = float2(uv.x + EdgeWidth, uv.y + EdgeWidth);
                    float2 uvLeftUp    = float2(uv.x - EdgeWidth, uv.y + EdgeWidth);
                    float2 uvRightDown = float2(uv.x + EdgeWidth, uv.y - EdgeWidth);
                    float2 uvLeftDown  = float2(uv.x - EdgeWidth, uv.y - EdgeWidth);

                    uvRightUp   = repeat == 1 ? saturate(uvRightUp) : uvRightUp;
                    uvLeftUp    = repeat == 1 ? saturate(uvLeftUp) : uvRightUp;
                    uvRightDown = repeat == 1 ? saturate(uvRightDown) : uvRightUp;
                    uvLeftDown  = repeat == 1 ? saturate(uvLeftDown) : uvRightUp;

                    float4 colRightUp   = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvRightUp);
                    float4 colLeftUp    = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvLeftUp);
                    float4 colRightDown = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvRightDown);
                    float4 colLeftDown  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvLeftDown);

                    colRightUp   = uvRightUp.x > 1 | uvRightUp.y > 1 ? 0 : colRightUp;
                    colLeftUp    = uvLeftUp.x < 0 | uvLeftUp.y    > 1 ? 0 : colLeftUp;
                    colRightDown = uvRightDown.x > 1 | uvRightDown.y < 0 ? 0 : colRightDown;
                    colLeftDown  = uvLeftDown.x < 0 | uvLeftDown.y < 0 ? 0 : colLeftDown;
                    float4 c_tmp;
                    float4 colBase = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                    if (mode == 1) {
                        float2 uvRight  = float2(uv.x + EdgeWidth, uv.y);
                        float2 uvLeft   = float2(uv.x - EdgeWidth, uv.y);
                        float2 uvUp     = float2(uv.x, uv.y + EdgeWidth);
                        float2 uvDown   = float2(uv.x, uv.y - EdgeWidth);

                        uvRight = repeat == 1 ? saturate(uvRight): uvRight;
                        uvLeft  = repeat == 1 ? saturate(uvLeft): uvLeft;
                        uvUp    = repeat == 1 ? saturate(uvUp): uvUp;
                        uvDown  = repeat == 1 ? saturate(uvDown): uvDown;

                        float4 colRight = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvRight);
                        float4 colLeft  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvLeft);
                        float4 colUp    = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvUp);
                        float4 colDown  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvDown);

                        colRight = uvRight.x > 1 ? 0 : colRight;
                        colLeft  = uvLeft.x < 0 ? 0 : colLeft;
                        colUp    = uvUp.y > 1 ? 0 : colUp;
                        colDown  = uvDown.y < 0 ? 0 : colDown;

                        c_tmp = colRight + colLeft + colUp + colDown + colRightUp + colLeftUp + colRightDown + colLeftDown - 8 * colBase;
                    }
                    else {
                        c_tmp = sqrt(dot(colRightUp - colLeftDown, colRightUp - colLeftDown) + dot(colLeftUp - colRightDown, colLeftUp - colRightDown));
                    }
                    float4 c = 1 - step(c_tmp, (float4)_Threshold);
                    float4 color = abs(c);

                    color = (color.r+color.g+color.b != 0 & Monocolor == 1 ? MainColor : color);
                    color = DrawBG == 1 ? (color == float4(0, 0, 0, 1) ? colBase : color) : color;
                    return color;

                }
                ENDHLSL
            }
        }
        FallBack "Diffuse"
}
