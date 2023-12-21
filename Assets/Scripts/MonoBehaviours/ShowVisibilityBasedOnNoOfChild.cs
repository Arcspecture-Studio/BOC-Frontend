using UnityEngine;

[ExecuteInEditMode]
public class ShowVisibilityBasedOnNoOfChild : MonoBehaviour
{
    public GameObject visibleObject;
    public Transform parent;
    public long targetChildCountMin;
    public long targetChildCountMax;
    long childCount;

    void Start()
    {
        childCount = -1;
    }
    void Update()
    {
        if (childCount == parent.childCount) return;
        childCount = parent.childCount;
        visibleObject.SetActive(childCount >= targetChildCountMin && childCount <= targetChildCountMax);
    }
}
