using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Ωªª•…Ë÷√")]
    [SerializeField] private float m_InteractRange = 3f;
    [SerializeField] private LayerMask m_InteractLayers;
    [SerializeField] private GameObject m_PromptUI;

    private Camera m_Camera;
    private IInteractable m_CurrentTarget;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
        if (m_Camera == null)
            m_Camera = Camera.main;
    }

    void Update()
    {
        // …‰œﬂºÏ≤‚
        Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_InteractRange, m_InteractLayers))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                m_CurrentTarget = interactable;
                m_PromptUI?.SetActive(true);
                return;
            }
        }

        m_CurrentTarget = null;
        m_PromptUI?.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (m_CurrentTarget == null) return;

        m_CurrentTarget.Interact();
    }
}
