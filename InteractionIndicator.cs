using UnityEngine;

public class InteractionIndicator : MonoBehaviour
{
    public GameObject triggerObject;
    public GameObject indicatorTargetObject;

    private Camera _camera;
    private Transform _interactionIndicatorTransform;
    private GameObject _player;
    private ITrigger _trigger;
    private Animator _indicatorAnimator;

    [SerializeField]
    private Vector3 indicatorOffset;
    private bool isIndicatorSubtle = true;
    public bool isAvailable = true;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _interactionIndicatorTransform = gameObject.transform;
        _player = GameObject.FindGameObjectWithTag("Player");
        _trigger = triggerObject.GetComponent<ITrigger>();
        _indicatorAnimator = gameObject.GetComponent<Animator>();
        if (isAvailable)
        {
            _indicatorAnimator.SetBool("IsAvailable", true);
        } else
        {
            _indicatorAnimator.SetBool("IsAvailable", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update element position
        Vector3 screenPos = _camera.WorldToScreenPoint(indicatorTargetObject.transform.position);
        screenPos += indicatorOffset;
        _interactionIndicatorTransform.SetPositionAndRotation(screenPos, rotation: Quaternion.identity);

        float dist = Vector3.Distance(indicatorTargetObject.transform.position, _player.transform.position);
        if (dist > 5.0f)
        {
            // Show subtle convo prompt
            if (!isIndicatorSubtle)
            {
                isIndicatorSubtle = true;
                _indicatorAnimator.SetBool("IsIndicatorSubtle", true);
            }
        }
        if (dist < 5.0f)
        {
            // Show convo prompt
            if (isIndicatorSubtle)
            {
                isIndicatorSubtle = false;
                _indicatorAnimator.SetBool("IsIndicatorSubtle", false);
            }

            if (Input.GetKeyDown(KeyCode.E) && isAvailable)
            {
                // Trigger action
                _trigger.Trigger();
                isAvailable = false;
                _indicatorAnimator.SetBool("IsAvailable", false);
            }

        }
    }

    public void SetAvailable(bool available)
    {
        isAvailable = available;
        _indicatorAnimator.SetBool("IsAvailable", available);
    }
}
