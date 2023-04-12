using UnityEngine;

public class PeriscopeEntryTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    private Dialogue periscopeAvailableDialogue;

    private InteractionIndicator _interactionIndicator;
    private DialogueManager _dialogueManager;
    private CurrencyCount _currencyCount;

    private bool isTriggered = false;
    void Start()
    {
        _interactionIndicator = GetComponentInChildren<InteractionIndicator>();
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _currencyCount = GameObject.Find("Currency Count").GetComponent<CurrencyCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currencyCount.GetCurrency() >= 20 && !isTriggered)
        {
            _dialogueManager.StartDialogue(periscopeAvailableDialogue, null);
            _interactionIndicator.SetAvailable(true);
            isTriggered = true;
        }
    }

    public void Trigger()
    {
        // Open periscope!
    }
}
