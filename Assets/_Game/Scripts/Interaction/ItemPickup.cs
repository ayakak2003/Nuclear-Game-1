using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Item Configuration")]
    [SerializeField] private string itemType = "Suit"; // Must match "Suit", "Container", or "Meter"
    [SerializeField] private string description = "Pick Up";
    
    [Header("Visuals")]
    [SerializeField] private GameObject promptObject; // The "Press E" UI

    public void Interact()
    {
        // 1. Add to Inventory
        PlayerInventory.Instance.CollectItem(itemType);

        // 2. Play Sound (Optional - add later)
        
        // 3. Destroy this object (Poof!)
        Destroy(gameObject);
    }

    public string GetDescription()
    {
        return description;
    }

    // Reuse the Trigger logic for the UI Prompt
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && promptObject != null) promptObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && promptObject != null) promptObject.SetActive(false);
    }

    private void Start()
    {
        if (promptObject != null) promptObject.SetActive(false);
    }
}