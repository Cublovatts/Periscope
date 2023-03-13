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
    }

    public void IntroDialogueUpdate()
    {
        _questManager.SetQuestProgress("Pick up sticks", 1);
        interactionIndicator.SetAvailable(true);
    }

    public void QuestInProgressUpdate()
    {
        interactionIndicator.SetAvailable(true);
    }

    public void SucceededDialogueUpdate()
    {
        _questManager.SetQuestProgress("Pick up sticks", 3);
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
