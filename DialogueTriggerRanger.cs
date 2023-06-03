using UnityEngine;

public class DialogueTriggerRanger : MonoBehaviour, ITrigger
{
    public InteractionIndicator InteractionIndicator;

    static private readonly QuestManager.QuestEnum RANGER_QUEST_REF = QuestManager.QuestEnum.Pick_up_sticks;

    [SerializeField]
    private Dialogue _introDialogue;
    [SerializeField]
    private Dialogue _questInProgressDialogue;
    [SerializeField]
    private Dialogue _succeededDialogue;
    [SerializeField]
    private Dialogue _fillerDialogue;

    private DialogueManager _dialogueManager;
    private QuestManager _questManager;
    private CurrencyCount _currencyCount;

    void Start()
    {
        _dialogueManager = DialogueManager.instance;
        _questManager = QuestManager.instance;
        _currencyCount = GameObject.Find("CurrencyCount").GetComponent<CurrencyCount>();
    }

    [ContextMenu("TriggerDialogue")]
    public void TriggerDialogue()
    {
        try
        {
            int rangerQuestProgress = _questManager.GetQuestProgress(RANGER_QUEST_REF);
            switch (rangerQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(_introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(_questInProgressDialogue, QuestInProgressUpdate);
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
            _questManager.SetQuestProgress(RANGER_QUEST_REF, 1);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        InteractionIndicator.SetAvailable(true);
    }

    public void QuestInProgressUpdate()
    {
        InteractionIndicator.SetAvailable(true);
    }

    public void SucceededDialogueUpdate()
    {
        try
        {
            _questManager.SetQuestProgress(RANGER_QUEST_REF, 3);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        InteractionIndicator.SetAvailable(true);
        _currencyCount.AddCurrency(5);
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
