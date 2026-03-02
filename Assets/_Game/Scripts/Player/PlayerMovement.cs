using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 rawInput;
    private Vector2 moveVelocity;
    private PlayerControls controls;

    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 1.5f;
    [SerializeField] private LayerMask interactableLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    private void Update()
    {
        rawInput = controls.Player.Move.ReadValue<Vector2>();

        Vector2 clampedInput = Vector2.ClampMagnitude(rawInput, 1f);
        
        moveVelocity = clampedInput * moveSpeed;

        if (controls.Player.Interact.WasPressedThisFrame())
        {
            TryInteract();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity; 
    }

    private void TryInteract()
    {
        // Create a small circle around the player to find objects
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

        foreach (var hit in hitColliders)
        {
            // Check if the object has the IInteractable interface
            IInteractable interactable = hit.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                // Found one! Trigger it and stop looking.
                interactable.Interact();
                return; // Only interact with one item at a time
            }
        }
    }

    // Visualize the interaction range in the Editor (Gizmos)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}