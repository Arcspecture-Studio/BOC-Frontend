using UnityEngine;
using UnityEngine.UI;

public class PromptAnimationSystem : MonoBehaviour
{
    PromptComponent promptComponent;
    Animation anim;
    Image image;

    bool active;
    void Start()
    {
        promptComponent = GetComponent<PromptComponent>();
        anim = GetComponent<Animation>();
        image = GetComponent<Image>();

        active = promptComponent.active;
        image.enabled = active;
    }
    void Update()
    {
        if (active == promptComponent.active) return;
        active = promptComponent.active;
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
