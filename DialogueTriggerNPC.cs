using UnityEngine;

public class DialogueTriggerNPC : MonoBehaviour, ITrigger
{
    public InteractionIndicator InteractionIndicator;

    [SerializeField]
    private Dialogue _dialogue;

    private DialogueManager _dialogueManager;

    void Start()
    {
        _dialogueManager = DialogueManager.instance;
    }

    public void Trigger()
    {
        _dialogueManager.StartDialogue(_dialogue, RepeatDialogue);
    }

    public void RepeatDialogue()
    {
        InteractionIndicator.SetAvailable(true);
    }
}
