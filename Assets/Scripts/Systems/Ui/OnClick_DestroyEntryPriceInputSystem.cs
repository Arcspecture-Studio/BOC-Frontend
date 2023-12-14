using UnityEngine;
using UnityEngine.UI;

public class OnClick_DestroyEntryPriceInputSystem : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            if(transform.parent.parent.childCount > 1)
            {
                DestroyImmediate(transform.parent.gameObject);
            }
        });
    }
}
