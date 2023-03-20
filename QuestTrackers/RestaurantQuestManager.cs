using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantQuestManager : MonoBehaviour
{
    [SerializeField]
    private int requiredDeliveries = 5;
    private int currentDeliveries = 0;

    private PlateSpawner plateSpawner;
    private QuestManager questManager;

    [SerializeField]
    private GameObject[] deliveryLocations;
    [SerializeField]
    private GameObject[] foodOptions;

    public void Start()
    {
        plateSpawner = GameObject.Find("PlateSpawner").GetComponent<PlateSpawner>();
        questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    public void IncrementDeliveries(int count)
    {
        currentDeliveries += count;
        Debug.Log("Incrementing deliveries: " + currentDeliveries);
        if (currentDeliveries < requiredDeliveries)
        {
            Debug.Log("Spawning plate");
            plateSpawner.SpawnPlate();
        } else
        {
            questManager.SetQuestProgress("Turn the tables", 2);
        }
    }

    public int GetCurrentDeliveries(int count)
    {
        return currentDeliveries;
    }

    public int GetRequiredDeliveries()
    {
        return requiredDeliveries;
    }

    public GameObject GetRandomDeliveryLocation()
    {
        int index = Random.Range(0, deliveryLocations.Length);
        return deliveryLocations[index];
    }

    public GameObject GetRandomFoodOption()
    {
        int index = Random.Range(0, foodOptions.Length);
        return foodOptions[index];
    }
}
