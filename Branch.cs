using System.Collections;
using UnityEngine;

public class Branch : MonoBehaviour, ITrigger
{
    public InteractionIndicator interactionIndicator;

    private RangerQuestTracker _rangerQuestTracker;
    private QuestManager _questManager;
    private Animator _animator;
    private Animator _playerAnimator;
    private MovementScriptBlock movementScriptBlock;

    private Vector3 startPosition;

    void Start()
    {
        _rangerQuestTracker = GameObject.FindGameObjectWithTag("RangerQuestTracker").GetComponent<RangerQuestTracker>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _animator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        movementScriptBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScriptBlock>();
    }

    public void Update()
    {
        int questProgress = _questManager.GetQuestProgress("Pick up sticks");
        if (questProgress == 1)
        {
            interactionIndicator.SetAvailable(true);
        } else
        {
            interactionIndicator.SetAvailable(false);
        }
    }

    [ContextMenu("Disappear Stick")]
    public void Trigger()
    {
        _rangerQuestTracker.AddStick();
        // Disable stick
        StartCoroutine(DisappearStick());
        // Trigger main character pick up stick animation
    }

    IEnumerator DisappearStick()
    {
        movementScriptBlock.IsAvailable = false;
        _playerAnimator.Play("PickUp");
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("IsDisappearing", true);
        yield return new WaitForSeconds(0.5f);
        movementScriptBlock.IsAvailable = true;
        gameObject.SetActive(false);
        
    }
}
