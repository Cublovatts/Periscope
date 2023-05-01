using UnityEngine;

public class DialogueTriggerCatLady : MonoBehaviour, ITrigger
{
    public InteractionIndicator InteractionIndicator;

    [SerializeField]
    private Dialogue _introDialogue;
    [SerializeField]
    private Dialogue _inProgressDialogue;
    [SerializeField]
    private Dialogue _succeededDialogue;
    [SerializeField]
    private Dialogue _fillerDialogue;

    static private readonly QuestManager.QuestEnum CAT_QUEST_REF = QuestManager.QuestEnum.MOGGY;

    private DialogueManager _dialogueManager;
    private QuestManager _questManager;
    private CurrencyCount _currencyCount;

    void Start()
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
            int catLadyQuestProgress = _questManager.GetQuestProgress(CAT_QUEST_REF);
            switch (catLadyQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(_introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(_inProgressDialogue, InProgressDialogueUpdate);
                    break;
                case 2:
                    _dialogueManager.StartDialogue(_succeededDialogue, SucceededDialogueUpdate);
                    break;
                case 3:
                    _dialogueManager.StartDialogue(_fillerDialogue, FillerDialogueUpdate);
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
        InteractionIndicator.SetAvailable(true);
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
         
        InteractionIndicator.SetAvailable(true);
        _currencyCount.AddCurrency(5);
    }

    public void InProgressDialogueUpdate()
    {
        InteractionIndicator.SetAvailable(true);
    }

    public void FillerDialogueUpdate()
    {
        InteractionIndicator.SetAvailable(true);
    }

    public void Trigger()
    {
        TriggerDialogue();
    }
}
