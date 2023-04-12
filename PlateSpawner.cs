using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    private RestaurantQuestTracker _restaurantQuestTracker;

    private void Start()
    {
        _restaurantQuestTracker = GameObject.Find("RestaurantQuestManager").GetComponent<RestaurantQuestTracker>();
    }

    public void SpawnPlate()
    {
        // Place plate of food at plate spawner
        GameObject foodOption = _restaurantQuestTracker.GetRandomFoodOption();
        GameObject newPlate = Instantiate(foodOption, gameObject.transform.parent);
        newPlate.transform.position = gameObject.transform.position;
    }

}
