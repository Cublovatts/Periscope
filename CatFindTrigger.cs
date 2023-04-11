using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFindTrigger : MonoBehaviour, ITrigger
{
    static private QuestManager.QuestEnum CAT_QUEST_REF = QuestManager.QuestEnum.MOGGY;

    private InteractionIndicator _indicator;
    private QuestManager _questManager;


    void Start()
    {
        _indicator = gameObject.GetComponentInChildren<InteractionIndicator>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
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
        // Send cat home
        // Update quest progress
        _questManager.SetQuestProgress(CAT_QUEST_REF, 2);
    }
}
