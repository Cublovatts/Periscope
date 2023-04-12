using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCount : MonoBehaviour
{
    private Text _text;
    private int _totalCurrency;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "$" + _totalCurrency.ToString();
    }

    public void AddCurrency(int toAdd)
    {
        _totalCurrency += toAdd;
        StartCoroutine(ShowForTime(5.0f));
    }

    [ContextMenu("ShowText")]
    public void ShowText()
    {
        _animator.SetBool("IsShowing", true);
    }

    [ContextMenu("HideText")]
    public void HideText()
    {
        _animator.SetBool("IsShowing", false);
    }

    IEnumerator ShowForTime(float seconds)
    {
        ShowText();
        yield return new WaitForSeconds(seconds);
        HideText();
    }

    public int GetCurrency()
    {
        return _totalCurrency;
    }

    [ContextMenu("AddCurrency5")]
    public void AddCurrency5()
    {
        AddCurrency(5);
    }
}
