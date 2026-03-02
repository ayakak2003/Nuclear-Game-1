using UnityEngine;
using System; // Required for Actions

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // THE OBSERVER EVENT
    // Any script can subscribe to this to know when the phase changes.
    public static event Action<GamePhase> OnPhaseChanged;

    [Header("Debug")]
    public GamePhase currentPhase;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Initialize the first objective on game start
        SetPhase(GamePhase.Start);
    }

    // Call this from your Quizzes or Interactables
    public void CompletePhase(GamePhase newPhase)
    {
        SetPhase(newPhase);
    }
    public void CompletePhaseByString(string newPhase)
    {
        if (Enum.TryParse(newPhase, out GamePhase phase))
            SetPhase(phase);
    }

    private void SetPhase(GamePhase phase)
    {
        currentPhase = phase;
        
        // Notify all listeners (UI, Audio, Doors) that phase has changed
        // The '?' checks if there are any subscribers before invoking to prevent null errors.
        OnPhaseChanged?.Invoke(currentPhase);
        
        Debug.Log($"Phase Updated to: {phase}");
    }
    
    // Win Condition helper
    public void WinGame()
    {
        SetPhase(GamePhase.Win);
        // Add specific Win logic here (Scene load, confetti, etc.)
    }
}