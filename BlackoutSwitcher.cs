using UnityEngine;
using UnityEngine.UI;

public class BlackoutSwitcher : MonoBehaviour
{
    public bool IsBlackedOut = false;

    private Image _blackoutPanel;

    private void Awake()
    {
        _blackoutPanel = GetComponent<Image>();
    }

    void Update()
    {
        if (!IsBlackedOut && _blackoutPanel.color.a > 0)
        {
            Color color = _blackoutPanel.color;
            color.a -= Time.deltaTime * 0.5f;
            _blackoutPanel.color = color;
        } else if (IsBlackedOut && _blackoutPanel.color.a < 256)
        {
            Color color = _blackoutPanel.color;
            color.a += Time.deltaTime * 0.5f;
            _blackoutPanel.color = color;
        }
    }
}
