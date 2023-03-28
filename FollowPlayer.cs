using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 1.0f;

    [SerializeField]private GameObject m_Player;
    [SerializeField]private Vector3 m_cameraOffset;

    private GameObject trackingObject;
    public bool isAvailable = true;

    private void Start()
    {
        trackingObject = m_Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAvailable)
        {
            transform.position = GetTargetObjectPosition();
        }
    }

    public Vector3 GetTargetObjectPosition()
    {
        return trackingObject.transform.position + m_cameraOffset;
    }

    public IEnumerator MoveCamera(Vector3 targetPosition, float targetSize, float duration)
    {
        Vector3 startPosition = transform.position;
        float startSize = Camera.main.orthographicSize;
        AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Define a smooth animation curve
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
        trackingObject = targetObject;
        Vector3 startPosition = transform.position;
        float startSize = Camera.main.orthographicSize;
        AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Define a smooth animation curve
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
