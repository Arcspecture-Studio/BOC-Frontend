using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PromptComponent : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public Button okayButton;
    public Button rightButton;
    public Button leftButton;
    public TMP_Text rightButtonText;
    public TMP_Text leftButtonText;

    private bool _active;
    public bool active
    {
        get { return _active; }
        set
        {
            _active = value;
            onChange_active.Invoke(value);
        }
    }
    [HideInInspector] public UnityEvent<bool> onChange_active = new();

    void Show(string title, string message)
    {
        this.active = true;
        this.title.text = title;
        this.description.text = message;
        this.leftButton.onClick.RemoveAllListeners();
        this.rightButton.onClick.RemoveAllListeners();
        this.okayButton.onClick.RemoveAllListeners();
    }
    public void ShowPrompt(string title, string message, UnityAction okayButtonCallback)
    {
        Show(title, message);
        this.leftButton.gameObject.SetActive(false);
        this.rightButton.gameObject.SetActive(false);
        this.okayButton.gameObject.SetActive(true);
        this.okayButton.onClick.AddListener(okayButtonCallback);
    }
    public void ShowSelection(string title, string message,
        string leftButtonText, string rightButtonText,
        UnityAction leftButtonCallback, UnityAction rightButtonCallback)
    {
        Show(title, message);
        this.okayButton.gameObject.SetActive(false);
        this.leftButton.gameObject.SetActive(true);
        this.leftButton.onClick.AddListener(leftButtonCallback);
        this.leftButtonText.text = leftButtonText;
        this.rightButton.gameObject.SetActive(true);
        this.rightButton.onClick.AddListener(rightButtonCallback);
        this.rightButtonText.text = rightButtonText;
    }
}
