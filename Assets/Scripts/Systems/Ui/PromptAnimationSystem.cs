using UnityEngine;
using UnityEngine.UI;

public class PromptAnimationSystem : MonoBehaviour
{
    PromptComponent promptComponent;
    ClosePositionPromptComponent closePositionPromptComponent;
    Animation anim;
    Image image;

    void Start()
    {
        promptComponent = GetComponent<PromptComponent>();
        closePositionPromptComponent = GetComponent<ClosePositionPromptComponent>();
        anim = GetComponent<Animation>();
        image = GetComponent<Image>();

        if (promptComponent != null)
        {
            image.enabled = promptComponent.active;
            promptComponent.onChange_active.AddListener(Active);
        }
        if (closePositionPromptComponent != null)
        {
            image.enabled = closePositionPromptComponent.active;
            closePositionPromptComponent.onChange_active.AddListener(Active);
        }
    }
    void Active(bool active)
    {
        image.enabled = active;
        if (active)
        {
            anim.Play("PromptShow");
        }
        else
        {
            anim.Play("PromptHide");
        }
    }
}
