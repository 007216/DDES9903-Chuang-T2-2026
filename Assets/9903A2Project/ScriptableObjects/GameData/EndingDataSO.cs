using UnityEngine;

[CreateAssetMenu(menuName = "GameData/EndingData")]
public class EndingDataSO : ScriptableObject
{
    public string endingID;
    public string endingTitle;
    [TextArea(5, 10)] public string endingDescription;
    public AudioClip endingAudio;
    public Sprite endingImage;
}
