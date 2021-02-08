using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_PixelAndColorDepthRenderer), PostProcessEvent.AfterStack, "Custom/Pixel and Color Depth")]
public sealed class PP_PixelAndColorDepth : PostProcessEffectSettings
{
    [Range(0,1920)]
    public IntParameter _PixelX = new IntParameter { value = 1920 };
    [Range(0, 1080)]
    public IntParameter _PixelY = new IntParameter { value = 1080 };
    [Range(0, 8)]
    public IntParameter _Rbit = new IntParameter { value = 8 };
    [Range(0, 8)]
    public IntParameter _Gbit = new IntParameter { value = 8 };
    [Range(0, 8)]
    public IntParameter _Bbit = new IntParameter { value = 8 };
}

public sealed class PP_PixelAndColorDepthRenderer : PostProcessEffectRenderer<PP_PixelAndColorDepth>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/PixelAndColorDepth"));
        sheet.properties.SetInt("_PixelX", settings._PixelX);
        sheet.properties.SetInt("_PixelY", settings._PixelY);
        sheet.properties.SetInt("_Rbit", settings._Rbit);
        sheet.properties.SetInt("_Gbit", settings._Gbit);
        sheet.properties.SetInt("_Bbit", settings._Bbit);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}