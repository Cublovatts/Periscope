using System.Collections;
using UnityEngine;

public class Branch : MonoBehaviour, ITrigger
{
    private const QuestManager.QuestEnum RANGER_QUEST_REF = QuestManager.QuestEnum.Pick_up_sticks;

    [SerializeField]
    private InteractionIndicator _interactionIndicator;
    private RangerQuestTracker _rangerQuestTracker;
    private QuestManager _questManager;
    private Animator _animator;
    private Animator _playerAnimator;
    private MovementScriptBlock _movementScriptBlock;

    void Start()
    {
        _rangerQuestTracker = GameObject.FindGameObjectWithTag("RangerQuestTracker").GetComponent<RangerQuestTracker>();
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _animator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _movementScriptBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScriptBlock>();
    }

    public void Update()
    {
        try
        {
            int questProgress = _questManager.GetQuestProgress(RANGER_QUEST_REF);
            if (questProgress == 1)
            {
                _interactionIndicator.SetAvailable(true);
            }
            else
            {
                _interactionIndicator.SetAvailable(false);
            }
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
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
        _movementScriptBlock.IsAvailable = false;
        _playerAnimator.Play("PickUp");
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("IsDisappearing", true);
        yield return new WaitForSeconds(0.5f);
        _movementScriptBlock.IsAvailable = true;
        gameObject.SetActive(false);
        
    }
}
