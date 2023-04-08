using UnityEngine;
using UnityEngine.UI;

public class SliderNode : MonoBehaviour
{
    [SerializeField]
    private bool isLeftNode;
    private bool isSet = false;

    private GameObject _middleBar;
    private Image _nodeImage;

    void Start()
    {
        _middleBar = GameObject.Find("MiddleBar");
        _nodeImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isSet) {
            // Check proximity
            float distance = Vector2.Distance(transform.position, _middleBar.transform.position);
            if (distance < 200 && distance > 100)
            {
                setToFail();
            }
            if (distance < 100 && isLeftNode)
            {
                setToPass();
            }
            if (distance < 100 && !isLeftNode)
            {
                setToFail();
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !isSet)
        {
            // Check proximity
            float distance = Vector2.Distance(transform.position, _middleBar.transform.position);
            if (distance < 200 && distance > 100)
            {
                setToFail();
            }
            if (distance < 100 && !isLeftNode)
            {
                setToPass();
            }
            if (distance < 100 && isLeftNode)
            {
                setToFail();
            }
        }
    }

    void setToPass()
    {
        isSet = true;
        _nodeImage.color = Color.green;
    }

    void setToFail()
    {
        isSet = true;
        _nodeImage.color = Color.red;
    }
}
