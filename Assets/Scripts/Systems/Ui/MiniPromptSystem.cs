using UnityEngine;

public class MiniPromptSystem : MonoBehaviour
{
    MiniPromptComponent miniPromptComponent;

    void Start()
    {
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;

        miniPromptComponent.onChange_message.AddListener(value =>
        {
            miniPromptComponent.messageText.text = value;
            miniPromptComponent.anim.Stop();
            miniPromptComponent.anim.Play();
        });
    }
}