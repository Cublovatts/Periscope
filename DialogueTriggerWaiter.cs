using UnityEngine;

public class DialogueTriggerWaiter : MonoBehaviour, ITrigger
{
    static private readonly QuestManager.QuestEnum RESTAURANT_QUEST_REF = QuestManager.QuestEnum.Turn_the_tables;

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
        try
        {
            int waiterQuestProgress = _questManager.GetQuestProgress(RESTAURANT_QUEST_REF);
            switch (waiterQuestProgress)
            {
                case 0:
                    _dialogueManager.StartDialogue(introDialogue, IntroDialogueUpdate);
                    break;
                case 1:
                    _dialogueManager.StartDialogue(questInProgressDialogue, QuestInProgressDialogueUpdate);
                    break;
                case 2:
                    _dialogueManager.StartDialogue(succeededDialogue, SucceededDialogueUpdate);
                    break;
                case 3:
                    _dialogueManager.StartDialogue(fillerDialogue, FillerDialogueUpdate);
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
        currencyCount.AddCurrency(5);
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
