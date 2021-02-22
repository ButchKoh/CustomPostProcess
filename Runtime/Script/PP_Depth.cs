using System;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_DepthRenderer), PostProcessEvent.AfterStack, "Custom/Depth")]
public sealed class PP_Depth : PostProcessEffectSettings
{
    public BoolParameter _Edge = new BoolParameter { value = false };
}

public sealed class PP_DepthRenderer : PostProcessEffectRenderer<PP_Depth>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/Depth"));
        sheet.properties.SetInt("_Edge", settings._Edge == true ? 1 : 0);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}