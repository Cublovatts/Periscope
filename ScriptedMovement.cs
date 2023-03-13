using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ScriptedMovement : MonoBehaviour
{
    public MovementDestination[] destinations;
    [SerializeField] private float WALKING_SPEED = 3.0f;

    private Animator _animator;
    private GameObject _player;

    private MovementDestination currentDestination;
    private int destinationNumber;
    private Vector3 newPos;

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
        currentDestination = destinations[destinationNumber];

        if (currentDestination.IsWarpDestination)
        {
            transform.position = currentDestination.Destination.transform.position;
        }

        var step = WALKING_SPEED * Time.deltaTime;
        newPos = Vector3.MoveTowards(transform.position, currentDestination.Destination.transform.position, step);
        Vector3 inputMovement = newPos - transform.position;
        transform.position = newPos;

        // Is destination reached
        if (Vector3.Distance(transform.position, currentDestination.Destination.transform.position) < 0.001f)
        {
            destinationNumber++;
            if (destinationNumber > destinations.Length -1)
            {
                destinationNumber = 0;
            }
        }

        // Always face forwards
        if (inputMovement != Vector3.zero)
        {
            transform.forward = inputMovement;
        }
    }
}
