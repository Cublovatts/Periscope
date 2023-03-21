using UnityEngine;

public class DialogueTriggerCatLady : MonoBehaviour, ITrigger
{
    public Dialogue introDialogue;
    public Dialogue succeededDialogue;
    public InteractionIndicator interactionIndicator;
    public CurrencyCount currencyCount;

    private DialogueManager _dialogueManager;
    private QuestManager _questManager;

    

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    [ContextMenu("TriggerDialogue")]
    public void TriggerDialogue()
    {
        try
        {
            int catLadyQuestProgress = _questManager.GetQuestProgress("MOGGY");
            switch (catLadyQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(succeededDialogue, SucceededDialogueUpdate);
                    break;
            }
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        
    }

    public void IntroDialogueUpdate()
    {
        try
        {
            _questManager.SetQuestProgress("MOGGY", 1);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        interactionIndicator.SetAvailable(true);
    }

    public void SucceededDialogueUpdate()
    {
        try
        {
            _questManager.SetQuestProgress("MOGGY", 2);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
         
        interactionIndicator.SetAvailable(false);
        currencyCount.AddCurrency(5.00f);
    }

    public void Trigger()
    {
        TriggerDialogue();
    }
}
