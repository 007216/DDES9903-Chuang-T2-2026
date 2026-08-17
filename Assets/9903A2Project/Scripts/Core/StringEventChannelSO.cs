using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/StringEventChannel")]
public class StringEventChannelSO : ScriptableObject
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        OnEventRaised?.Invoke(value);
    }
}
