using UnityEngine;

[CreateAssetMenu(menuName = "GameData/MemoryData")]
public class MemoryDataSO : ScriptableObject
{
    [Header("基本信息")]
    public string memoryID;
    public string memoryTitle;
    [TextArea(3, 5)] public string memoryText;

    [Header("音频")]
    public AudioClip memoryVoice;

    [Header("视觉")]
    public Color memoryLightColor = Color.white;
    public float memoryLightIntensity = 1f;

    [Header("状态")]
    public bool isFound = false;
}

