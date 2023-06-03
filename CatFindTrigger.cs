using UnityEngine;

public class CatFindTrigger : MonoBehaviour, ITrigger
{
    static private readonly QuestManager.QuestEnum CAT_QUEST_REF = QuestManager.QuestEnum.MOGGY;

    private InteractionIndicator _indicator;
    private QuestManager _questManager;

    void Start()
    {
        _indicator = gameObject.GetComponentInChildren<InteractionIndicator>();
        _questManager = QuestManager.instance;
    }

    void Update()
    {
        // Only show indicator at correct quest progress
        if (_questManager.GetQuestProgress(CAT_QUEST_REF) == 1)
        {
            _indicator.SetAvailable(true);
        } else
        {
            _indicator.SetAvailable(false);
        }
    }

    public void Trigger()
    {
        _questManager.SetQuestProgress(CAT_QUEST_REF, 2);
    }
}
