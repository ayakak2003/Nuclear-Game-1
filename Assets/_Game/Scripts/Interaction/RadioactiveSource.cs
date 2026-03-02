using UnityEngine;

public class RadioactiveSource : MonoBehaviour, IInteractable
{
    [Header("Configuration")]
    // [SerializeField] private float radiationMultiplier = 5f; // Carrying it hurts MORE!
    [SerializeField] private GameObject visualModel; // The blue glowing sphere

    private bool _isCarrying = false;

    public void Interact()
    {
        if (_isCarrying) return;

        // Check if we have the Remote Tool (Optional, or just assume we use hands/tongs)
        // For now, let's just pick it up.
        PickUp();
    }

    private void PickUp()
    {
        _isCarrying = true;
        Debug.Log("⚠️ WARNING: CARRYING RADIOACTIVE SOURCE! GET TO THE CONTAINER!");

        // Visual: Hide the floor object, maybe show an icon on UI?
        if (visualModel) visualModel.SetActive(false);

        // Mechanic: Increase Radiation Damage significantly while holding
        // We will hack into the Health script for this
        PlayerHealth.Instance.isCarryingSource = true; 
    }

    public string GetDescription()
    {
        return "Pick Up Source (REQUIRES REMOTE TOOL)";
    }
}