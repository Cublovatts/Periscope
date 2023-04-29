using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _questContainer;

    private QuestManager.QuestEnum[] questEnums = {
        QuestManager.QuestEnum.Lord_of_the_dance,
        QuestManager.QuestEnum.Pick_up_sticks,
        QuestManager.QuestEnum.Turn_the_tables,
        QuestManager.QuestEnum.MOGGY
    };
    private readonly int _offset = 150;
    private readonly List<QuestContainer> _questContainers = new List<QuestContainer>();

    private QuestManager _questManager;

    private float _lastActivated = 0;
    private bool _isShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _questManager.QuestUpdate += UpdateQuest;


        int targetHeight = 0;

        foreach (QuestManager.QuestEnum questEnum in questEnums)
        {
            // Instantiate a new QuestContainer prefab
            GameObject newQuestContainer = Instantiate(_questContainer, gameObject.transform);
            newQuestContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, targetHeight);
            // Set the title, description and completeness to false
            QuestContainer questContainerComponent = newQuestContainer.GetComponent<QuestContainer>();
            questContainerComponent.InstaniateQuestContainer();
            questContainerComponent.SetQuestEnum(questEnum);
            questContainerComponent.SetTitle(_questManager.GetQuestName(questEnum));
            questContainerComponent.SetDescription(_questManager.GetQuestCurrentProgressDescription(questEnum));
            questContainerComponent.SetCompleted(false);
            // Add a reference to the QuestContainer script to the array
            _questContainers.Add(questContainerComponent);
            targetHeight += _offset;
        }
        HideQuestTrackers();
    }

    // Update is called once per frame
    void Update()
    {
        // Show when a button is pressed
        if (Input.GetKeyDown(KeyCode.Tab) && !_isShowing)
        {
            ShowQuestTrackers();
        } 
        else if (Input.GetKeyDown(KeyCode.Tab) && _isShowing)
        {
            HideQuestTrackers();
        }

        // Disappear if has been showing for 10 seconds
        if (Time.time > _lastActivated + 10)
        {
            HideQuestTrackers();
        }
    }

    public void UpdateQuest(QuestManager.QuestEnum questEnum)
    {
        foreach (QuestContainer container in _questContainers)
        {
            if (container.GetQuestEnum() == questEnum)
            {
                ShowQuestTrackers();
                container.SetDescription(_questManager.GetQuestCurrentProgressDescription(questEnum));
            }
        }
    }

    public void HideQuestTrackers()
    {
        foreach (QuestContainer container in _questContainers)
        {
            container.Hide();
        }
        _isShowing = false;
    }

    public void ShowQuestTrackers()
    {
        _lastActivated = Time.time;
        foreach (QuestContainer container in _questContainers)
        {
            container.Show();
        }
        _isShowing = true;
    }
}
