using UnityEngine;
using UnityEngine.UI; // Required for UI
using UnityEngine.SceneManagement; // Required to restart level

public class PlayerHealth : MonoBehaviour
{
    // Singleton for easy access
    public static PlayerHealth Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private float maxDose = 100f;
    [SerializeField] private Slider dosimeterSlider; // Drag UI Slider here
    [SerializeField] private Image fillImage; // To change color (Green -> Red)

    private float _currentDose = 0f;
    private bool _isDead = false;
    public bool isCarryingSource = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _currentDose = 0f;
        UpdateUI();
    }
    private void Update()
    {
        if (isCarryingSource)
        {
            // Massive damage every frame if holding the source
            AbsorbRadiation(5f * Time.deltaTime); 
        }
    }

    public void AbsorbRadiation(float amount)
    {
        if (_isDead) return;

        _currentDose += amount;
        UpdateUI();

        // Check for "Death"
        if (_currentDose >= maxDose)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        if (dosimeterSlider != null)
        {
            dosimeterSlider.value = _currentDose / maxDose; // Normalize to 0-1

            // Visual Flair: Change color as danger increases
            if (fillImage != null)
            {
                fillImage.color = Color.Lerp(Color.green, Color.red, _currentDose / maxDose);
            }
        }
    }

    private void Die()
    {
        _isDead = true;
        Debug.Log("☢️ CRITICAL DOSE RECEIVED. MISSION FAILED.");
        
        // Simple Restart Logic for now
        // In a real game, show a "Game Over" screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}