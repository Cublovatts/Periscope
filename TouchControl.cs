using UnityEngine;

public class TouchControl : MonoBehaviour
{

    public float speed = 5f;  // The speed of the character movement

    private Rigidbody2D rb;  // The Rigidbody2D component of the character
    private Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component of the character
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {  // Check if there is at least one touch input
            _animator.SetBool("IsShifting", true);
            _animator.SetBool("IsMoving", true);
            Touch touch = Input.GetTouch(0);  // Get the first touch input

            if (touch.phase == TouchPhase.Moved)
            {  // Check if the touch input has moved
                Vector2 touchDeltaPosition = touch.deltaPosition;  // Get the position delta of the touch input
                Vector2 moveDirection = new Vector2(touchDeltaPosition.x, touchDeltaPosition.y).normalized;  // Calculate the normalized direction of movement
                rb.velocity = moveDirection * speed;  // Set the velocity of the character to move in the calculated direction
                transform.forward = moveDirection;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {  // Check if the touch input has ended or been canceled
                rb.velocity = Vector2.zero;  // Stop the character's movement
            }
        } else
        {
            _animator.SetBool("IsShifting", true);
            _animator.SetBool("IsMoving", false);
        }
    }
}
