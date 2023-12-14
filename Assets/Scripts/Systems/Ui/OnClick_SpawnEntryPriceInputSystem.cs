using UnityEngine;
using UnityEngine.UI;

public class OnClick_SpawnEntryPriceInputSystem : MonoBehaviour
{
    [SerializeField] OrderPageInputEntryPricesComponent inputEntryPricesComponent;

    Button button;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            Instantiate(inputEntryPricesComponent.priceInput, inputEntryPricesComponent.parent);
        });
    }
}
