using UnityEngine;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    [Header("结局配置")]
    [SerializeField] private EndingDataSO[] m_AllEndings;

    [Header("UI显示")]
    [SerializeField] private GameObject m_EndingPanel;
    [SerializeField] private TMPro.TextMeshProUGUI m_EndingTitle;
    [SerializeField] private TMPro.TextMeshProUGUI m_EndingDescription;
    [SerializeField] private UnityEngine.UI.Image m_EndingImage;

    [Header("事件")]
    [SerializeField] private StringEventChannelSO m_OnEndingSelected;

    [Header("引用")]
    [SerializeField] private GameStateManager m_StateManager;

    public void ShowEnding(string endingID)
    {
        // 查找匹配的结局数据
        EndingDataSO selectedEnding = null;
        foreach (var ending in m_AllEndings)
        {
            if (ending.endingID == endingID)
            {
                selectedEnding = ending;
                break;
            }
        }

        if (selectedEnding == null)
        {
            Debug.LogError($"未找到结局: {endingID}");
            return;
        }

        // 显示结局UI
        if (m_EndingPanel != null)
        {
            m_EndingPanel.SetActive(true);
        }

        if (m_EndingTitle != null)
        {
            m_EndingTitle.text = selectedEnding.endingTitle;
        }

        if (m_EndingDescription != null)
        {
            m_EndingDescription.text = selectedEnding.endingDescription;
        }

        if (m_EndingImage != null && selectedEnding.endingImage != null)
        {
            m_EndingImage.sprite = selectedEnding.endingImage;
        }

        // 播放音频
        if (selectedEnding.endingAudio != null)
        {
            AudioSource.PlayClipAtPoint(selectedEnding.endingAudio, Camera.main.transform.position);
        }

        // 触发事件
        m_OnEndingSelected?.RaiseEvent(endingID);

        Debug.Log($"结局已显示: {selectedEnding.endingTitle}");
    }

    // 基于玩家状态决定结局
    public void DetermineEndingFromState()
    {
        if (m_StateManager == null)
        {
            ShowEnding("ending_keep");
            return;
        }

        var prioritized = m_StateManager.GetPrioritizedMemory();
        if (prioritized == null)
        {
            ShowEnding("ending_keep");
            return;
        }

        // 根据优先记忆选择结局
        string endingID = prioritized.memoryID switch
        {
            "memory_guitar" => "ending_keep",
            "memory_photo" => "ending_delete",
            "memory_journal" => "ending_rewrite",
            _ => "ending_keep"
        };

        ShowEnding(endingID);
    }
}