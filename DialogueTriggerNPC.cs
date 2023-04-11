using UnityEngine;

public class DialogueTriggerNPC : MonoBehaviour, ITrigger
{
    [SerializeField]
    private Dialogue dialogue;

    private DialogueManager _dialogueManager;

    void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    public void Trigger()
    {
        _dialogueManager.StartDialogue(dialogue, null);
    }
}
