using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_NoiseRenderer), PostProcessEvent.AfterStack, "Custom/Noise")]
public sealed class PP_Noise : PostProcessEffectSettings
{
    //[Tooltip("Number of pixels between samples that are tested for an edge. When this value is 0, tested samples are adjacent.")]

    [Range(0,10)]
    public FloatParameter _TimeScale = new FloatParameter { value = 2.5f };
    public BoolParameter _UVHorizontalSlipOn = new BoolParameter { value = true };
    public FloatParameter _SlippingPosOffset = new FloatParameter { value = 0 };
    public IntParameter _SlippingFrequency = new IntParameter { value = 30 };
    [Range(0, 2)]
    public FloatParameter _SlippingLevel = new FloatParameter { value = 0.05f };
    public FloatParameter _SlippingWidth = new FloatParameter { value = 0.25f };
    public FloatParameter _NoiseParam1 = new FloatParameter { value = 500 };
    public FloatParameter _NoiseIntensity = new FloatParameter { value = 1 };

    [Range(-5, 5)]
    public FloatParameter _VerticalSlipping = new FloatParameter { value = 0 };

    public BoolParameter _UVNoiseOn = new BoolParameter { value = true };
    public FloatParameter _NoiseFrequency = new FloatParameter { value = 100 };
    public FloatParameter _NoiseDetail = new FloatParameter { value = 2 };
    public FloatParameter _NoiseSpeed = new FloatParameter { value = 10 };
    public FloatParameter _NoiseThreshold = new FloatParameter { value = 0.3f };
    [Range(0, 1.5f)]
    public FloatParameter _NoiseWidth = new FloatParameter { value = 0.075f };

    public BoolParameter _StretchOn = new BoolParameter { value = true };
    public FloatParameter _StretchIntensity = new FloatParameter { value = 0.5f };
    public IntParameter _StretchLevel = new IntParameter { value = 8 };
    [Range(0, 1)]
    public FloatParameter _StretchThreshold = new FloatParameter { value = 0.2f };
    public IntParameter _NoiseParam4 = new IntParameter { value = 600 };

    public BoolParameter _SeparationOn = new BoolParameter { value = true };
    [Range(0, 1)]
    public FloatParameter _RGBSeparationWidth = new FloatParameter { value = 0.015f };
    [Range(0, 1)]
    public FloatParameter _RGBSeparationThreshold = new FloatParameter { value = 0.4f };
    public IntParameter _NoiseParam5 = new IntParameter { value = 30 };

    public BoolParameter _MosicOn = new BoolParameter { value = true };
    [Range(0,1000)]
    public FloatParameter _MosicLevelX = new FloatParameter { value = 128 };
    [Range(0,1000)]
    public FloatParameter _MosicLevelY = new FloatParameter { value = 128 };
    [Range(0,1)]
    public FloatParameter _MosicThreshold = new FloatParameter { value = 0.15f };

    public BoolParameter _WaveOn = new BoolParameter { value = true };
    public FloatParameter _WaveSpeed = new FloatParameter { value = 1 };
    public FloatParameter _LineWidth = new FloatParameter { value = 1 };
    //[Range(0,1)]
    public FloatParameter _LineIntensity = new FloatParameter { value = 0.25f };

    public BoolParameter _SimpleNoiseOn = new BoolParameter { value = false };
    [Range(0,2.5f)]
    public FloatParameter _SimpleNoiseLevel = new FloatParameter { value = 0.5f };
    [Range(250,50000)]
    public FloatParameter _SimpleNoiseScale = new FloatParameter { value = 250 };
    public IntParameter _NoiseParam7 = new IntParameter { value = 500 };

    public BoolParameter _PixelizeOn = new BoolParameter { value = false };
    public FloatParameter _PixelNum = new FloatParameter { value = 128 };
    public FloatParameter _PixelSize = new FloatParameter { value = 0.85f };
    public FloatParameter _PixelWidth = new FloatParameter { value = 0.85f };


}

public sealed class PP_NoiseRenderer : PostProcessEffectRenderer<PP_Noise>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/NoiseShader"));
        sheet.properties.SetFloat("_TimeScale", Mathf.Pow(settings._TimeScale / 10, 3) * 10);

        sheet.properties.SetInt("_UVHorizontalSlipOn", settings._UVHorizontalSlipOn == true ? 1 : 0);
        sheet.properties.SetFloat("_SlippingPosOffset", settings._SlippingPosOffset);
        sheet.properties.SetInt("_SlippingFrequency", settings._SlippingFrequency);
        sheet.properties.SetFloat("_SlippingLevel", settings._SlippingLevel);
        sheet.properties.SetFloat("_SlippingWidth", settings._SlippingWidth);
        sheet.properties.SetFloat("_NoiseParam1", settings._NoiseParam1);
        sheet.properties.SetFloat("_NoiseIntensity", settings._NoiseIntensity);

        sheet.properties.SetFloat("_VerticalSlipping", settings._VerticalSlipping);

        sheet.properties.SetInt("_UVNoiseOn", settings._UVNoiseOn == true ? 1 : 0);
        sheet.properties.SetFloat("_NoiseFrequency", settings._NoiseFrequency);
        sheet.properties.SetFloat("_NoiseDetail", settings._NoiseDetail);
        sheet.properties.SetFloat("_NoiseSpeed", settings._NoiseSpeed);
        sheet.properties.SetFloat("_NoiseThreshold", settings._NoiseThreshold);
        sheet.properties.SetFloat("_NoiseWidth", settings._NoiseWidth);

        sheet.properties.SetInt("_StretchOn", settings._StretchOn == true ? 1 : 0);
        sheet.properties.SetFloat("_StretchIntensity", settings._StretchIntensity);
        sheet.properties.SetInt("_StretchLevel", settings._StretchLevel);
        sheet.properties.SetFloat("_StretchThreshold", settings._StretchThreshold);
        sheet.properties.SetInt("_NoiseParam4", settings._NoiseParam4);

        sheet.properties.SetInt("_SeparationOn", settings._SeparationOn == true ? 1 : 0);
        sheet.properties.SetFloat("_RGBSeparationWidth", settings._RGBSeparationWidth);
        sheet.properties.SetFloat("_RGBSeparationThreshold", settings._RGBSeparationThreshold);
        sheet.properties.SetInt("_NoiseParam5", settings._NoiseParam5);

        sheet.properties.SetInt("_MosicOn", settings._MosicOn == true ? 1 : 0);
        sheet.properties.SetFloat("_MosicLevelX", settings._MosicLevelX);
        sheet.properties.SetFloat("_MosicLevelY", settings._MosicLevelY);
        sheet.properties.SetFloat("_MosicThreshold", settings._MosicThreshold);

        sheet.properties.SetInt("_WaveOn", settings._WaveOn == true ? 1 : 0);
        sheet.properties.SetFloat("_WaveSpeed", settings._WaveSpeed);
        sheet.properties.SetFloat("_LineWidth", settings._LineWidth);
        sheet.properties.SetFloat("_LineIntensity", settings._LineIntensity);

        sheet.properties.SetInt("_SimpleNoiseOn", settings._SimpleNoiseOn == true ? 1 : 0);
        sheet.properties.SetFloat("_SimpleNoiseLevel", settings._SimpleNoiseLevel);
        sheet.properties.SetFloat("_SimpleNoiseScale", settings._SimpleNoiseScale);
        sheet.properties.SetInt("_NoiseParam7", settings._NoiseParam7);

        sheet.properties.SetInt("_PixelizeOn", settings._PixelizeOn == true ? 1 : 0);
        sheet.properties.SetFloat("_PixelNum", settings._PixelNum);
        sheet.properties.SetFloat("_PixelSize", settings._PixelSize);
        sheet.properties.SetFloat("_PixelWidth", settings._PixelWidth);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}