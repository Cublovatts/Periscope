using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SocialPlatforms;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float MOVE_SPEED = 3.0f;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(inputMovement * Time.deltaTime * MOVE_SPEED, Space.World);

        if (inputMovement != Vector3.zero)
        {
            transform.forward = inputMovement;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("IsWalking", true);
        } else
        {
            _animator.SetBool("IsWalking", false);
        }
    }
}
