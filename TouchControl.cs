using UnityEngine;

public class TouchControl : MonoBehaviour
{

    public float speed = 5f;  // The speed of the character movement

    private Rigidbody2D _rigidbody;  // The Rigidbody2D component of the character
    private Animator _animator;
    private Camera _mainCamera;

    private Vector3 _touchPosition;
    private bool _touchEnabled = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component of the character
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            _touchEnabled = true;
        }

        if (Input.touchCount > 0 && _touchEnabled)
        {  // Check if there is at least one touch input
            _animator.SetBool("IsShifting", true);
            _animator.SetBool("IsMoving", true);
            Touch touch = Input.GetTouch(0);  // Get the first touch input

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                // Store the touch position and start moving the character
                _touchPosition = _mainCamera.ScreenToWorldPoint(touch.position);
                //_touchPosition.z = transform.position.z;

                Vector3 direction = (_touchPosition - transform.position).normalized;
                direction.y = 0;
                //_rigidbody.velocity = direction * speed;
                transform.Translate(direction * Time.deltaTime * speed, Space.World);

                if (direction != Vector3.zero)
                {
                    transform.forward = direction;
                }

            }  
        } else if (_touchEnabled)
        {
            _animator.SetBool("IsShifting", false);
            _animator.SetBool("IsMoving", false);
        }
    }
}
