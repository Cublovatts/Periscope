using UnityEngine;
using UnityEngine.Serialization;

public class InteractionIndicator : MonoBehaviour
{
    [FormerlySerializedAs("triggerObject")]
    public GameObject TriggerObject;
    [FormerlySerializedAs("indicatorTargetObject")]
    public GameObject IndicatorTargetObject;
    
    private Camera _camera;
    private Transform _interactionIndicatorTransform;
    private GameObject _player;
    private ITrigger _trigger;
    private Animator _indicatorAnimator;

    [SerializeField]
    private Vector3 _indicatorOffset;
    private bool _isIndicatorSubtle = true;
    [SerializeField]
    private bool _isAvailable = true;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _interactionIndicatorTransform = gameObject.transform;
        _player = GameObject.FindGameObjectWithTag("Player");
        _trigger = TriggerObject.GetComponent<ITrigger>();
        _indicatorAnimator = gameObject.GetComponent<Animator>();
        if (_isAvailable)
        {
            _indicatorAnimator.SetBool("IsAvailable", true);
        } else
        {
            _indicatorAnimator.SetBool("IsAvailable", false);
        }
    }

    void Update()
    {
        Vector3 screenPos = _camera.WorldToScreenPoint(IndicatorTargetObject.transform.position + _indicatorOffset);
        _interactionIndicatorTransform.SetPositionAndRotation(screenPos, rotation: Quaternion.identity);

        SetAvailable(_isAvailable);

        float dist = Vector3.Distance(IndicatorTargetObject.transform.position, _player.transform.position);
        if (dist > 5.0f)
        {
            // Show subtle convo prompt
            if (!_isIndicatorSubtle)
            {
                _isIndicatorSubtle = true;
                _indicatorAnimator.SetBool("IsIndicatorSubtle", true);
            }
        }
        if (dist < 5.0f)
        {
            // Show convo prompt
            if (_isIndicatorSubtle)
            {
                _isIndicatorSubtle = false;
                _indicatorAnimator.SetBool("IsIndicatorSubtle", false);
            }

            if (Input.GetKeyDown(KeyCode.E) && _isAvailable)
            {
                // Trigger action
                _trigger.Trigger();
                _isAvailable = false;
                _indicatorAnimator.SetBool("IsAvailable", false);
            }

        }
    }

    public void SetAvailable(bool available)
    {
        _isAvailable = available;
        _indicatorAnimator.SetBool("IsAvailable", available);
    }
}
