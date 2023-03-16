using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public delegate void QuestUpdateDelegate(string questName);
    public static event QuestUpdateDelegate QuestUpdate;

    List<Quest> quests = new List<Quest>();

    private void Start()
    {
        // SETUP QUESTS IN HERE
        quests.Add(new Quest("Lord of the dance", new string [] { "Talk to the busker", "Collect your reward!" }));
        quests.Add(new Quest("Pick up sticks", new string [] { "Talk to the park ranger", "Pick up those sticks", "Collect your reward!", "Quest complete" }));
        quests.Add(new Quest("Turn the tables", new string [] { "Talk to the waiter", "Deliver food to tables", "Collect your reward!" , "Quest complete"}));
        quests.Add(new Quest("MOGGY", new string[] { "Talk to the cat lady", "Collect your reward!" }));
    }

    public Quest GetQuest(string questName)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetName() == questName) return quest;
        }
        throw new System.Exception("Requested non existent quest: " + questName);
    }

    public int GetQuestProgress(string questName)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetName() == questName) return quest.GetProgress();
        }
        throw new System.Exception("Requested non existent quest progress: " + questName);
    }

    public void SetQuestProgress(string questName, int progress)
    {
        foreach (Quest quest in quests)
        {
            if (quest.GetName() == questName) quest.SetProgress(progress);
        }
    }
}
