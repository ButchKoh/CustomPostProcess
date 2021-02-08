using System;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_ReplaceWithTextureRenderer), PostProcessEvent.AfterStack, "Custom/Replace With Texture")]
public sealed class PP_ReplaceWithTexture : PostProcessEffectSettings
{
    public ColorParameter col1 = new ColorParameter { value = Color.white };
    [Range(0, 1)]
    public FloatParameter tolerance1 = new FloatParameter { value = 0.3f };
    public ColorParameter col2 = new ColorParameter { value = Color.white };
    [Range(0, 1)]
    public FloatParameter tolerance2 = new FloatParameter { value = 0.3f }; 
    public ColorParameter col3 = new ColorParameter { value = Color.white };
    [Range(0, 1)]
    public FloatParameter tolerance3 = new FloatParameter { value = 0.3f };
    public FloatParameter scaleX = new FloatParameter { value = 1 };
    public FloatParameter scaleY = new FloatParameter { value = 1 };
    public TextureParameter _Tex1 = new TextureParameter { value = null };
    
    public override bool IsEnabledAndSupported(PostProcessRenderContext context)
    {
        //TextureParameterがnullだとエラーを出し続けるので
        bool state = enabled.value && _Tex1.value != null;
        return state;
    }
}

public sealed class PP_ReplaceWithTextureRenderer : PostProcessEffectRenderer<PP_ReplaceWithTexture>
{
    public bool Flag = false;

    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffect/ReplaceWithTexture"));
        sheet.properties.SetColor("col1", settings.col1);
        sheet.properties.SetFloat("tolerance1", Mathf.Pow(settings.tolerance1, 3) * 3);
        sheet.properties.SetColor("col2", settings.col2);
        sheet.properties.SetFloat("tolerance2", Mathf.Pow(settings.tolerance2, 3) * 3);
        sheet.properties.SetColor("col3", settings.col3);
        sheet.properties.SetFloat("tolerance3", Mathf.Pow(settings.tolerance3, 3) * 3);
        sheet.properties.SetFloat("scaleX", settings.scaleX);
        sheet.properties.SetFloat("scaleY", settings.scaleY);
        sheet.properties.SetTexture("_Tex1", settings._Tex1);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}