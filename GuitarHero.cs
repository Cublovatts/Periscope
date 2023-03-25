using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarHero : MonoBehaviour, ITrigger
{
    static private QuestManager.QuestEnum BUSKER_QUEST_REF = QuestManager.QuestEnum.Lord_of_the_dance;

    public InteractionIndicator indicator;
    [SerializeField]
    private Vector3 positionOffset;
    [SerializeField]
    private Quaternion rotationOffset;

    private QuestManager _questManager;
    private Animator _playerAnimator;
    private GameObject _player;
    private MovementScriptBlock _movement;

    private bool onDrums = false;

    void Start()
    {
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerAnimator = _player.GetComponent<Animator>();
        _movement = _player.GetComponent<MovementScriptBlock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_questManager.GetQuestProgress(BUSKER_QUEST_REF) == 1 && !onDrums)
        {
            indicator.SetAvailable(true);
        }

        if (onDrums)
        {
            // take drum input and play animation for each arm
            //_player.transform.position = gameObject.transform.position;
            _player.transform.position = new Vector3(gameObject.transform.position.x + positionOffset.x, 
                gameObject.transform.position.y + positionOffset.y, 
                gameObject.transform.position.z + positionOffset.z);
            _player.transform.rotation = rotationOffset;

            if (Input.GetKeyDown(KeyCode.A))
            {
                _playerAnimator.Play("Drum_Right");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _playerAnimator.Play("Drum_Left");
            }
        }
    }

    public void Trigger()
    {
        onDrums = true;
        // Disable player movement
        _movement.IsAvailable = false;
        // Set animation to drum idle
        _playerAnimator.SetBool("IsDrumming", true);
        _playerAnimator.Play("Drum_Idle");
    }
}
