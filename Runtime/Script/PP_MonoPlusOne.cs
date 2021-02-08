using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_MonoPlusOneRenderer), PostProcessEvent.AfterStack, "Custom/MonoPlusOne")]
public sealed class PP_MonoPlusOne : PostProcessEffectSettings
{

    public ColorParameter col1 = new ColorParameter { value = Color.white };
    [Range(0, 1)]
    public FloatParameter tolerance1 = new FloatParameter { value = 0.3f };

}

public sealed class PP_MonoPlusOneRenderer : PostProcessEffectRenderer<PP_MonoPlusOne>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/MonoPlusOne"));
        sheet.properties.SetColor("col1", settings.col1);
        sheet.properties.SetFloat("tolerance1", settings.tolerance1);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}