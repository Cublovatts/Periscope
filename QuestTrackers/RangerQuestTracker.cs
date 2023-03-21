using UnityEngine;

public class RangerQuestTracker : MonoBehaviour
{
    private int sticksToGet = 8;
    private int sticksGotten = 0;
    private QuestManager _questManager;

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
                _questManager.SetQuestProgress("Pick up sticks", 2);
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
