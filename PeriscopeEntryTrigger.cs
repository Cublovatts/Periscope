using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PeriscopeEntryTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    private Dialogue _periscopeAvailableDialogue;
    [SerializeField]
    private BlackoutSwitcher _blackoutSwitcher;
    [SerializeField]
    private Animator _playerAnimator;
    [SerializeField]
    private MusicManager _musicManager;

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

    [ContextMenu("Trigger Entry")]
    public void Trigger()
    {
        StartCoroutine(EnterPeriscope());
    }

    IEnumerator EnterPeriscope()
    {
        _blackoutSwitcher.IsBlackedOut = true;
        _playerAnimator.Play("PickUpMid");
        _musicManager.TransitionVolumeDown();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("PeriscopeInterior");
    }
}
