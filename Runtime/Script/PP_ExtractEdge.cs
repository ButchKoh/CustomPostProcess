using System;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_ExtractEdgeRenderer), PostProcessEvent.AfterStack, "Custom/Edge Extraction")]
public sealed class PP_ExtractEdge : PostProcessEffectSettings
{
    public BoolParameter DrawBGColor = new BoolParameter { value = false };
    public FloatParameter _EdgeSize = new FloatParameter { value = 1 };
    [Range(0, 1)]
    public FloatParameter _Threshold = new FloatParameter { value = 0.35f };
    public BoolParameter Monocolor = new BoolParameter { value = false };
    public BoolParameter mode = new BoolParameter { value = true };
    public BoolParameter repeat = new BoolParameter { value = true };
    public ColorParameter MainColor = new ColorParameter { value = Color.white };

}

public sealed class PP_ExtractEdgeRenderer : PostProcessEffectRenderer<PP_ExtractEdge>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/ExtractEdge"));
        sheet.properties.SetInt("DrawBG", settings.DrawBGColor == true ? 1 : 0);
        sheet.properties.SetFloat("_EdgeSize", settings._EdgeSize);
        sheet.properties.SetFloat("_Threshold", Mathf.Pow(settings._Threshold, 3));
        sheet.properties.SetInt("Monocolor", settings.Monocolor == true ? 1 : 0);
        sheet.properties.SetInt("mode", settings.mode == true ? 1 : 0);
        sheet.properties.SetInt("repeat", settings.repeat == true ? 1 : 0);
        sheet.properties.SetColor("MainColor", settings.MainColor);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}