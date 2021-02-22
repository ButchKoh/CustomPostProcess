Shader "Custom/PostEffect/NoiseShader" {
    
    SubShader{
            
        Cull Off ZWrite Off ZTest Always

        Pass{

            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment frag
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

            TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

            half _TimeScale;

            float _UVAnimX;
            float _UVAnimY;
            float _UVAnimSpeed;

            int _UVHorizontalSlipOn;
            half _SlippingPosOffset;
            int _SlippingFrequency;
            int _NoiseParam1;
            half _SlippingLevel;
            half _SlippingWidth;
            half _VerticalSlipping;
            half _NoiseIntensity;

            int _UVNoiseOn;
            half _NoiseFrequency;
            half _NoiseDetail;
            half _NoiseSpeed;
            half _NoiseThreshold;
            half _NoiseWidth;

            int _StretchOn;
            half _StretchIntensity;
            int _StretchLevel;
            half _StretchThreshold;
            int _NoiseParam4;

            int _SeparationOn;
            half _RGBSeparationWidth;
            half _RGBSeparationThreshold;
            int _NoiseParam5;

            int _MosicOn;
            half _MosicLevelX;
            half _MosicLevelY;
            half _MosicThreshold;

            int _WaveOn;
            half _WaveSpeed;
            half _LineWidth;
            half _LineIntensity;

            int _SimpleNoiseOn;
            half _SimpleNoiseLevel;
            half _SimpleNoiseScale;
            int _NoiseParam7;

            int _PixelizeOn;
            half _PixelNum;
            half _PixelSize;
            half _PixelWidth;

            half4 _Color;
            //ノイズ生成、参考:https://docs.unity3d.com/Packages/com.unity.shadergraph@7.1/manual/Simple-Noise-Node.html
            inline half noise_randomValue(half2 uv)
        {
            return frac(sin(dot(uv, half2(12.9828, 78.2333))) * 43758.4535);
        }
            inline half noise_interpolate(half a, half b, half t)
        {
            return (1.0 - t) * a + (t * b);
        }
            inline half valueNoise(half2 uv)
        {
            half2 i = floor(uv);
            half2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);

            uv = abs(frac(uv) - 0.5);
            half2 c0 = i + half2(0.0, 0.0);
            half2 c1 = i + half2(1.0, 0.0);
            half2 c2 = i + half2(0.0, 1.0);
            half2 c3 = i + half2(1.0, 1.0);
            half r0 = noise_randomValue(c0);
            half r1 = noise_randomValue(c1);
            half r2 = noise_randomValue(c2);
            half r3 = noise_randomValue(c3);

            half bottomOfGrid = noise_interpolate(r0, r1, f.x);
            half topOfGrid = noise_interpolate(r2, r3, f.x);
            half t = noise_interpolate(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
            inline half SimpleNoise(half2 UV, half Scale) {
            half noise; noise = 0.0;
            half noise_tmp1 = pow(2.0, half(0));
            half noise_tmp2 = pow(0.5, half(3 - 0));
            noise += valueNoise(half2(UV.x * Scale / noise_tmp1, UV.y * Scale / noise_tmp1)) * noise_tmp2;
            noise_tmp1 = pow(2.0, half(1)); noise_tmp2 = pow(0.5, half(3 - 1));
            noise += valueNoise(half2(UV.x * Scale / noise_tmp1, UV.y * Scale / noise_tmp1)) * noise_tmp2;
            noise_tmp1 = pow(2.0, half(2)); noise_tmp2 = pow(0.5, half(3 - 2));
            noise += valueNoise(half2(UV.x * Scale / noise_tmp1, UV.y * Scale / noise_tmp1)) * noise_tmp2;
            return noise;
        }
            //色空間周り
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
            //長方形の描画単位
            half Rectangle(half2 UV, half Width, half Height)
        {
            half2 d = abs(UV * 2 - 1) - half2(Width, Height);
            d = 1 - d / fwidth(d);
            return saturate(min(d.x, d.y));
        }

            half ScaledTime;

            float4 frag(VaryingsDefault i) : SV_TARGET{
                ScaledTime += _Time.y * _TimeScale;
                float2 uv = frac(i.texcoord.xy + float2(_UVAnimX, _UVAnimY) * _UVAnimSpeed * ScaledTime);

                half ScaledTimeFraction = frac(ScaledTime);
                half TimeFraction = _Time.y;

                //1----------------------------------------------------------------------------------

                half modulo = fmod(ScaledTime, 10.0);
                half posterized1 = floor(modulo / (1.0 / _SlippingFrequency)) * (1.0 / _SlippingFrequency);
                half2 TimeFractionPair;
                TimeFractionPair.x = ScaledTimeFraction;
                TimeFractionPair.y = ScaledTimeFraction;
                half2 PosterizedPair;
                PosterizedPair.x = posterized1;
                PosterizedPair.y = posterized1;
                half noise1 = SimpleNoise(PosterizedPair, _NoiseParam1);
                noise1 = noise1 == 0 ? 1 : noise1;
                half saturated = saturate(step(uv.y, noise1 * _NoiseIntensity + _SlippingWidth + _SlippingPosOffset) - step(uv.y, noise1 * _NoiseIntensity + _SlippingPosOffset));
                half2 VerticalSlipped;
                VerticalSlipped.x = saturated * _SlippingLevel * _UVHorizontalSlipOn;
                VerticalSlipped.y = _VerticalSlipping;

                //2-------------------------------------------------------------------------

                half noise2 = SimpleNoise(TimeFractionPair, _NoiseFrequency);
                half noise3 = (SimpleNoise(float2(uv.y, uv.y * noise2) * _NoiseDetail, 150) * 2) - 1;
                half stepped3 = step(noise2, _NoiseThreshold);
                half2 NoiseUV = half2(noise3 * stepped3 * _NoiseWidth * _UVNoiseOn, 0);

                //3-------------------------------------------------------------------------

                half noise4 = SimpleNoise(PosterizedPair, _NoiseParam4);
                half posterizedNoise = floor(noise4 * _StretchLevel) / _StretchLevel;
                half2 UVStretch = _StretchIntensity * posterizedNoise;
                UVStretch *= step(posterizedNoise, _StretchThreshold);
                UVStretch.x = (1.0 - abs(sign(_StretchOn - 1))) * uv.x * UVStretch.x;
                UVStretch.y = 0;

                //UV関係-------------------------------------------------------------

                half2 UVsum1 = (uv + VerticalSlipped + NoiseUV + UVStretch);
                UVsum1.y=frac(UVsum1.y);

                //4-------------------------------------------------------------------------

                half noise5 = SimpleNoise(noise4, _NoiseParam5);
                half RGBSeparaton = step(noise5, _RGBSeparationThreshold) * _RGBSeparationWidth * _SeparationOn;
                half2 uvR = UVsum1;
                half2 uvG = UVsum1;
                half2 uvB = UVsum1;
                uvR.x -= RGBSeparaton;
                uvB.x += RGBSeparaton;

                float4 r = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvR);
                float4 g = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvG);
                float4 b = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvB);
                float4 color = float4(r.r, g.g, b.b, 1);

                //5------------------------------------------------------------------------

                half noise6 = SimpleNoise(noise1 * 10, noise4);
                half2 UVMosicLevel_tmp = half2(_MosicLevelX * noise1, _MosicLevelY * noise1);
                half2 UVMosicLevel = round(UVMosicLevel_tmp);
                half2 MosicUV = (floor(uv * UVMosicLevel.xy) / UVMosicLevel.xy);
                int MosicThres = step(noise6, _MosicThreshold) * _MosicOn;

                //6----------------------------------------------------------------------------------

                half noise7 = SimpleNoise(uv, _SimpleNoiseScale);
                half addSimpleNoise = frac(noise7 * 30 + ScaledTime * 3 * _SimpleNoiseOn) * _SimpleNoiseLevel;
                float4 AddSimpleNoise = float4(addSimpleNoise, addSimpleNoise, addSimpleNoise, 1) / 2;

                //7-------------------------------------------------------------------------

                
                half LineSequence = frac(_WaveSpeed * ScaledTime + uv.y * _LineWidth) * _LineIntensity * _WaveOn;
                LineSequence *= frac(LineSequence * LineSequence) * addSimpleNoise;

                //8----------------------------------------------------------------------------------

                half2 PixelizedUV = (round(uv.xy * _PixelNum.xx)  / _PixelNum.xx );
                PixelizedUV.y = frac(PixelizedUV.y);
                half noise8 = SimpleNoise(ScaledTime, _NoiseParam7) * _SimpleNoiseOn;
                float4 pxelizedColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, PixelizedUV);


                float3 albedoColorRGB = (lerp(color.rgb, AddSimpleNoise.rgb, noise8 * _SimpleNoiseLevel)
                                    - LineSequence) * (1 - MosicThres)
                                    + SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, MosicUV).rgb * MosicThres;
                float4 albedoColor = float4(albedoColorRGB, 1);

                float4 PixelR = Rectangle(frac((uv - float2(0.333 / _PixelNum, 0)) * _PixelNum), _PixelSize * _PixelWidth / 3, _PixelSize)
                    * float4(albedoColor.r, 0, 0, 1);
                float4 PixelG = Rectangle(frac(uv * _PixelNum), _PixelSize * _PixelWidth / 3, _PixelSize)
                    * float4(0, albedoColor.g, 0, 1);
                float4 PixelB = Rectangle(frac((uv + float2(0.333 / _PixelNum, 0)) * _PixelNum), _PixelSize * _PixelWidth / 3, _PixelSize)
                    * float4(0, 0, albedoColor.b, 1);

                float4 RGBColor = (PixelR + PixelG + PixelB) * _PixelizeOn + (1 - _PixelizeOn) * albedoColor;
                return RGBColor + _PixelizeOn * RGBColor * 0.5;
                //return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, MosicUV);
            }
            ENDHLSL
        }
    }
            FallBack Off
}