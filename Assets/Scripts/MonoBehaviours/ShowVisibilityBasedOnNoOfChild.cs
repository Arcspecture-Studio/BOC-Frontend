using UnityEngine;

[ExecuteInEditMode]
public class ShowVisibilityBasedOnNoOfChild : MonoBehaviour
{
    public GameObject visibleObject;
    public Transform parent;
    public int targetChildCountMin;
    public int targetChildCountMax;
    int childCount;

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
