using UnityEngine;

public class ExitSystem : MonoBehaviour
{
    PromptComponent promptComponent;

    void Start()
    {
        promptComponent = GlobalComponent.instance.promptComponent;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (promptComponent.active)
            {
                promptComponent.active = false;
            }
            else
            {
                promptComponent.active = true;
                promptComponent.ShowSelection(PromptConstant.EXIT, PromptConstant.EXIT_PROMPT, PromptConstant.YES, PromptConstant.NO,
                    () =>
                    {
                        Utils.QuitApplication();
                    },
                    () =>
                    {
                        promptComponent.active = false;
                    });
            }
        }
    }
}
