using System;
using UnityEngine;

public class DialogueTriggerBusker : MonoBehaviour, ITrigger
{
    public Dialogue introDialogue;
    public Dialogue succeededDialogue;
    public InteractionIndicator interactionIndicator;
    public CurrencyCount currencyCount;

    private DialogueManager _dialogueManager;
    private QuestManager _questManager;

    private void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }


    [ContextMenu("TriggerDialogue")]
    public void TriggerDialogue()
    {
        try
        {
            int buskerQuestProgress = _questManager.GetQuestProgress("Lord of the dance");
            switch (buskerQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(succeededDialogue, SucceededDialogueUpdate);
                    break;
            }
        } catch (Exception e) 
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
    }

    public void IntroDialogueUpdate()
    {
        try
        {
            _questManager.SetQuestProgress("Lord of the dance", 1);
        } catch(Exception e)
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
            _questManager.SetQuestProgress("Lord of the dance", 2);
        } catch (Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        
        interactionIndicator.SetAvailable(false);
        currencyCount.AddCurrency(5.0f);
    }

    public void Trigger()
    {
        TriggerDialogue();
    }
}
