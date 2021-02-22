using System;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_ExtractEdge2Renderer), PostProcessEvent.AfterStack, "Custom/Edge Extraction2")]
public sealed class PP_ExtractEdge2 : PostProcessEffectSettings
{
    public FloatParameter _EdgeSize = new FloatParameter { value = 1 };
    [Range(0, 1)]
    public FloatParameter _Threshold = new FloatParameter { value = 0.7f };
    public FloatParameter _Multiply = new FloatParameter { value = 2 };
    [Range(0, 1)]
    public FloatParameter _Param = new FloatParameter { value = 0.5f };
    public BoolParameter _DrawBG = new BoolParameter { value = true };
    public ColorParameter _BaseColor = new ColorParameter { value = new Color(0.1f, 0.1f, 0.1f, 1) };

}

public sealed class PP_ExtractEdge2Renderer : PostProcessEffectRenderer<PP_ExtractEdge2>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/ExtractEdge2"));
        sheet.properties.SetFloat("_EdgeSize", settings._EdgeSize);
        sheet.properties.SetFloat("_Threshold", Mathf.Pow(settings._Threshold, 3));
        sheet.properties.SetFloat("_Multiply", settings._Multiply);
        sheet.properties.SetFloat("_Param", settings._Param);
        sheet.properties.SetInt("_DrawBG", settings._DrawBG == true ? 1 : 0);
        sheet.properties.SetColor("_BaseColor", settings._BaseColor);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}