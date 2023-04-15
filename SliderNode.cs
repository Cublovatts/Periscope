using UnityEngine;
using UnityEngine.UI;

public class SliderNode : MonoBehaviour
{
    [SerializeField]
    private bool isLeftNode;
    private bool isSet = false;

    private GameObject _middleBar;
    private Image _nodeImage;

    private float resolutionDistanceFactor;
    private float inPlayDistance;
    private float passDistance;

    void Start()
    {
        _middleBar = GameObject.Find("MiddleBar");
        _nodeImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        resolutionDistanceFactor = Screen.height / 1080f;
        inPlayDistance = 200f * resolutionDistanceFactor;
        passDistance = 100f * resolutionDistanceFactor;

        if (Input.GetKeyDown(KeyCode.A) && !isSet) {
            // Check proximity
            float distance = Vector2.Distance(transform.position, _middleBar.transform.position);
            if (distance < inPlayDistance && distance > passDistance)
            {
                setToFail();
            }
            if (distance < passDistance && isLeftNode)
            {
                setToPass();
            }
            if (distance < passDistance && !isLeftNode)
            {
                setToFail();
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !isSet)
        {
            // Check proximity
            float distance = Vector2.Distance(transform.position, _middleBar.transform.position);
            if (distance < inPlayDistance && distance > passDistance)
            {
                setToFail();
            }
            if (distance < passDistance && !isLeftNode)
            {
                setToPass();
            }
            if (distance < passDistance && isLeftNode)
            {
                setToFail();
            }
        }
    }

    void setToPass()
    {
        isSet = true;
        _nodeImage.color = Color.green;
    }

    void setToFail()
    {
        isSet = true;
        _nodeImage.color = Color.red;
    }
}
