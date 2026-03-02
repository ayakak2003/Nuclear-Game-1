using UnityEngine;

public class ContainmentUnit : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject secureVisuals; // Green light / Closed lid sprite
    [SerializeField] private GameObject radiationZone;
    [SerializeField] private GameObject door;

    public void Interact()
    {
        // 1. Check if Player is holding the source
        if (PlayerHealth.Instance.isCarryingSource)
        {
            SecureSource();
        }
        else
        {
            Debug.Log("Container is empty. Find the source.");
        }
    }

    private void SecureSource()
    {
        // 1. Remove source from player
        PlayerHealth.Instance.isCarryingSource = false;

        // 2. Show visuals
        if (secureVisuals) secureVisuals.SetActive(true);

        // 3. Trigger Win
        Debug.Log("✅ CONTAINMENT SUCCESSFUL!");

        radiationZone.SetActive(false);
        
        if (door.TryGetComponent<DoorController>(out DoorController doorController))
        {
            doorController.Interact();
        }
        if (door.TryGetComponent<DialogueTrigger>(out DialogueTrigger doorTrigger))
        {
            doorTrigger.SetGlobalBoolTrue(16);
        }

        GameManager.Instance.CompletePhase(GamePhase.Win);
    }

    public string GetDescription()
    {
        return "Place Source Here";
    }
}