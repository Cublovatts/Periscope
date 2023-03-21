using UnityEngine;

public class DialogueTriggerRanger : MonoBehaviour, ITrigger
{
    public Dialogue introDialogue;
    public Dialogue questInProgressDialogue;
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
            int rangerQuestProgress = _questManager.GetQuestProgress("Pick up sticks");
            switch (rangerQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(questInProgressDialogue, QuestInProgressUpdate);
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
            Debug.LogError("Couldn't find quest")
        }
        
    }

    public void IntroDialogueUpdate()
    {
        try
        {
            _questManager.SetQuestProgress("Pick up sticks", 1);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        interactionIndicator.SetAvailable(true);
    }

    public void QuestInProgressUpdate()
    {
        interactionIndicator.SetAvailable(true);
    }

    public void SucceededDialogueUpdate()
    {
        try
        {
            _questManager.SetQuestProgress("Pick up sticks", 3);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        interactionIndicator.SetAvailable(true);
        currencyCount.AddCurrency(5.00f);
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
