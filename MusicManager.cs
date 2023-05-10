using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _tracks;
    [SerializeField]
    private float _secondsBetween;
    [SerializeField]
    private float _maxVolume;

    private AudioSource _audioSource;
    private float _lastTrackStarted = 0;
    private int _currentTrack = 0;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
    }

    void Update()
    {
        if (Time.time - _secondsBetween > _lastTrackStarted)
        {
            PlayNextTrack();
            if (_currentTrack >= _tracks.Length)
            {
                _currentTrack = 0;
            }
        }
    }

    public void SetMaxVolume()
    {
        _audioSource.volume = _maxVolume;
    }

    public void SetMinVolume()
    {
        _audioSource.volume = 0;
    }

    private void PlayNextTrack()
    {
        _audioSource.clip = _tracks[_currentTrack];
        _audioSource.Play();
        _lastTrackStarted = Time.time;
        _currentTrack++;
    }
}
