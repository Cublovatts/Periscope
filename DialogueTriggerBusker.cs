using System;
using UnityEngine;

public class DialogueTriggerBusker : MonoBehaviour, ITrigger
{
    public Dialogue IntroDialogue;
    public Dialogue SucceededDialogue;
    public Dialogue FillerDialogue;
    public InteractionIndicator InteractionIndicator;

    [SerializeField]
    private Animator _buskerAnimator;

    static private readonly QuestManager.QuestEnum BUSKER_QUEST_REF = QuestManager.QuestEnum.Lord_of_the_dance;

    private DialogueManager _dialogueManager;
    private QuestManager _questManager;
    private CurrencyCount _currencyCount;

    private void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _currencyCount = GameObject.Find("CurrencyCount").GetComponent<CurrencyCount>();
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
                    _dialogueManager.StartDialogue(IntroDialogue, IntroDialogueUpdate);
                    break;
                case 2:
                    _dialogueManager.StartDialogue(SucceededDialogue, SucceededDialogueUpdate);
                    break;
                case 3:
                    _dialogueManager.StartDialogue(FillerDialogue, FillerDialogueUpdate);
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
        
        InteractionIndicator.SetAvailable(false);
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
        
        InteractionIndicator.SetAvailable(true);
        _currencyCount.AddCurrency(5);
        _buskerAnimator.SetBool("IsDrumming", true);
    }

    public void FillerDialogueUpdate()
    {
        InteractionIndicator.SetAvailable(true);
        _buskerAnimator.SetBool("IsDrumming", true);
    }

    public void Trigger()
    {
        _buskerAnimator.SetBool("IsDrumming", false);
        TriggerDialogue();
    }
}
