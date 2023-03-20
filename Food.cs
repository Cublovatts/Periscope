using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, ITrigger
{
    private MovementScriptBlock movementScriptBlock;
    private Animator _animator;
    private Animator _playerAnimator;
    private RestaurantQuestManager restaurantQuestManager;

    void Start()
    {
        movementScriptBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScriptBlock>();
        _animator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        restaurantQuestManager = GameObject.Find("RestaurantQuestManager").GetComponent<RestaurantQuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDeliverySpot()
    {
        GameObject deliverySpot = restaurantQuestManager.GetRandomDeliveryLocation();
        deliverySpot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<InteractionIndicator>().SetAvailable(true);
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
