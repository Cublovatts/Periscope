using UnityEngine;

public class MovementScriptBlock : MonoBehaviour
{
    public bool IsAvailable = true;

    [SerializeField] 
    private float _runningSpeed = 3.0f;
    [SerializeField] 
    private float _walkingSpeed = 1.0f;

    private Animator _animator;
    private bool _isShifting = false;
    private readonly float _movementRotationOffsetAngle = -45f;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsAvailable)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _animator.SetBool("IsShifting", true);
                _isShifting = true;
            }
            else
            {
                _animator.SetBool("IsShifting", false);
                _isShifting = false;
            }

            Vector3 inputMovement = Quaternion.Euler(0, _movementRotationOffsetAngle, 0) * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (_isShifting)
            {
                transform.Translate(inputMovement * Time.deltaTime * _runningSpeed, Space.World);
            }
            else
            {
                transform.Translate(inputMovement * Time.deltaTime * _walkingSpeed, Space.World);
            }


            if (inputMovement != Vector3.zero)
            {
                transform.forward = inputMovement;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || 
                Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                _animator.SetBool("IsMoving", true);
            }
            else
            {
                _animator.SetBool("IsMoving", false);
            }
        }
    }
}
