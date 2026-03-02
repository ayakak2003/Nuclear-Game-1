using UnityEngine;

public class RadiationZone : MonoBehaviour
{
    [Header("Radiation Settings")]
    [SerializeField] private float baseRadiationPerSecond = 10f; // Fatal in 10 seconds unshielded
    [SerializeField] private float shieldedReductionFactor = 0.1f; // 90% protection with suit

    private void OnTriggerStay2D(Collider2D other)
    {
        // Only affect the player
        if (other.CompareTag("Player"))
        {
            float damageToDeal = baseRadiationPerSecond * Time.deltaTime;

            // Check if player has the suit (The Mitigation Mechanic)
            if (PlayerInventory.Instance.hasSuit)
            {
                damageToDeal *= shieldedReductionFactor; // Reduce damage significantly
            }

            // Apply the dose
            PlayerHealth.Instance.AbsorbRadiation(damageToDeal);
        }
    }
}