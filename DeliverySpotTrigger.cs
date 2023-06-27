using System.Collections;
using UnityEngine;

public class DeliverySpotTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    private GameObject _foodPrefab;
    [SerializeField]
    private MovementScriptBlock _movementScriptBlock;
    [SerializeField]
    private Animator _playerAnimator;
    private RestaurantQuestTracker _restaurantQuestTracker;

    void Start()
    {
        _restaurantQuestTracker = GameObject.Find("RestaurantQuestManager").GetComponent<RestaurantQuestTracker>();
    }

    public void Trigger()
    {
        StartCoroutine(DeliverFood());
        _restaurantQuestTracker.IncrementDeliveries(1);
    }

    IEnumerator DeliverFood()
    {
        _playerAnimator.Play("PickUpMid");
        _movementScriptBlock.IsAvailable = false;
        Animator plateAnimator = Instantiate(_foodPrefab, gameObject.transform).GetComponent<Animator>();
        plateAnimator.Play("DeliveredPlateAppear");
        yield return new WaitForSeconds(1.0f);
        _movementScriptBlock.IsAvailable = true;
    }
}
