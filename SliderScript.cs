using UnityEngine;

public class SliderScript : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private bool _isPlaying = false;
    private float _resolutionSpeedFactor;

    void Update()
    {
        _resolutionSpeedFactor = Screen.height / 1080f;

        if (_isPlaying)
        {
            transform.position = transform.position + (Vector3.down * (_speed * _resolutionSpeedFactor * Time.deltaTime));
        }
    }

    public void SetPlaying(bool playing)
    {
        _isPlaying = playing;
    }
}
