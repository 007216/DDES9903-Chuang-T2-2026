// ScriptableObjects/LightingPresetSO.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Lighting/LightingPreset")]
public class LightingPresetSO : ScriptableObject
{
    [Header("主光源")]
    public Color m_DirectionalColor = Color.white;
    public float m_DirectionalIntensity = 1f;

    [Header("环境光")]
    public Color m_AmbientColor = Color.gray;
    public float m_AmbientIntensity = 0.5f;

    [Header("雾效")]
    public bool m_EnableFog = true;
    public Color m_FogColor = Color.gray;
    public float m_FogDensity = 0.02f;
}
