using UnityEngine;
using UnityEngine.Serialization;

public class DialogueTriggerWaiter : MonoBehaviour, ITrigger
{
    public InteractionIndicator interactionIndicator;

    static private readonly QuestManager.QuestEnum RESTAURANT_QUEST_REF = QuestManager.QuestEnum.Turn_the_tables;

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
    private PlateSpawner _spawner;
    private CurrencyCount _currencyCount;

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _spawner = GameObject.Find("PlateSpawner").GetComponent<PlateSpawner>();
        _currencyCount = GameObject.Find("CurrencyCount").GetComponent<CurrencyCount>();
    }

    [ContextMenu("TriggerDialogue")]
    public void TriggerDialogue()
    {
        try
        {
            int waiterQuestProgress = _questManager.GetQuestProgress(RESTAURANT_QUEST_REF);
            switch (waiterQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(_introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(_questInProgressDialogue, QuestInProgressDialogueUpdate);
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
        _questManager.SetQuestProgress(RESTAURANT_QUEST_REF, 1);
        interactionIndicator.SetAvailable(true);
        _spawner.SpawnPlate();
    }

    public void QuestInProgressDialogueUpdate()
    {
        interactionIndicator.SetAvailable(true);
    }

    public void SucceededDialogueUpdate()
    {
        _questManager.SetQuestProgress(RESTAURANT_QUEST_REF, 3);
        interactionIndicator.SetAvailable(true);
        _currencyCount.AddCurrency(5);
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
