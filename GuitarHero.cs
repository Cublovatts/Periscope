using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GuitarHero : MonoBehaviour, ITrigger
{
    static private readonly QuestManager.QuestEnum BUSKER_QUEST_REF = QuestManager.QuestEnum.Lord_of_the_dance;

    public InteractionIndicator indicator;
    [SerializeField]
    private Vector3 positionOffset;
    [SerializeField]
    private Quaternion rotationOffset;
    [SerializeField]
    private GameObject finishPosition;

    private QuestManager _questManager;
    private SliderScript _slider;
    private Animator _playerAnimator;
    private GameObject _player;
    private Rigidbody _playerRigidbody;
    private MovementScriptBlock _movement;
    private Image[] _guitarHeroUI;
    private MusicManager _musicManager;
    private AudioSource _audioSource;
    private InteractionIndicator _buskerInteraction;

    private bool onDrums = false;

    void Start()
    {
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _playerAnimator = _player.GetComponent<Animator>();
        _movement = _player.GetComponent<MovementScriptBlock>();
        _guitarHeroUI = GameObject.FindGameObjectsWithTag("GuitarHeroUI").Select(x => x.GetComponent<Image>()).ToArray();
        _slider = gameObject.GetComponentInChildren<SliderScript>();
        _musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _buskerInteraction = GameObject.Find("Busker").GetComponentInChildren<InteractionIndicator>();
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
        // STARTING GUITAR HERO
        // Reveal guitar hero UI elements
        foreach(Image element in _guitarHeroUI)
        {
            Color color= element.color;
            color.a = 1;
            element.color = color;
        }

        // Turn down music, turn up guitar hero song
        _musicManager.SetMinVolume();
        _audioSource.Play();

        _slider.SetPlaying(true);

        yield return new WaitForSeconds(30);

        // FINISHING GUITAR HERO

        foreach (Image element in _guitarHeroUI)
        {
            Color color = element.color;
            color.a = 0;
            element.color = color;
        }

        _buskerInteraction.SetAvailable(true);

        _musicManager.SetMaxVolume();
        _audioSource.Pause();

        _slider.SetPlaying(false);

        onDrums = false;
        _player.transform.position = new Vector3(finishPosition.transform.position.x,
                finishPosition.transform.position.y,
                finishPosition.transform.position.z);

        _playerAnimator.SetBool("IsDrumming", false);
        _movement.IsAvailable = true;
        _playerRigidbody.velocity = Vector3.zero;

        try
        {
            _questManager.SetQuestProgress(BUSKER_QUEST_REF, 2);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Couldn't find quest");
        }
        
    }
}
