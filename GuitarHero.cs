using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GuitarHero : MonoBehaviour, ITrigger
{
    static private QuestManager.QuestEnum BUSKER_QUEST_REF = QuestManager.QuestEnum.Lord_of_the_dance;

    public InteractionIndicator indicator;
    [SerializeField]
    private Vector3 positionOffset;
    [SerializeField]
    private Quaternion rotationOffset;
    [SerializeField]
    private GameObject finishPosition;

    private QuestManager _questManager;
    private Animator _playerAnimator;
    private GameObject _player;
    private Rigidbody _playerRigidbody;
    private MovementScriptBlock _movement;
    private Image[] _guitarHeroUI;

    private bool onDrums = false;

    void Start()
    {
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _playerAnimator = _player.GetComponent<Animator>();
        _movement = _player.GetComponent<MovementScriptBlock>();
        _guitarHeroUI = GameObject.FindGameObjectsWithTag("GuitarHeroUI").Select(x => x.GetComponent<Image>()).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (_questManager.GetQuestProgress(BUSKER_QUEST_REF) == 1 && !onDrums)
        {
            indicator.SetAvailable(true);
        } else
        {
            indicator.SetAvailable(false);
        }

        if (onDrums)
        {
            // Put player on drum box
            _player.transform.position = new Vector3(gameObject.transform.position.x + positionOffset.x, 
                gameObject.transform.position.y + positionOffset.y, 
                gameObject.transform.position.z + positionOffset.z);
            _player.transform.rotation = rotationOffset;

            if (Input.GetKeyDown(KeyCode.A))
            {
                _playerAnimator.Play("Drum_Right");
                //Process input for guitar hero game
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _playerAnimator.Play("Drum_Left");
                //Process input for guitar hero game
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
        StartCoroutine(PlayGuitarHero());
    }

    public IEnumerator PlayGuitarHero()
    {
        // Reveal guitar hero UI elements
        foreach(Image element in _guitarHeroUI)
        {
            Color color= element.color;
            color.a = 1;
            element.color = color;
        }

        yield return new WaitForSeconds(5);

        foreach (Image element in _guitarHeroUI)
        {
            Color color = element.color;
            color.a = 0;
            element.color = color;
        }

        onDrums = false;
        _player.transform.position = new Vector3(finishPosition.transform.position.x,
                finishPosition.transform.position.y,
                finishPosition.transform.position.z);

        _playerAnimator.SetBool("IsDrumming", false);
        _movement.IsAvailable = true;
        _playerRigidbody.velocity = Vector3.zero;
    }
}
