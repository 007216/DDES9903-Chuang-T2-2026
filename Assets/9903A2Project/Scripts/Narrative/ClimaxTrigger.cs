using UnityEngine;

public class ClimaxTrigger : MonoBehaviour
{
    [Header("事件绑定")]
    [SerializeField] private VoidEventChannelSO m_OnAllMemoriesFound;
    [SerializeField] private VoidEventChannelSO m_OnHologramActivated;

    [Header("全息投影")]
    [SerializeField] private GameObject m_HologramProjector;
    [SerializeField] private GameObject m_HologramContent;
    [SerializeField] private AudioClip m_HologramVoice;

    [Header("引用")]
    [SerializeField] private GameStateManager m_StateManager;

    private bool m_IsActivated = false;

    void OnEnable()
    {
        if (m_OnAllMemoriesFound != null)
        {
            m_OnAllMemoriesFound.OnEventRaised += ActivateClimax;
        }
    }

    void OnDisable()
    {
        if (m_OnAllMemoriesFound != null)
        {
            m_OnAllMemoriesFound.OnEventRaised -= ActivateClimax;
        }
    }

    public void ActivateClimax()
    {
        if (m_IsActivated) return;
        m_IsActivated = true;

        Debug.Log("高潮激活！全息投影启动...");

        // 1. 显示全息投影
        if (m_HologramContent != null)
        {
            m_HologramContent.SetActive(true);
        }

        // 2. 播放语音
        if (m_HologramVoice != null)
        {
            AudioSource.PlayClipAtPoint(m_HologramVoice, Camera.main.transform.position);
        }

        // 3. 触发事件
        m_OnHologramActivated?.RaiseEvent();

        // 4. 根据优先级变化结局内容
        if (m_StateManager != null)
        {
            var prioritized = m_StateManager.GetPrioritizedMemory();
            if (prioritized != null)
            {
                Debug.Log($"基于优先记忆调整结局: {prioritized.memoryTitle}");
            }
        }
    }
}