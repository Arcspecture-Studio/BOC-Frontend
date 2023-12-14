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
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    },
                    () =>
                    {
                        promptComponent.active = false;
                    });
            }
        }
    }
}
