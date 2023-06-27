using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Periscope : MonoBehaviour, ITrigger
{
    [SerializeField]
    private GameObject _callToAction;
    [SerializeField]
    private GameObject _inspiredBy;
    [SerializeField]
    private GameObject _madeBy;
    [SerializeField]
    private GameObject _dedication;
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private Image _blackoutPanel;
    [SerializeField]
    private Image _periscopePanel;
    [SerializeField]
    private GameObject _animationSpot;
    [SerializeField]
    private GameObject _player;
    private Rigidbody _playerRigidBody;
    private Animator _playerAnimator;
    private MovementScriptBlock _playerMovementScript;

    private bool _movePlayer = false;
    private bool _isBlackedOut = false;
    private bool _isPeriscopeView = false;
    private bool _isMusicLoud = true;

    [ContextMenu("Trigger Ending")]
    public void Trigger()
    {
        _playerAnimator.Play("PeriscopeLook");
        StartCoroutine(MoveToAnimationSpot());
        StartCoroutine(DisplayCredits());
    }

    void Start()
    {
        _playerRigidBody = _player.GetComponent<Rigidbody>();
        _playerAnimator = _player.GetComponent<Animator>();
        _playerMovementScript = _player.GetComponent<MovementScriptBlock>();
    }

    void Update()
    {
        if (_movePlayer && Vector3.Distance(_player.transform.position, _animationSpot.transform.position) > 0.01f) 
        {
            var step = Time.deltaTime * 1.0f;
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _animationSpot.transform.position, step);
        }

        if (_isBlackedOut)
        {
            Color color = _blackoutPanel.color;
            color.a += Time.deltaTime * 1.0f;
            _blackoutPanel.color = color;
        }

        if (_isPeriscopeView && _periscopePanel.color.a < 256)
        {
            Color color = _periscopePanel.color;
            color.a += Time.deltaTime * 0.1f;
            _periscopePanel.color = color;
        }
        if (!_isPeriscopeView && _periscopePanel.color.a > 0)
        {
            Color color = _periscopePanel.color;
            color.a -= Time.deltaTime * 0.3f;
            _periscopePanel.color = color;
        }

        if (!_isMusicLoud)
        {
            _musicSource.volume -= Time.deltaTime * 0.01f;
        }
    }

    IEnumerator MoveToAnimationSpot()
    {
        _playerRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        _movePlayer = true;
        _player.transform.eulerAngles = new Vector3(0f, 270f, 0f);
        _playerMovementScript.IsAvailable = false;
        yield return null;
    }

    IEnumerator DisplayCredits()
    {
        yield return new WaitForSeconds(2.0f);
        _isBlackedOut = true;
        yield return new WaitForSeconds(2.0f);
        _isPeriscopeView = true;
        _musicSource.Play();
        yield return new WaitForSeconds(4.0f);
        _madeBy.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        _madeBy.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        _dedication.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        _dedication.SetActive(false);
        yield return new WaitForSeconds(1.9f);
        _inspiredBy.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        _inspiredBy.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        _callToAction.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        _isPeriscopeView = false;
        _isMusicLoud = false;
        yield return new WaitForSeconds(5.0f);
        _callToAction.SetActive(false);
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene("LevelLayout");
        
        yield return null;
    }
}
