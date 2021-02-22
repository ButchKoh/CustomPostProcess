using UnityEditor;
using UnityEngine;
using UnityEditor.Rendering.PostProcessing;

[PostProcessEditor(typeof(PP_Noise))]
public class PP_NoiseEditor : PostProcessEffectEditor<PP_Noise>
{
    SerializedParameterOverride _TimeScale;
    SerializedParameterOverride _UVAnimX;
    SerializedParameterOverride _UVAnimY;
    SerializedParameterOverride _UVAnimSpeed;

    SerializedParameterOverride _UVHorizontalSlipOn;
    SerializedParameterOverride _SlippingPosOffset;
    SerializedParameterOverride _SlippingFrequency;
    SerializedParameterOverride _SlippingLevel;
    SerializedParameterOverride _SlippingWidth;
    SerializedParameterOverride _NoiseParam1;
    SerializedParameterOverride _NoiseIntensity;

    SerializedParameterOverride _VerticalSlipping;
    SerializedParameterOverride _UVNoiseOn;
    SerializedParameterOverride _NoiseFrequency;
    SerializedParameterOverride _NoiseDetail;
    SerializedParameterOverride _NoiseSpeed;
    SerializedParameterOverride _NoiseThreshold;
    SerializedParameterOverride _NoiseWidth;

    SerializedParameterOverride _StretchOn;
    SerializedParameterOverride _StretchIntensity;
    SerializedParameterOverride _StretchLevel;
    SerializedParameterOverride _StretchThreshold;
    SerializedParameterOverride _NoiseParam4;

    SerializedParameterOverride _SeparationOn;
    SerializedParameterOverride _RGBSeparationWidth;
    SerializedParameterOverride _RGBSeparationThreshold;
    SerializedParameterOverride _NoiseParam5;

    SerializedParameterOverride _MosicOn;
    SerializedParameterOverride _MosicLevelX;
    SerializedParameterOverride _MosicLevelY;
    SerializedParameterOverride _MosicThreshold;

    SerializedParameterOverride _WaveOn;
    SerializedParameterOverride _WaveSpeed;
    SerializedParameterOverride _LineWidth;
    SerializedParameterOverride _LineIntensity;

    SerializedParameterOverride _SimpleNoiseOn;
    SerializedParameterOverride _SimpleNoiseLevel;
    SerializedParameterOverride _SimpleNoiseScale;
    SerializedParameterOverride _NoiseParam7;

    SerializedParameterOverride _PixelizeOn;
    SerializedParameterOverride _PixelNum;
    SerializedParameterOverride _PixelSize;
    SerializedParameterOverride _PixelWidth;

    //折り畳み用
    private bool _BasicSettingisOpen = true, _UVSliceisOpen, _UVNoiseisOpen, _UVStretchisOpen,
        _RGBSeparationisOpen, _MosicisOpen, _WaveisOpen, _SimpleNoiseisOpen, _PixelisOpen;


    public override void OnEnable()
    {
        _TimeScale = FindParameterOverride(x => x._TimeScale);
        _UVAnimX = FindParameterOverride(x => x._UVAnimX);
        _UVAnimY = FindParameterOverride(x => x._UVAnimY);
        _UVAnimSpeed = FindParameterOverride(x => x._UVAnimSpeed);

        _UVHorizontalSlipOn = FindParameterOverride(x => x._UVHorizontalSlipOn);
        _SlippingPosOffset = FindParameterOverride(x => x._SlippingPosOffset);
        _SlippingFrequency = FindParameterOverride(x => x._SlippingFrequency);
        _SlippingLevel = FindParameterOverride(x => x._SlippingLevel);
        _SlippingWidth = FindParameterOverride(x => x._SlippingWidth);
        _NoiseParam1 = FindParameterOverride(x => x._NoiseParam1);
        _NoiseIntensity = FindParameterOverride(x => x._NoiseIntensity);

        _VerticalSlipping = FindParameterOverride(x => x._VerticalSlipping);

        _UVNoiseOn = FindParameterOverride(x => x._UVNoiseOn);
        _NoiseFrequency = FindParameterOverride(x => x._NoiseFrequency);
        _NoiseDetail = FindParameterOverride(x => x._NoiseDetail);
        _NoiseSpeed = FindParameterOverride(x => x._NoiseSpeed);
        _NoiseThreshold = FindParameterOverride(x => x._NoiseThreshold);
        _NoiseWidth = FindParameterOverride(x => x._NoiseWidth);

        _StretchOn = FindParameterOverride(x => x._StretchOn);
        _StretchIntensity = FindParameterOverride(x => x._StretchIntensity);
        _StretchLevel = FindParameterOverride(x => x._StretchLevel);
        _StretchThreshold = FindParameterOverride(x => x._StretchThreshold);
        _NoiseParam4 = FindParameterOverride(x => x._NoiseParam4);

        _SeparationOn = FindParameterOverride(x => x._SeparationOn);
        _RGBSeparationWidth = FindParameterOverride(x => x._RGBSeparationWidth);
        _RGBSeparationThreshold = FindParameterOverride(x => x._RGBSeparationThreshold);
        _NoiseParam5 = FindParameterOverride(x => x._NoiseParam5);

        _MosicOn = FindParameterOverride(x => x._MosicOn);
        _MosicLevelX = FindParameterOverride(x => x._MosicLevelX);
        _MosicLevelY = FindParameterOverride(x => x._MosicLevelY);
        _MosicThreshold = FindParameterOverride(x => x._MosicThreshold);

        _WaveOn = FindParameterOverride(x => x._WaveOn);
        _WaveSpeed = FindParameterOverride(x => x._WaveSpeed);
        _LineWidth = FindParameterOverride(x => x._LineWidth);
        _LineIntensity = FindParameterOverride(x => x._LineIntensity);

        _SimpleNoiseOn = FindParameterOverride(x => x._SimpleNoiseOn);
        _SimpleNoiseLevel = FindParameterOverride(x => x._SimpleNoiseLevel);
        _SimpleNoiseScale = FindParameterOverride(x => x._SimpleNoiseScale);
        _NoiseParam7 = FindParameterOverride(x => x._NoiseParam7);

        _PixelizeOn = FindParameterOverride(x => x._PixelizeOn);
        _PixelNum = FindParameterOverride(x => x._PixelNum);
        _PixelSize = FindParameterOverride(x => x._PixelSize);
        _PixelWidth = FindParameterOverride(x => x._PixelWidth);
    }

    public override void OnInspectorGUI()
    {
        GUIStyle leftbold = new GUIStyle()
        {
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
        };


        bool BasicSettingisOpen = EditorGUILayout.Foldout(_BasicSettingisOpen, "Basic Setting");
        if (_BasicSettingisOpen != BasicSettingisOpen) _BasicSettingisOpen = BasicSettingisOpen;
        if (BasicSettingisOpen)
        {
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_TimeScale);
            PropertyField(_VerticalSlipping);
            PropertyField(_UVAnimX);
            PropertyField(_UVAnimY);
            PropertyField(_UVAnimSpeed);
        }

        EditorStyles.label.fontStyle = FontStyle.Normal;

        bool UVSliceisOpen = EditorGUILayout.Foldout(_UVSliceisOpen, "UV Slice");
        if (_UVSliceisOpen != UVSliceisOpen) _UVSliceisOpen = UVSliceisOpen;
        if (_UVSliceisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_UVHorizontalSlipOn, new GUIContent("eneble/disable"));
            PropertyField(_SlippingPosOffset, new GUIContent("offset"));
            PropertyField(_SlippingFrequency, new GUIContent("frequency"));
            PropertyField(_SlippingLevel, new GUIContent("intensity"));
            PropertyField(_SlippingWidth, new GUIContent("width"));
            PropertyField(_NoiseParam1, new GUIContent("noise parameter"));
            PropertyField(_NoiseIntensity, new GUIContent("adjust"));

            EditorGUI.indentLevel--;
        }
        
        bool UVNoiseisOpen = EditorGUILayout.Foldout(_UVNoiseisOpen, "UV Noise");
        if (_UVNoiseisOpen != UVNoiseisOpen) _UVNoiseisOpen = UVNoiseisOpen;
        if (_UVNoiseisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_UVNoiseOn, new GUIContent("eneble/disable"));
            PropertyField(_NoiseFrequency, new GUIContent("frequency"));
            PropertyField(_NoiseDetail, new GUIContent("fineness"));
            PropertyField(_NoiseSpeed, new GUIContent("speed"));
            PropertyField(_NoiseThreshold, new GUIContent("threshold"));
            PropertyField(_NoiseWidth, new GUIContent("width"));
            EditorGUI.indentLevel--;
        }
        
        bool UVStretchisOpen = EditorGUILayout.Foldout(_UVStretchisOpen, "UV Stretch");
        if (_UVStretchisOpen != UVStretchisOpen) _UVStretchisOpen = UVStretchisOpen;
        if (_UVStretchisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_StretchOn, new GUIContent("eneble/disable"));
            PropertyField(_StretchIntensity, new GUIContent("intensity"));
            PropertyField(_StretchLevel, new GUIContent("stage"));
            PropertyField(_StretchThreshold, new GUIContent("threshold"));
            PropertyField(_NoiseParam4, new GUIContent("noise parameter"));
            EditorGUI.indentLevel--;
        }
        
        bool RGBSeparationisOpen = EditorGUILayout.Foldout(_RGBSeparationisOpen, "RGB Separation");
        if (_RGBSeparationisOpen != RGBSeparationisOpen) _RGBSeparationisOpen = RGBSeparationisOpen;
        if (_RGBSeparationisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_SeparationOn, new GUIContent("eneble/disable"));
            PropertyField(_RGBSeparationWidth, new GUIContent("width"));
            PropertyField(_RGBSeparationThreshold, new GUIContent("threshold"));
            PropertyField(_NoiseParam5, new GUIContent("noise parameter"));
            EditorGUI.indentLevel--;
        }
        
        bool MosicisOpen = EditorGUILayout.Foldout(_MosicisOpen, "Mosic");
        if (_MosicisOpen != MosicisOpen) _MosicisOpen = MosicisOpen;
        if (_MosicisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_MosicOn, new GUIContent("eneble/disable"));
            PropertyField(_MosicLevelX, new GUIContent("mosic y"));
            PropertyField(_MosicLevelY, new GUIContent("mosic x"));
            PropertyField(_MosicThreshold, new GUIContent("threshold"));
            EditorGUI.indentLevel--;
        }
        
        bool WaveisOpen = EditorGUILayout.Foldout(_WaveisOpen, "Wave");
        if (_WaveisOpen != WaveisOpen) _WaveisOpen = WaveisOpen;
        if (_WaveisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_WaveOn, new GUIContent("eneble/disable"));
            PropertyField(_WaveSpeed, new GUIContent("speed"));
            PropertyField(_LineWidth, new GUIContent("number"));
            PropertyField(_LineIntensity, new GUIContent("intensity"));
            EditorGUI.indentLevel--;
        }
        
        bool SimpleNoiseisOpen = EditorGUILayout.Foldout(_SimpleNoiseisOpen, "Simple Noise");
        if (_SimpleNoiseisOpen != SimpleNoiseisOpen) _SimpleNoiseisOpen = SimpleNoiseisOpen;
        if (_SimpleNoiseisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_SimpleNoiseOn, new GUIContent("eneble/disable"));
            PropertyField(_SimpleNoiseLevel, new GUIContent("intensity"));
            PropertyField(_SimpleNoiseScale, new GUIContent("scale"));
            PropertyField(_NoiseParam7, new GUIContent("noise parameter"));
            EditorGUI.indentLevel--;
        }
        
        bool PixelisOpen = EditorGUILayout.Foldout(_PixelisOpen, "Pixelize");
        if (_PixelisOpen != PixelisOpen) _PixelisOpen = PixelisOpen;
        if (_PixelisOpen)
        {
            EditorGUI.indentLevel++;
            EditorStyles.label.fontStyle = FontStyle.Normal;
            PropertyField(_PixelizeOn, new GUIContent("eneble/disable"));
            PropertyField(_PixelNum, new GUIContent("number"));
            PropertyField(_PixelSize, new GUIContent("pixel size"));
            PropertyField(_PixelWidth, new GUIContent("width adjustment"));
            EditorGUI.indentLevel--;
        }
        

    }
}
