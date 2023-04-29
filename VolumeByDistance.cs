using UnityEngine;

public class VolumeByDistance : MonoBehaviour
{
    [SerializeField]
    private float _maxVolume;
    [SerializeField]
    private float _minVolume;
    [SerializeField]
    private float _minDistance;
    [SerializeField]
    private float _maxDistance;

    private AudioSource _audioSource;
    private GameObject _player;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, _player.transform.position);

        if (distance > _maxDistance)
        {
            _audioSource.volume = _minVolume;
        } 
        else if (distance < _minDistance)
        {
            _audioSource.volume = _maxVolume;
        } 
        else
        {
            _audioSource.volume = Mathf.Lerp(_maxVolume, _minVolume, (distance - _minDistance) / (_maxDistance - _minDistance));
        }
    }
}
