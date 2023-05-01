using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 1.0f;
    public bool isAvailable = true;

    [SerializeField]
    private GameObject _player;
    [FormerlySerializedAs("m_cameraOffset")]
    [SerializeField]
    private Vector3 _cameraOffset;

    private GameObject _trackingObject;
    
    private void Start()
    {
        _trackingObject = _player;
    }

    void Update()
    {
        if (isAvailable)
        {
            transform.position = GetTargetObjectPosition();
        }
    }

    public Vector3 GetTargetObjectPosition()
    {
        return _trackingObject.transform.position + _cameraOffset;
    }

    public IEnumerator MoveCamera(Vector3 targetPosition, float targetSize, float duration)
    {
        Vector3 startPosition = transform.position;
        float startSize = Camera.main.orthographicSize;
        AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float curveValue = curve.Evaluate(t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, curveValue);
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, curveValue);

            yield return null;
        }

        transform.position = targetPosition;
        Camera.main.orthographicSize = targetSize;
    }

    public IEnumerator MoveCameraTracking(GameObject targetObject, float targetSize, float duration)
    {
        _trackingObject = targetObject;
        Vector3 startPosition = transform.position;
        float startSize = Camera.main.orthographicSize;
        AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float curveValue = curve.Evaluate(t);

            transform.position = Vector3.Lerp(startPosition, GetTargetObjectPosition(), curveValue);
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, curveValue);

            yield return null;
        }

        transform.position = GetTargetObjectPosition();
        Camera.main.orthographicSize = targetSize;
        isAvailable = true;
    }
}
