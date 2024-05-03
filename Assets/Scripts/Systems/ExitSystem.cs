using UnityEngine;

public class ExitSystem : MonoBehaviour
{
    ExitComponent exitComponent;
    PromptComponent promptComponent;

    void Start()
    {
        exitComponent = GlobalComponent.instance.exitComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

        exitComponent.onChange_exit.AddListener(QuitApplication);
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
                    exitComponent.exit = true;
                },
                () =>
                {
                    promptComponent.active = false;
                });
            }
        }
    }
    void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}
