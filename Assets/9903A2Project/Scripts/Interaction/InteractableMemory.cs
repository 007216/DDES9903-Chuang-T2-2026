using UnityEngine;

public class InteractableMemory : MonoBehaviour, IInteractable
{
    [Header("关联数据")]
    [SerializeField] private MemoryDataSO m_MemoryData;
    [SerializeField] private VoidEventChannelSO m_OnInteractedEvent;
    [SerializeField] private GameObject m_VisualFeedback;

    [Header("引用")]
    [SerializeField] private GameStateManager m_StateManager;

    private bool m_IsInteracted = false;

    public void Interact()
    {
        if (m_IsInteracted) return;

        m_IsInteracted = true;

        // 1. 触发事件
        m_OnInteractedEvent?.RaiseEvent();

        // 2. 更新状态
        if (m_StateManager != null)
        {
            m_StateManager.RegisterMemoryFound(m_MemoryData);
        }

        // 3. 视觉反馈
        if (m_VisualFeedback != null)
        {
            m_VisualFeedback.SetActive(true);
        }

        // 4. 播放音频（如果有）
        if (m_MemoryData != null && m_MemoryData.memoryVoice != null)
        {
            AudioSource.PlayClipAtPoint(m_MemoryData.memoryVoice, transform.position);
        }

        // 5. 禁用交互（或销毁物体）
        // 可选：禁用Collider让物体不再可交互
        // GetComponent<Collider>().enabled = false;
        // 或：Destroy(gameObject, 0.5f);

        Debug.Log($"记忆已触发: {m_MemoryData?.memoryTitle}");
    }

    // 在编辑器中可视化交互范围
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
