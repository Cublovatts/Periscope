using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerWaiter : MonoBehaviour, ITrigger
{
    public Dialogue introDialogue;
    public Dialogue questInProgressDialogue;
    public Dialogue succeededDialogue;
    public Dialogue fillerDialogue;
    public InteractionIndicator interactionIndicator;
    public CurrencyCount currencyCount;

    private DialogueManager _dialogueManager;
    private QuestManager _questManager;

    private PlateSpawner _spawner;

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _spawner = GameObject.Find("PlateSpawner").GetComponent<PlateSpawner>();
    }

    [ContextMenu("TriggerDialogue")]
    public void TriggerDialogue()
    {
        int waiterQuestProgress = _questManager.GetQuestProgress("Turn the tables");
        switch (waiterQuestProgress)
        {
            case 0:
                _dialogueManager.StartDialogue(introDialogue, IntroDialogueUpdate);
                break;
            case 1:
                _dialogueManager.StartDialogue(questInProgressDialogue, QuestInProgressDialogueUpdate);
                break;
        }
    }

    public void IntroDialogueUpdate()
    {
        _questManager.SetQuestProgress("Turn the tables", 1);
        interactionIndicator.SetAvailable(true);
        _spawner.SpawnPlate();
    }

    public void QuestInProgressDialogueUpdate()
    {
        interactionIndicator.SetAvailable(true);
    }

    public void SucceededDialogueUpdate()
    {
        _questManager.SetQuestProgress("Turn the tables", 3);
        interactionIndicator.SetAvailable(false);
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
