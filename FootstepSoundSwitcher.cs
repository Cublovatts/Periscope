using UnityEngine;

public class FootstepSoundSwitcher : MonoBehaviour
{
    [SerializeField]
    private AudioClip _grassFootStep;
    [SerializeField]
    private AudioClip _concreteFootStep;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Grass")
        {
            _audioSource.clip = _grassFootStep;
        } else
        {
            _audioSource.clip = _concreteFootStep;
        }
    }
}
