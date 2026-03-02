using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    [Header("Configuration")]
    [SerializeField] private string actionName = "Interact"; 
    [SerializeField] private GameObject promptObject; // Drag your UI Prefab here

    [Header("Events")]
    public UnityEvent OnInteract;
    public UnityEvent onPlayerExit;

    private void Start()
    {
        // Ensure the prompt is hidden when the game starts
        if (promptObject != null)
        {
            promptObject.transform.GetChild(0).GetChild(0).GetChild(0).
            GetComponentInChildren<TextMeshProUGUI>().text = $" Press \"E\" to \n{actionName}";
            promptObject.SetActive(false);
        }
    }

    public void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
        promptObject.SetActive(false);
        OnInteract?.Invoke();
    }

    public string GetDescription()
    {
        return actionName;
    }

    // Triggers handle the VISUALS (Showing the UI)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && promptObject != null)
        {
            promptObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && promptObject != null)
        {
            promptObject.SetActive(false);
            onPlayerExit.Invoke();
        }
    }
}