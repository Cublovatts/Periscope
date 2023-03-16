using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, ITrigger
{
    private MovementScriptBlock movementScriptBlock;
    private Animator _animator;
    private Animator _playerAnimator;

    void Start()
    {
        movementScriptBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScriptBlock>();
        _animator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDeliverySpot()
    {
        Debug.Log("Setting delivery spot");
    }

    public void Trigger()
    {
        StartCoroutine(DisappearFood());
    }

    IEnumerator DisappearFood()
    {
        movementScriptBlock.IsAvailable = false;
        _playerAnimator.Play("PickUpMid");
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("IsDisappearing", true);
        yield return new WaitForSeconds(0.5f);
        movementScriptBlock.IsAvailable = true;
        gameObject.SetActive(false);
        SetDeliverySpot();
    }
}
