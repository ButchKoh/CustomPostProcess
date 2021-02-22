using System;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_NormalRenderer), PostProcessEvent.AfterStack, "Custom/Normal")]
public sealed class PP_Normal : PostProcessEffectSettings
{
    public BoolParameter _Edge = new BoolParameter { value = false };
}

public sealed class PP_NormalRenderer : PostProcessEffectRenderer<PP_Normal>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/Normal"));
        sheet.properties.SetInt("_Edge", settings._Edge == true ? 1 : 0);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}