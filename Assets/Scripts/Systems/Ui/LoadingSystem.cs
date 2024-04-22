using UnityEngine;

public class LoadingSystem : MonoBehaviour
{
    LoadingComponent loadingComponent;

    void Start()
    {
        loadingComponent = GlobalComponent.instance.loadingComponent;

        loadingComponent.onChange_active.AddListener(ActiveOrDeactiveUi);
        loadingComponent.deactive.AddListener(Deactive);

        Deactive();
    }

    void ActiveOrDeactiveUi()
    {
        if (loadingComponent.active)
        {
            loadingComponent.gameObject.SetActive(loadingComponent.active);
            loadingComponent.anim.Play("LoadingShow");
        }
        else
        {
            loadingComponent.anim.Play("LoadingHide");
        }
    }
    void Deactive()
    {
        loadingComponent.gameObject.SetActive(false);
    }
}
