// VFX/HologramGlitch.cs
using UnityEngine;

public class HologramGlitch : MonoBehaviour
{
    [Header("故障参数")]
    [SerializeField] private float m_GlitchSpeed = 0.5f;
    [SerializeField] private float m_GlitchIntensity = 0.1f;
    [SerializeField] private Material m_HologramMaterial;

    private Vector3 m_OriginalPosition;
    private float m_GlitchTimer = 0f;

    void Start()
    {
        m_OriginalPosition = transform.localPosition;
        if (m_HologramMaterial == null)
        {
            m_HologramMaterial = GetComponent<Renderer>()?.material;
        }
    }

    void Update()
    {
        m_GlitchTimer += Time.deltaTime * m_GlitchSpeed;

        // 位置抖动
        float offsetX = Mathf.Sin(m_GlitchTimer * 13f) * m_GlitchIntensity;
        float offsetY = Mathf.Sin(m_GlitchTimer * 7f + 1f) * m_GlitchIntensity;
        transform.localPosition = m_OriginalPosition + new Vector3(offsetX, offsetY, 0);

        // 材质闪烁
        if (m_HologramMaterial != null)
        {
            float flicker = Mathf.PerlinNoise(m_GlitchTimer * 0.5f, 0) * 0.3f + 0.7f;
            m_HologramMaterial.SetFloat("_GlowIntensity", flicker);
        }
    }
}