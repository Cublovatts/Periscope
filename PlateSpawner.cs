using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPlate;

    public void SpawnPlate()
    {
        // TODO: Select a new plate of food randomly from List of possible plates of food 
        // Place plate of food at plate spawner
        GameObject newPlate = GameObject.Instantiate(foodPlate, gameObject.transform.parent);
        newPlate.transform.position = gameObject.transform.position;
    }

}
