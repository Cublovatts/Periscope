using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] tracks;
    [SerializeField]
    private float secondsBetween;
    [SerializeField]
    private float maxVolume;

    private AudioSource _audioSource;
    private float lastTrackStarted = 0;
    private int currentTrack = 0;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = tracks[1];
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - secondsBetween > lastTrackStarted)
        {
            // Play current track and increment/loop track number
            _audioSource.clip = tracks[currentTrack];
            _audioSource.Play();
            lastTrackStarted = Time.time;
            currentTrack++;
            if (currentTrack >= tracks.Length)
            {
                currentTrack = 0;
            }
        }
    }

    public void SetMaxVolume()
    {
        _audioSource.volume = maxVolume;
    }

    public void SetMinVolume()
    {
        _audioSource.volume = 0;
    }
}
