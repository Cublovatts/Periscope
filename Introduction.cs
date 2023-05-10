using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private Dialogue _introDialogue;

    private DialogueManager _dialogueManager;
    private PauseManager _pauseManager;
    private QuestTrackerUI _questTrackerUI;

    private bool _isSent = false;

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseManager>();
        _questTrackerUI = GameObject.Find("QuestTrackers").GetComponent<QuestTrackerUI>();
        PauseManager.onUnpause += OnUnpauseCheck;
    }

    public void OnUnpauseCheck()
    {
        if (!_isSent && !_pauseManager.GetPaused())
        {
            _dialogueManager.StartDialogue(_introDialogue, _questTrackerUI.ShowQuestTrackers);
            _isSent = true;
        }
    }
}
