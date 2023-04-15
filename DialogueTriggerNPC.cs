using UnityEngine;

public class DialogueTriggerNPC : MonoBehaviour, ITrigger
{
    [SerializeField]
    private Dialogue dialogue;

    public InteractionIndicator interactionIndicator;

    private DialogueManager _dialogueManager;

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    public void Trigger()
    {
        _dialogueManager.StartDialogue(dialogue, RepeatDialogue);
    }

    public void RepeatDialogue()
    {
        interactionIndicator.SetAvailable(true);
    }
}
