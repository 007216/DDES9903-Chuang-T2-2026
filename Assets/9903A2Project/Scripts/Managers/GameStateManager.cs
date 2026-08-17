using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    [Header("状态")]
    [SerializeField] private IntVariableSO m_MemoriesFoundCount;
    [SerializeField] private List<MemoryDataSO> m_FoundMemories = new List<MemoryDataSO>();

    [Header("事件")]
    [SerializeField] private VoidEventChannelSO m_OnMemoryFound;
    [SerializeField] private VoidEventChannelSO m_OnAllMemoriesFound;

    [Header("配置")]
    [SerializeField] private int m_TotalMemories = 3;

    public int TotalMemories => m_TotalMemories;
    public int FoundCount => m_MemoriesFoundCount != null ? m_MemoriesFoundCount.Value : m_FoundMemories.Count;
    public List<MemoryDataSO> FoundMemories => m_FoundMemories;

    public void RegisterMemoryFound(MemoryDataSO memoryData)
    {
        if (memoryData == null) return;
        if (m_FoundMemories.Contains(memoryData)) return;

        m_FoundMemories.Add(memoryData);

        if (m_MemoriesFoundCount != null)
        {
            m_MemoriesFoundCount.Value = m_FoundMemories.Count;
        }

        m_OnMemoryFound?.RaiseEvent();

        Debug.Log($"记忆找到: {memoryData.memoryTitle} ({m_FoundMemories.Count}/{m_TotalMemories})");

        // 检查是否所有记忆都已找到
        if (m_FoundMemories.Count >= m_TotalMemories)
        {
            m_OnAllMemoriesFound?.RaiseEvent();
            Debug.Log("所有记忆已找到！触发高潮...");
        }
    }

    // 获取玩家探索的"情感优先级"
    // 最后找到的记忆 = 玩家最优先处理的记忆
    public MemoryDataSO GetPrioritizedMemory()
    {
        if (m_FoundMemories.Count == 0) return null;
        return m_FoundMemories[m_FoundMemories.Count - 1];
    }
}