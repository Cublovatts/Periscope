using System.Collections;
using System.Collections.Generic;
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
    }

    public void IntroDialogueUpdate()
    {
        _questManager.SetQuestProgress("MOGGY", 1);
        interactionIndicator.SetAvailable(true);
    }

    public void SucceededDialogueUpdate()
    {
        _questManager.SetQuestProgress("MOGGY", 2);
        interactionIndicator.SetAvailable(false);
        currencyCount.AddCurrency(5.00f);
    }

    public void Trigger()
    {
        TriggerDialogue();
    }
}
