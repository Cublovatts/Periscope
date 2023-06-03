using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private Animator _animator;
    private Text _nameText;
    private Text _displayLine;
    private Queue<string> _dialogueQueue = new Queue<string>();
    private Action _currentCallback;
    private MovementScriptBlock _movementScriptBlock;
    private AudioSource _audioSource;
    
    private bool _sentenceFinished = false;
    private string _currentLine;

    private void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameObject dialogueBox = GameObject.Find("DialogueBox");
        _animator = dialogueBox.GetComponent<Animator>();
        _nameText = dialogueBox.transform.GetChild(0).GetComponent<Text>();
        _displayLine = dialogueBox.transform.GetChild(1).GetComponent<Text>();
        _movementScriptBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScriptBlock>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _animator.GetBool("IsShowing") && _sentenceFinished)
        {
            _sentenceFinished = false;
            DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.E) && _animator.GetBool("IsShowing") && !_sentenceFinished)
        {
            FinishSentence();
        }

        if (Input.touchCount > 0)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, Action callback)
    {
        _movementScriptBlock.IsAvailable = false;
        _currentCallback = callback;
        _animator.SetBool("IsShowing", true);
        _nameText.text = dialogue.name;
        _dialogueQueue.Clear();

        foreach(string line in dialogue.lines)
        {
            _dialogueQueue.Enqueue(line);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_dialogueQueue.Count == 0) 
        {
            EndDialogue();
            return;
        }
 
        string line = _dialogueQueue.Dequeue();
        _currentLine = line;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence(string line)
    {
        _sentenceFinished = false;
        _displayLine.text = "";
        foreach (char letter in line.ToCharArray())
        {
            _displayLine.text += letter;
            yield return new WaitForSeconds(0.05f);
            _audioSource.Play();
        }
        _sentenceFinished = true;
    }

    public void EndDialogue()
    {
        _animator.SetBool("IsShowing", false);
        _movementScriptBlock.IsAvailable = true;
        StopAllCoroutines();
        StartCoroutine(DelayedCallback());
    }

    IEnumerator DelayedCallback()
    {
        yield return new WaitForSeconds(0.1f);
        if (_currentCallback != null)
        {
            _currentCallback();
        }
    }

    public void FinishSentence()
    {
        StopAllCoroutines(); 
        _displayLine.text = _currentLine;
        _sentenceFinished = true;
    }
}
