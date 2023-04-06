using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseManager : MonoBehaviour
{
    public VolumeProfile mVolumeProfile;

    static private float playCameraSize = 12f;
    static private float pauseCameraSize = 50f;
    static private Vector3 pausePos = new Vector3(98f, 99f, -175f);

    private MovementScriptBlock _movement;
    private FollowPlayer _followPlayer;
    private GameObject _camera;
    private GameObject _player;
    private GameObject _pauseCanvas;
    private Vignette _vignette;
    private ColorAdjustments _colorAdjustments;
    private InteractionIndicator[] _interactionIndicators;

    private bool isPaused = false;
    private float speed = 2.0f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _movement = _player.GetComponent<MovementScriptBlock>();
        _followPlayer = _camera.GetComponent<FollowPlayer>();
        _pauseCanvas = GameObject.Find("PauseCanvas");
        _interactionIndicators = GameObject.FindObjectsOfType<InteractionIndicator>();

        // get the vignette effect
        for (int i = 0; i < mVolumeProfile.components.Count; i++)
        {
            if (mVolumeProfile.components[i].name == "Vignette")
            {
                _vignette = (Vignette)mVolumeProfile.components[i];
            }
            if (mVolumeProfile.components[i].name == "ColorAdjustments")
            {
                _colorAdjustments = (ColorAdjustments)mVolumeProfile.components[i];
            }
        }

        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                UnPause();
            } else
            {
                isPaused = true;
                Pause();
            }
        }
    }

    public void Pause()
    {
        // disable player movement and camera tracking
        _followPlayer.isAvailable = false;
        _movement.IsAvailable = false;

        // stop previous animation and start new one
        StopAllCoroutines();
        StartCoroutine(_followPlayer.MoveCamera(pausePos, pauseCameraSize, 3.0f));

        // set pause menu effects
        ClampedFloatParameter intensity = _vignette.intensity;
        intensity.value = 0.3f;
        FloatParameter colorAdjustment = _colorAdjustments.postExposure;
        colorAdjustment.value = -2.5f;

        // hide all interaction indicators
        foreach(InteractionIndicator indicator in _interactionIndicators) 
        {
            indicator.gameObject.SetActive(false);
        }

        // reveal pause elements
        _pauseCanvas.SetActive(true);
    }

    public void UnPause()
    {
        // enable player movement
        _movement.IsAvailable = true;

        // stop previous animation and start new one
        StopAllCoroutines();
        StartCoroutine(_followPlayer.MoveCameraTracking(_player, playCameraSize, 3.0f));

        // set pause menu effects
        ClampedFloatParameter intensity = _vignette.intensity;
        intensity.value = 0f;
        FloatParameter colorAdjustment = _colorAdjustments.postExposure;
        colorAdjustment.value = 0f;

        // hide all interaction indicators
        foreach (InteractionIndicator indicator in _interactionIndicators)
        {
            indicator.gameObject.SetActive(true);
        }

        // hide pause elements
        _pauseCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
