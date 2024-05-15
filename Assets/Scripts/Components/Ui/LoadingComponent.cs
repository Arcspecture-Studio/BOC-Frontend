using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class LoadingComponent : MonoBehaviour
{
    [Header("Reference")]
    public RectTransform iconRectTransform;
    public Animation anim;

    [Header("Config")]
    public Ease iconRotateAnimEase;
    [SerializeField] private bool _active;
    public bool active
    {
        get { return _active; }
        set
        {
            if (_active != value)
            {
                _active = value;
                onChange_active.Invoke();
            }
        }
    }
    [HideInInspector] public UnityEvent onChange_active = new();
    [HideInInspector] public UnityEvent deactive = new();

    void Deactive() // Used by animation: LoadingHide
    {
        deactive.Invoke();
    }
}