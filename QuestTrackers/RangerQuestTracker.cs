using UnityEngine;

public class RangerQuestTracker : MonoBehaviour
{
    private const QuestManager.QuestEnum RANGER_QUEST_REF = QuestManager.QuestEnum.Pick_up_sticks;

    private QuestManager _questManager;

    private int _sticksToGet = 8;
    private int _sticksGotten = 0;

    private void Start()
    {
        _questManager = QuestManager.instance;
    }

    [ContextMenu("Add stick")]
    public void AddStick()
    {
        _sticksGotten++;

        if (_sticksGotten == _sticksToGet)
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
        return _sticksGotten;
    }

    public int GetMaxSticks()
    {
        return _sticksToGet;
    }
}
