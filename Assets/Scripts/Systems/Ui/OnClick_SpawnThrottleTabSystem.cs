using UnityEngine;
using UnityEngine.UI;

public class OnClick_SpawnThrottleTabSystem : MonoBehaviour
{
    [SerializeField] OrderPageThrottleParentComponent orderPageThrottleParentComponent;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            GameObject throttleTabObject = Instantiate(orderPageThrottleParentComponent.throttleTabPrefab, orderPageThrottleParentComponent.transform);
        });
    }
}
