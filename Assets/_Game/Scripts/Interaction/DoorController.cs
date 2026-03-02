using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [Header("Configuration")]
    [SerializeField] private GamePhase requiredPhase = GamePhase.Lab; // When does this unlock?
    [SerializeField] private string lockedMessage = "Access Denied: Complete Safety Protocols.";
    [SerializeField] private GameObject doorVisuals; // The sprite/object to disable when opened
    [SerializeField] private Collider2D doorCollider; // The physical collider to disable

    private bool _isOpen = false;

    public void Interact()
    {
        if (_isOpen) return;

        // 1. Check the Game Manager for the current phase
        if (GameManager.Instance.currentPhase >= requiredPhase)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log(lockedMessage);
            // Later, we will show this message on the UI
        }
    }

    private void OpenDoor()
    {
        _isOpen = true;
        Debug.Log("Door Unlocked!");
        
        // Disable the door visuals and physics
        if (doorVisuals) doorVisuals.SetActive(false);
        if (doorCollider) doorCollider.enabled = false;
    }

    public string GetDescription()
    {
        return _isOpen ? "" : "Open Door";
    }
}