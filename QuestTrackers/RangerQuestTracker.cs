using UnityEngine;

public class RangerQuestTracker : MonoBehaviour
{
    static private readonly QuestManager.QuestEnum RANGER_QUEST_REF = QuestManager.QuestEnum.Pick_up_sticks;

    private QuestManager _questManager;

    private int sticksToGet = 8;
    private int sticksGotten = 0;

    private void Start()
    {
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    [ContextMenu("Add stick")]
    public void AddStick()
    {
        sticksGotten++;

        if (sticksGotten == sticksToGet)
        {
            try
            {
                _questManager.SetQuestProgress(RANGER_QUEST_REF, 2);
            } catch (System.Exception e) 
            { 
                Debug.LogError(e);
                Debug.LogError("Couldn't find quest");
            }
            
        }
    }

    public int GetCurrentSticks()
    {
        return sticksGotten;
    }

    public int GetMaxSticks()
    {
        return sticksToGet;
    }


}
