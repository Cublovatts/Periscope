using UnityEngine;

public class ScriptedMovement : MonoBehaviour
{
    public MovementDestination[] Destinations;
    [SerializeField] private float _walkingSpeed = 3.0f;
    [SerializeField] private bool _isBlockable;

    private Animator _animator;
    private GameObject _player;

    private MovementDestination _currentDestination;
    private int _destinationNumber;
    private Vector3 _newPos;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsMoving", true);
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, _player.transform.position);
        if (_isBlockable && dist < 3.0f)
        {
            _animator.SetBool("IsMoving", false);
            return;
        }
        _animator.SetBool("IsMoving", true);

        _currentDestination = Destinations[_destinationNumber];

        if (_currentDestination.IsWarpDestination)
        {
            transform.position = _currentDestination.Destination.transform.position;
        }

        var step = _walkingSpeed * Time.deltaTime;
        _newPos = Vector3.MoveTowards(transform.position, _currentDestination.Destination.transform.position, step);
        Vector3 inputMovement = _newPos - transform.position;
        transform.position = _newPos;

        // Is destination reached
        if (Vector3.Distance(transform.position, _currentDestination.Destination.transform.position) < 0.001f)
        {
            _destinationNumber++;
            if (_destinationNumber > Destinations.Length -1)
            {
                _destinationNumber = 0;
            }
        }

        // Always face forwards
        if (inputMovement != Vector3.zero)
        {
            transform.forward = inputMovement;
        }
    }
}
