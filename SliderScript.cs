using UnityEngine;

public class SliderScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool isPlaying = false;
    private float resolutionSpeedFactor;

    void Update()
    {
        resolutionSpeedFactor = Screen.height / 1080f;

        if (isPlaying)
        {
            transform.position = transform.position + (Vector3.down * (speed * resolutionSpeedFactor * Time.deltaTime));
        }
    }

    public void SetPlaying(bool playing)
    {
        isPlaying = playing;
    }
}
