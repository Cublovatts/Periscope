using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public delegate void QuestUpdateDelegate(QuestEnum questEnum);
    public event QuestUpdateDelegate QuestUpdate;

    List<Quest> quests = new List<Quest>();

    public enum QuestEnum
    {
        Lord_of_the_dance,
        Pick_up_sticks,
        Turn_the_tables,
        MOGGY
    }

    private void Start()
    {
        // SETUP QUESTS IN HERE
        quests.Add(new Quest(QuestEnum.Lord_of_the_dance, "Lord of the dance", new string [] { "Talk to the busker", "Jump on the drum box", "Collect your reward!", "Quest complete" }));
        quests.Add(new Quest(QuestEnum.Pick_up_sticks, "Pick up sticks", new string [] { "Talk to the park ranger", "Pick up those sticks", "Collect your reward!", "Quest complete" }));
        quests.Add(new Quest(QuestEnum.Turn_the_tables, "Turn the tables", new string [] { "Talk to the waiter", "Deliver food to tables", "Collect your reward!" , "Quest complete"}));
        quests.Add(new Quest(QuestEnum.MOGGY, "MOGGY", new string[] { "Talk to the cat lady", "Collect your reward!", "Quest complete" }));
    }

    public Quest GetQuest(QuestEnum questEnum)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetQuestEnum() == questEnum) return quest;
        }
        throw new System.Exception("Requested non existent quest: " + questEnum.ToString());
    }

    public string GetQuestName(QuestEnum questEnum)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetQuestEnum() == questEnum) return quest.GetName();
        }
        throw new System.Exception("Requested non existent quest: " + questEnum.ToString());
    }

    public int GetQuestProgress(QuestEnum questEnum)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetQuestEnum() == questEnum) return quest.GetProgress();
        }
        throw new System.Exception("Requested non existent quest progress: " + questEnum.ToString());
    }

    public void SetQuestProgress(QuestEnum questEnum, int progress)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetQuestEnum() == questEnum)
            {
                quest.SetProgress(progress);
                QuestUpdate?.Invoke(questEnum);
                return;
            }
        }
        throw new System.Exception("Requested non existent quest: " + questEnum.ToString());
    }

    public string GetQuestCurrentProgressDescription(QuestEnum questEnum)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetQuestEnum() == questEnum)
            {
               return quest.GetCurrentProgressDescription();
            }
        }
        throw new System.Exception("Requested non existent quest: " + questEnum.ToString());
    }

    [ContextMenu("DebugBusker")]
    public void DebugGetBuskerQuestProgress()
    {
        Debug.Log("Busker: " + GetQuestProgress(QuestEnum.Lord_of_the_dance));
    }
}
