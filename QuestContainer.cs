using UnityEngine;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    [Header("Display Symbols")]
    [SerializeField]
    private Sprite _completedSprite;
    [SerializeField]
    private Sprite _incompleteSprite;

    private GameObject _titleTextObject;
    private GameObject _descriptionTextObject;
    private GameObject _checkboxImageObject;
    private Text _titleText;
    private Text _descriptionText;
    private Image _checkboxImage;

    private QuestManager.QuestEnum _questEnum;

    public void InstaniateQuestContainer()
    {
        _titleText = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        _titleTextObject = gameObject.transform.GetChild(0).gameObject;
        _descriptionText = gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        _descriptionTextObject = gameObject.transform.GetChild(1).gameObject;
        _checkboxImage = gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        _checkboxImageObject = gameObject.transform.GetChild(2).gameObject;
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
    }

    public void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void SetCompleted(bool completed)
    {
        if (completed)
        {
            _checkboxImage.sprite = _completedSprite;
        } else
        {
            _checkboxImage.sprite = _incompleteSprite;
        }
    }

    public void Hide()
    {
        _titleTextObject.SetActive(false);
        _descriptionTextObject.SetActive(false);
        _checkboxImageObject.SetActive(false);
    }

    public void Show()
    {
        _titleTextObject.SetActive(true);
        _descriptionTextObject.SetActive(true);
        _checkboxImageObject.SetActive(true);
    }
}
