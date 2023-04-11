using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    static private QuestManager.QuestEnum CAT_QUEST_REF = QuestManager.QuestEnum.MOGGY;

    [SerializeField]
    private List<GameObject> destinations;
    [SerializeField]
    private float RUNNING_SPEED = 2.0f;

    private QuestManager _questManager;
    private GameObject _catMesh;
    private Animator _catAnimator;

    private int destinationNumber = 0;
    private bool movementFinished = false;

    void Start()
    {
        _questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        _catMesh = gameObject.transform.GetChild(1).gameObject;
        _catAnimator = GetComponent<Animator>();
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
            if (!movementFinished)
            {
                GameObject currentDestination = destinations[destinationNumber];
                var step = RUNNING_SPEED * Time.deltaTime;
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
                    destinationNumber++;
                    if (destinationNumber > destinations.Count - 1)
                    {
                        movementFinished = true;
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
