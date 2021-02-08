using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_HSV_STRenderer), PostProcessEvent.AfterStack, "Custom/HSV_ST")]
public sealed class PP_HSV_ST : PostProcessEffectSettings
{
    
    public FloatParameter _HueScale = new FloatParameter { value = 1 };
    [Range(-1,1)]
    public FloatParameter _HueOffset = new FloatParameter { value = 0.5f };
    public FloatParameter _SaturationScale = new FloatParameter { value = 1 };
    [Range(-1,1)]
    public FloatParameter _SaturationOffset = new FloatParameter { value = 0 };
    public FloatParameter _ValueScale = new FloatParameter { value = 1 };
    [Range(-1,1)]
    public FloatParameter _ValueOffset = new FloatParameter { value = 0 };

}

public sealed class PP_HSV_STRenderer : PostProcessEffectRenderer<PP_HSV_ST>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/HSV_ST"));
        sheet.properties.SetFloat("_HueScale", settings._HueScale);
        sheet.properties.SetFloat("_HueOffset", settings._HueOffset);
        sheet.properties.SetFloat("_SaturationScale", settings._SaturationScale);
        sheet.properties.SetFloat("_SaturationOffset", settings._SaturationOffset);
        sheet.properties.SetFloat("_ValueScale", settings._ValueScale);
        sheet.properties.SetFloat("_ValueOffset", settings._ValueOffset);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}