using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private Dialogue introDialogue;

    private DialogueManager _dialogueManager;
    private PauseManager _pauseManager;

    private bool isSent = false;

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseManager>();
        PauseManager.onUnpause += OnUnpauseCheck;
    }

    public void OnUnpauseCheck()
    {
        if (!isSent && !_pauseManager.GetPaused())
        {
            _dialogueManager.StartDialogue(introDialogue, null);
            isSent = true;
        }
    }
}
