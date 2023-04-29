using UnityEngine;
using UnityEngine.UI;

public class SliderNode : MonoBehaviour
{
    [SerializeField]
    private bool _isLeftNode;
    private bool _isSet = false;

    private GameObject _middleBar;
    private Image _nodeImage;

    private float _resolutionDistanceFactor;
    private float _inPlayDistance;
    private float _passDistance;

    void Start()
    {
        _middleBar = GameObject.Find("MiddleBar");
        _nodeImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _resolutionDistanceFactor = Screen.height / 1080f;
        _inPlayDistance = 200f * _resolutionDistanceFactor;
        _passDistance = 100f * _resolutionDistanceFactor;

        if (Input.GetKeyDown(KeyCode.A) && !_isSet) {
            // Check proximity
            float distance = Vector2.Distance(transform.position, _middleBar.transform.position);
            if (distance < _inPlayDistance && distance > _passDistance)
            {
                setToFail();
            }
            if (distance < _passDistance && _isLeftNode)
            {
                setToPass();
            }
            if (distance < _passDistance && !_isLeftNode)
            {
                setToFail();
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !_isSet)
        {
            // Check proximity
            float distance = Vector2.Distance(transform.position, _middleBar.transform.position);
            if (distance < _inPlayDistance && distance > _passDistance)
            {
                setToFail();
            }
            if (distance < _passDistance && !_isLeftNode)
            {
                setToPass();
            }
            if (distance < _passDistance && _isLeftNode)
            {
                setToFail();
            }
        }
    }

    void setToPass()
    {
        _isSet = true;
        _nodeImage.color = Color.green;
    }

    void setToFail()
    {
        _isSet = true;
        _nodeImage.color = Color.red;
    }
}
