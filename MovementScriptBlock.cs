using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SocialPlatforms;

public class MovementScriptBlock : MonoBehaviour
{
    [SerializeField] private float RUNNING_SPEED = 3.0f;
    [SerializeField] private float WALKING_SPEED = 1.0f;

    private Animator _animator;
    private bool IsShifting = false;

    public bool IsAvailable = true;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAvailable)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _animator.SetBool("IsShifting", true);
                IsShifting = true;
            }
            else
            {
                _animator.SetBool("IsShifting", false);
                IsShifting = false;
            }

            Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (IsShifting)
            {
                transform.Translate(inputMovement * Time.deltaTime * RUNNING_SPEED, Space.World);
            }
            else
            {
                transform.Translate(inputMovement * Time.deltaTime * WALKING_SPEED, Space.World);
            }


            if (inputMovement != Vector3.zero)
            {
                transform.forward = inputMovement;
            }



            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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
