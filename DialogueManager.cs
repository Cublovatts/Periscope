using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text displayLine;
    private Queue<string> dialogueQueue = new Queue<string>();
    private Action currentCallback;
    private MovementScriptBlock movementScriptBlock;
    private AudioSource _audioSource;

    public Animator animator;

    private bool sentenceFinished = false;
    private string currentLine;

    void Start()
    {
        movementScriptBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScriptBlock>();
        dialogueQueue = new Queue<string>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && animator.GetBool("IsShowing") && sentenceFinished)
        {
            sentenceFinished = false;
            DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.E) && animator.GetBool("IsShowing") && !sentenceFinished)
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
        movementScriptBlock.IsAvailable = false;

        currentCallback = callback;

        animator.SetBool("IsShowing", true);

        nameText.text = dialogue.name;

        dialogueQueue.Clear();

        foreach(string line in dialogue.lines)
        {
            dialogueQueue.Enqueue(line);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0) 
        {
            EndDialogue();
            return;
        }
 
        string line = dialogueQueue.Dequeue();
        currentLine = line;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence(string line)
    {
        sentenceFinished = false;
        displayLine.text = "";
        foreach (char letter in line.ToCharArray())
        {
            displayLine.text += letter;
            yield return new WaitForSeconds(0.05f);
            _audioSource.Play();
        }
        sentenceFinished = true;
    }

    public void EndDialogue()
    {
        animator.SetBool("IsShowing", false);
        movementScriptBlock.IsAvailable = true;
        StopAllCoroutines();
        StartCoroutine(DelayedCallback());
    }

    IEnumerator DelayedCallback()
    {
        yield return new WaitForSeconds(0.1f);
        if (currentCallback != null)
        {
            currentCallback();
        }
    }

    public void FinishSentence()
    {
        StopAllCoroutines(); 
        displayLine.text = currentLine;
        sentenceFinished = true;
    }
}
