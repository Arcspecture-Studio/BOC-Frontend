using UnityEngine;
using UnityEngine.InputSystem;

public class LoadingSystem : MonoBehaviour
{
    LoadingComponent loadingComponent;
    InputComponent inputComponent;

    void Start()
    {
        loadingComponent = GlobalComponent.instance.loadingComponent;
        inputComponent = GlobalComponent.instance.inputComponent;

        loadingComponent.onChange_active.AddListener(ActiveOrDeactiveUi);
        loadingComponent.deactive.AddListener(Deactive);
        inputComponent.space.started += Test;

        Deactive();
    }

    void ActiveOrDeactiveUi()
    {
        if (loadingComponent.active)
        {
            loadingComponent.gameObject.SetActive(loadingComponent.active);
            loadingComponent.animator.SetBool("show", true);
        }
        else
        {
            loadingComponent.animator.SetBool("show", false);
        }
    }
    void Deactive()
    {
        loadingComponent.gameObject.SetActive(false);
    }
    void Test(InputAction.CallbackContext context)
    {
        loadingComponent.active = !loadingComponent.active;
    }
}
