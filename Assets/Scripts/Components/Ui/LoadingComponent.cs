using UnityEngine;
using UnityEngine.Events;

public class LoadingComponent : MonoBehaviour
{
    [Header("Reference")]
    public Animator animator;

    [Header("Config")]
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