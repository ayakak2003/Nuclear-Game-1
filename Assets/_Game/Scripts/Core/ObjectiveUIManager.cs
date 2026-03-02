using UnityEngine;
using TMPro; // Standard for Unity UI Text
using System.Collections.Generic;
using System.Linq; // Allows us to use LINQ for easy list searching

public class ObjectiveUIManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject panel; // To hide it if needed

    [Header("Data")]
    // Drag all your ScriptableObjects here in the Inspector
    [SerializeField] private List<ObjectiveData> allObjectives;

    // 1. Subscribe to the event when this UI object turns on
    private void OnEnable()
    {
        GameManager.OnPhaseChanged += UpdateObjectiveDisplay;
    }

    // 2. Unsubscribe when turned off (CRITICAL to prevent memory leaks)
    private void OnDisable()
    {
        GameManager.OnPhaseChanged -= UpdateObjectiveDisplay;

        // if (GameManager.Instance != null)
        // UpdateObjectiveDisplay(GameManager.Instance.currentPhase);
    }

    // 3. The actual logic called by the event
    private void UpdateObjectiveDisplay(GamePhase newPhase)
    {
        // LINQ Query: Find the data that matches the new phase
        ObjectiveData data = allObjectives.FirstOrDefault(o => o.linkedPhase == newPhase);

        if (data != null)
        {
            panel.SetActive(true);
            titleText.text = data.objectiveTitle;
            descriptionText.text = data.objectiveDescription;
            
            // Optional: Add a simple pop animation effect here
        }
        else
        {
            // If no objective data exists for this phase (e.g., Win screen), hide the box
            Debug.LogWarning($"No Objective Data found for phase: {newPhase}");
        }
    }
}