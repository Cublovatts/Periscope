using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueTriggerBusker : MonoBehaviour, ITrigger
{
    public InteractionIndicator InteractionIndicator;

    static private readonly QuestManager.QuestEnum BUSKER_QUEST_REF = QuestManager.QuestEnum.Lord_of_the_dance;

    [SerializeField]
    private Dialogue _introDialogue;
    [SerializeField]
    private Dialogue _succeededDialogue;
    [SerializeField]
    private Dialogue _fillerDialogue;  

    private DialogueManager _dialogueManager;
    private QuestManager _questManager;
    private CurrencyCount _currencyCount;
    private Animator _buskerAnimator;

    private void Start()
    {
        _dialogueManager = DialogueManager.instance;
        _questManager = QuestManager.instance;
        _currencyCount = GameObject.Find("CurrencyCount").GetComponent<CurrencyCount>();
        _buskerAnimator = GameObject.Find("BuskerCharacter").GetComponent<Animator>();
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
                    _dialogueManager.StartDialogue(_introDialogue, IntroDialogueUpdate);
                    break;
                case 2:
                    _dialogueManager.StartDialogue(_succeededDialogue, SucceededDialogueUpdate);
                    break;
                case 3:
                    _dialogueManager.StartDialogue(_fillerDialogue, FillerDialogueUpdate);
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
