using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OrderPageSymbolDropdownSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] OrderPageSymbolDropdownComponent orderPageSymbolDropdownComponent;

    PlatformComponent platformComponent;
    InputComponent inputComponent;
    TMP_Dropdown dropdown;

    bool hover;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        inputComponent = GlobalComponent.instance.inputComponent;
        dropdown = GetComponent<TMP_Dropdown>();

        inputComponent.click.performed += OnClick;
        dropdown.onValueChanged.AddListener((value) =>
        {
            orderPageSymbolDropdownComponent.selectedSymbol = orderPageSymbolDropdownComponent.symbols[value];
        });
    }
    void Update()
    {
        SyncAllSymbols();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }
    void OnDestroy()
    {
        inputComponent.click.performed -= OnClick;
    }

    public void OnSearchChanged(string value)
    {
        orderPageSymbolDropdownComponent.symbols = platformComponent.allSymbols.FindAll(symbol => symbol.Contains(value.ToUpper()));
        UpdateOptions();
    }
    void UpdateOptions()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(orderPageSymbolDropdownComponent.symbols);
        dropdown.value = orderPageSymbolDropdownComponent.symbols.IndexOf(orderPageSymbolDropdownComponent.selectedSymbol);
    }
    void SyncAllSymbols()
    {
        if (orderPageSymbolDropdownComponent.allSymbolsCount == platformComponent.allSymbols.Count) return;
        orderPageSymbolDropdownComponent.allSymbolsCount = platformComponent.allSymbols.Count;

        orderPageSymbolDropdownComponent.symbols = platformComponent.allSymbols;
        UpdateOptions();
    }
    void OnClick(InputAction.CallbackContext context)
    {
        if (hover)
        {
            orderPageSymbolDropdownComponent.symbols = platformComponent.allSymbols;
            UpdateOptions();
        }
    }
}