using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;

    // We store the last direction so the player stays facing that way when idle
    private Vector2 _lastMoveDirection = Vector2.down; 

    // Animator Parameter Hashes (More efficient than using strings)
    private static readonly int InputX = Animator.StringToHash("InputX");
    private static readonly int InputY = Animator.StringToHash("InputY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 1. Check if we are moving based on the Rigidbody velocity
        // We use sqrMagnitude because it is faster than calculating the square root
        bool isMoving = _rb.velocity.sqrMagnitude > 0.01f;

        // 2. Tell the animator if we are walking or not
        _animator.SetBool(IsMoving, isMoving);

        // 3. Update direction parameters
        if (isMoving)
        {
            // Normalize the vector so diagonal speed doesn't mess up the blend weight
            Vector2 direction = _rb.velocity.normalized;

            _animator.SetFloat(InputX, direction.x);
            _animator.SetFloat(InputY, direction.y);

            // Save this direction for when we stop
            _lastMoveDirection = direction;
        }
        else
        {
            // If we stopped, feed the animator the LAST known direction
            // effectively "freezing" the facing direction
            _animator.SetFloat(InputX, _lastMoveDirection.x);
            _animator.SetFloat(InputY, _lastMoveDirection.y);
        }
    }
}