using UnityEngine;

// 1. Define the Phases strictly (No magic strings!)
public enum GamePhase
{
    Start,
    ControlRoom, // Phase 1
    Lab,         // Phase 2
    Storage,     // Phase 3
    PowerRoom,   // Phase 4
    Win          // Game Over
}

// 2. The ScriptableObject definition
[CreateAssetMenu(fileName = "NewObjective", menuName = "GammaLeak/Objective Data")]
public class ObjectiveData : ScriptableObject
{
    [Header("Trigger Condition")]
    public GamePhase linkedPhase;

    [Header("UI Display")]
    public string objectiveTitle;
    [TextArea(3, 5)] // Makes the box bigger in Inspector
    public string objectiveDescription;
} 