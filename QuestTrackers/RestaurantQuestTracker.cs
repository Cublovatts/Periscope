using UnityEngine;

public class RestaurantQuestTracker : MonoBehaviour
{
    private const QuestManager.QuestEnum RESTAURANT_QUEST_REF = QuestManager.QuestEnum.Turn_the_tables;

    [SerializeField]
    private int _requiredDeliveries = 5;
    private int _currentDeliveries = 0;

    [SerializeField]
    private PlateSpawner _plateSpawner;
    private QuestManager _questManager;

    [SerializeField]
    private GameObject[] _deliveryLocations;
    [SerializeField]
    private GameObject[] _foodOptions;

    public void Start()
    {
        _questManager = QuestManager.instance;
    }

    public void IncrementDeliveries(int count)
    {
        _currentDeliveries += count;
        if (_currentDeliveries < _requiredDeliveries)
        {
            _plateSpawner.SpawnPlate();
        } else
        {
            try
            {
                _questManager.SetQuestProgress(RESTAURANT_QUEST_REF, 2);
            } catch (System.Exception e)
            {
                Debug.LogError(e);
                Debug.LogError("Couldn't find quest");
            }
        }
    }

    public int GetCurrentDeliveries(int count)
    {
        return _currentDeliveries;
    }

    public int GetRequiredDeliveries()
    {
        return _requiredDeliveries;
    }

    public GameObject GetRandomDeliveryLocation()
    {
        int index = Random.Range(0, _deliveryLocations.Length);
        return _deliveryLocations[index];
    }

    public GameObject GetRandomFoodOption()
    {
        int index = Random.Range(0, _foodOptions.Length);
        return _foodOptions[index];
    }
}
