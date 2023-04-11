using UnityEngine;

public class DialogueTriggerCatLady : MonoBehaviour, ITrigger
{
    static private QuestManager.QuestEnum CAT_QUEST_REF = QuestManager.QuestEnum.MOGGY;

    public Dialogue introDialogue;
    public Dialogue inProgressDialogue;
    public Dialogue succeededDialogue;
    public Dialogue fillerDialogue;
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
            int catLadyQuestProgress = _questManager.GetQuestProgress(CAT_QUEST_REF);
            switch (catLadyQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(inProgressDialogue, InProgressDialogueUpdate);
                    break;
                case 2:
                    _dialogueManager.StartDialogue(succeededDialogue, SucceededDialogueUpdate);
                    break;
                case 3:
                    _dialogueManager.StartDialogue(fillerDialogue, FillerDialogueUpdate);
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
            _questManager.SetQuestProgress(CAT_QUEST_REF, 1);
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
            _questManager.SetQuestProgress(CAT_QUEST_REF, 3);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
         
        interactionIndicator.SetAvailable(true);
        currencyCount.AddCurrency(5);
    }

    public void InProgressDialogueUpdate()
    {
        interactionIndicator.SetAvailable(true);
    }

    public void FillerDialogueUpdate()
    {
        interactionIndicator.SetAvailable(true);
    }

    public void Trigger()
    {
        TriggerDialogue();
    }
}
