using UnityEngine;

public class DeliverySpotTrigger : MonoBehaviour, ITrigger
{
    private RestaurantQuestTracker _restaurantQuestTracker;

    void Start()
    {
        _restaurantQuestTracker = GameObject.Find("RestaurantQuestManager").GetComponent<RestaurantQuestTracker>();
    }

    public void Trigger()
    {
        // TODO: Animate food being delivered
        _restaurantQuestTracker.IncrementDeliveries(1);
    }
}
