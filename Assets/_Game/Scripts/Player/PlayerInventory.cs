using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    // Singleton for easy access
    public static PlayerInventory Instance { get; private set; }

    [Header("Current Equipment")]
    public bool hasSuit = false;
    public bool hasContainer = false;
    public bool hasMeter = false;

    [Header("Events")]
    public UnityEvent OnInventoryUpdated; // UI listens to this!
    public UnityEvent OnInventoryFilled;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CollectItem(string itemType)
    {
        switch (itemType)
        {
            case "Suit":
                hasSuit = true;
                Debug.Log("🥼 Hazmat Suit Equipped!");
                break;
            case "Container":
                hasContainer = true;
                Debug.Log("📦 Lead Container Acquired!");
                break;
            case "Meter":
                hasMeter = true;
                Debug.Log("📟 Survey Meter Calibrated!");
                break;
            default:
                Debug.LogWarning($"Unknown item type: {itemType}");
                break;
        }

        // Tell the UI to refresh
        OnInventoryUpdated?.Invoke();

        // Check if we have everything to unlock the final room
        CheckLoadout();
    }

    private void CheckLoadout()
    {
        if (!hasSuit && hasContainer && hasMeter)
        {
            Debug.Log("Power Room Access Granted.");
            GameManager.Instance.CompletePhase(GamePhase.PowerRoom); // Unlocks the final door
            OnInventoryFilled?.Invoke();
        }
        else if (hasSuit && hasContainer && hasMeter)
        {
            Debug.Log("✅ ALL GEAR COLLECTED. Power Room Access Granted.");
            GameManager.Instance.CompletePhase(GamePhase.PowerRoom); // Unlocks the final door
            OnInventoryFilled?.Invoke();
        }
        else
            Debug.Log("⚠️ You Must Collect All Gear.");
    }
}