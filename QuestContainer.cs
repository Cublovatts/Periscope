using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    [SerializeField]
    private Sprite completedSprite;
    [SerializeField]
    private Sprite incompleteSprite;

    private GameObject _titleTextObject;
    private GameObject _descriptionTextObject;
    private GameObject _checkboxImageObject;

    private Text _titleText;
    private Text _descriptionText;
    private Image _checkboxImage;

    private QuestManager.QuestEnum _questEnum;
    private string _referenceTitle;

    public void InstaniateQuestContainer()
    {
        _titleText = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        _titleTextObject = gameObject.transform.GetChild(0).gameObject;
        _descriptionText = gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        _descriptionTextObject = gameObject.transform.GetChild(1).gameObject;
        _checkboxImage = gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        _checkboxImageObject = gameObject.transform.GetChild(2).gameObject;
    }

    public string GetTitle()
    {
        return _referenceTitle;
    }

    public void SetQuestEnum(QuestManager.QuestEnum questEnum)
    {
        _questEnum = questEnum;
    }

    public QuestManager.QuestEnum GetQuestEnum()
    {
        return _questEnum;
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
        _referenceTitle = title;
    }

    public void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void SetCompleted(bool completed)
    {
        if (completed)
        {
            _checkboxImage.sprite = completedSprite;
        } else
        {
            _checkboxImage.sprite = incompleteSprite;
        }
    }

    public void Hide()
    {
        _titleTextObject.active = false;
        _descriptionTextObject.active = false;
        _checkboxImageObject.active = false;
    }

    public void Show()
    {
        _titleTextObject.active = true;
        _descriptionTextObject.active = true;
        _checkboxImageObject.active = true;
    }
}
