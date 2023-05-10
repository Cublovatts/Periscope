using UnityEngine;

public class PeriscopeEntryTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    private Dialogue _periscopeAvailableDialogue;

    private InteractionIndicator _interactionIndicator;
    private DialogueManager _dialogueManager;
    private CurrencyCount _currencyCount;

    private bool _isTriggered = false;

    void Start()
    {
        _interactionIndicator = GetComponentInChildren<InteractionIndicator>();
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _currencyCount = GameObject.Find("CurrencyCount").GetComponent<CurrencyCount>();
    }

    void Update()
    {
        if (_currencyCount.GetCurrency() >= 20 && !_isTriggered)
        {
            _dialogueManager.StartDialogue(_periscopeAvailableDialogue, null);
            _interactionIndicator.SetAvailable(true);
            _isTriggered = true;
        }
    }

    public void Trigger()
    {
        // Open periscope!
    }
}
