using UnityEngine;

public class FootstepSoundSwitcher : MonoBehaviour
{
    [SerializeField]
    private AudioClip grassFootStep;
    [SerializeField]
    private AudioClip concreteFootStep;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Grass")
        {
            _audioSource.clip = grassFootStep;
        } else
        {
            _audioSource.clip = concreteFootStep;
        }
    }
}
