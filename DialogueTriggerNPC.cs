using UnityEngine;

public class DialogueTriggerNPC : MonoBehaviour, ITrigger
{
    public InteractionIndicator interactionIndicator;

    [SerializeField]
    private Dialogue _dialogue;

    private DialogueManager _dialogueManager;

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    public void Trigger()
    {
        _dialogueManager.StartDialogue(_dialogue, RepeatDialogue);
    }

    public void RepeatDialogue()
    {
        interactionIndicator.SetAvailable(true);
    }
}
