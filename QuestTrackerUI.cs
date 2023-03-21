using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject questContainer;

    static private string[] questTitles = { "Lord of the dance", "Pick up sticks" , "Turn the tables" , "MOGGY" };
    static private int offset = 150;
    private List<QuestContainer> questContainers = new List<QuestContainer>();

    private QuestManager _questManager;

    private float lastActivated;

    // Start is called before the first frame update
    void Start()
    {
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _questManager.QuestUpdate += UpdateQuest;


        int targetHeight = 0;

        foreach (string title in questTitles)
        {
            // Instantiate a new QuestContainer prefab
            GameObject newQuestContainer = Instantiate(questContainer, gameObject.transform);
            newQuestContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, targetHeight);
            // Set the title, description and completeness to false
            QuestContainer questContainerComponent = newQuestContainer.GetComponent<QuestContainer>();
            questContainerComponent.InstaniateQuestContainer();
            questContainerComponent.SetTitle(title);
            questContainerComponent.SetDescription(_questManager.GetQuestCurrentProgressDescription(title));
            questContainerComponent.SetCompleted(false);
            // Add a reference to the QuestContainer script to the array
            questContainers.Add(questContainerComponent);
            targetHeight += offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Show when a button is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowQuestTrackers();
        }

        // Disappear if has been showing for 10 seconds
        if (Time.time > lastActivated + 10)
        {
            HideQuestTrackers();
        }
    }

    public void UpdateQuest(string questName)
    {
        foreach (QuestContainer container in questContainers)
        {
            if (container.GetTitle() == questName)
            {
                ShowQuestTrackers();
                container.SetDescription(_questManager.GetQuestCurrentProgressDescription(questName));
            }
        }
    }

    public void HideQuestTrackers()
    {
        foreach (QuestContainer container in questContainers)
        {
            container.Hide();
        }
    }

    public void ShowQuestTrackers()
    {
        lastActivated = Time.time;
        foreach (QuestContainer container in questContainers)
        {
            container.Show();
        }
    }
}
