using UnityEngine;

public class SliderScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool isPlaying = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isPlaying)
        {
            transform.position = transform.position + (Vector3.down * (speed * Time.deltaTime));
        }
    }

    public void SetPlaying(bool playing)
    {
        isPlaying = playing;
    }
}
