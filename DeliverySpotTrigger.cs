using UnityEngine;

public class DeliverySpotTrigger : MonoBehaviour, ITrigger
{
    RestaurantQuestManager restaurantQuestManager;

    

    void Start()
    {
        restaurantQuestManager = GameObject.Find("RestaurantQuestManager").GetComponent<RestaurantQuestManager>();
    }

    public void Trigger()
    {
        // Animate food being delivered
        restaurantQuestManager.IncrementDeliveries(1);
    }
}
