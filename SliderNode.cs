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
                SetToFail();
            }
            if (distance < _passDistance && _isLeftNode)
            {
                SetToPass();
            }
            if (distance < _passDistance && !_isLeftNode)
            {
                SetToFail();
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !_isSet)
        {
            // Check proximity
            float distance = Vector2.Distance(transform.position, _middleBar.transform.position);
            if (distance < _inPlayDistance && distance > _passDistance)
            {
                SetToFail();
            }
            if (distance < _passDistance && !_isLeftNode)
            {
                SetToPass();
            }
            if (distance < _passDistance && _isLeftNode)
            {
                SetToFail();
            }
        }
    }

    private void SetToPass()
    {
        _isSet = true;
        _nodeImage.color = Color.green;
    }

    private void SetToFail()
    {
        _isSet = true;
        _nodeImage.color = Color.red;
    }
}
