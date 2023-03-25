using System;
using UnityEngine;

public class DialogueTriggerBusker : MonoBehaviour, ITrigger
{
    [SerializeField]
    private Animator _buskerAnimator;

    static private QuestManager.QuestEnum BUSKER_QUEST_REF = QuestManager.QuestEnum.Lord_of_the_dance;

    public Dialogue introDialogue;
    public Dialogue succeededDialogue;
    public Dialogue fillerDialogue;
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
            int buskerQuestProgress = _questManager.GetQuestProgress(BUSKER_QUEST_REF);
            switch (buskerQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(introDialogue, IntroDialogueUpdate);
                    break;
                case 2:
                    _dialogueManager.StartDialogue(succeededDialogue, SucceededDialogueUpdate);
                    break;
                case 3:
                    _dialogueManager.StartDialogue(fillerDialogue, FillerDialogueUpdate);
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
            _questManager.SetQuestProgress(BUSKER_QUEST_REF, 1);
        } catch(Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        
        interactionIndicator.SetAvailable(false);
        _buskerAnimator.SetBool("IsDrumming", true);
    }

    public void SucceededDialogueUpdate()
    {
        try
        {
            _questManager.SetQuestProgress(BUSKER_QUEST_REF, 3);
        } catch (Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        
        interactionIndicator.SetAvailable(true);
        currencyCount.AddCurrency(5);
        _buskerAnimator.SetBool("IsDrumming", true);
    }

    public void FillerDialogueUpdate()
    {
        interactionIndicator.SetAvailable(true);
        _buskerAnimator.SetBool("IsDrumming", true);
    }

    public void Trigger()
    {
        _buskerAnimator.SetBool("IsDrumming", false);
        TriggerDialogue();
    }
}
