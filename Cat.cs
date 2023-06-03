using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cat : MonoBehaviour
{
    static private QuestManager.QuestEnum CAT_QUEST_REF = QuestManager.QuestEnum.MOGGY;

    [SerializeField]
    private List<GameObject> _destinations;
    [SerializeField]
    private float _runningSpeed = 2.0f;

    private QuestManager _questManager;
    [SerializeField]
    private GameObject _catMesh;
    private Animator _catAnimator;

    private int _destinationNumber = 0;
    private bool _movementFinished = false;

    private void Awake()
    {
        _catAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        _questManager = QuestManager.instance;
    }

    void Update()
    {
        if(_questManager.GetQuestProgress(CAT_QUEST_REF) == 0)
        {
            // hide cat
            _catMesh.SetActive(false);
        }
        if (_questManager.GetQuestProgress(CAT_QUEST_REF) == 1)
        {
            // show as visible
            _catMesh.SetActive(true);
        } 
        if (_questManager.GetQuestProgress(CAT_QUEST_REF) == 2)
        {
            // Run home and curl up by feet
            _catAnimator.SetBool("IsRunning", true);

            //Loop through locations
            if (!_movementFinished)
            {
                GameObject currentDestination = _destinations[_destinationNumber];
                var step = _runningSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, currentDestination.transform.position) > 0.001f)
                {
                    Vector3 newPos = Vector3.MoveTowards(transform.position, currentDestination.transform.position, step);
                    Vector3 inputMovement = newPos - transform.position;
                    if (inputMovement != Vector3.zero)
                    {
                        transform.right = -inputMovement;
                    }
                    transform.position = newPos;
                }

                // Is destination reached
                if (Vector3.Distance(transform.position, currentDestination.transform.position) < 0.001f)
                {
                    _destinationNumber++;
                    if (_destinationNumber > _destinations.Count - 1)
                    {
                        _movementFinished = true;
                    }
                }
            } else
            {
                // curl up by feet
                _catAnimator.SetBool("IsLying", true);
            }
        }
    }
}
