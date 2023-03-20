using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    private RestaurantQuestManager restaurantQuestManager;

    private void Start()
    {
        restaurantQuestManager = GameObject.Find("RestaurantQuestManager").GetComponent<RestaurantQuestManager>();
    }

    public void SpawnPlate()
    {
        // Place plate of food at plate spawner
        GameObject foodOption = restaurantQuestManager.GetRandomFoodOption();
        GameObject newPlate = GameObject.Instantiate(foodOption, gameObject.transform.parent);
        newPlate.transform.position = gameObject.transform.position;
    }

}
