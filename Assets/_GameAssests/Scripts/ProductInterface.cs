using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(Button))]
public class ProductInterface : MonoBehaviour
{
    public static Action<string> OnAnyProductButtonPressed;

    [SerializeField] private TextMeshProUGUI t_ProductName;
    [SerializeField] private TextMeshProUGUI t_ProductPrice;
    [SerializeField] private TextMeshProUGUI t_ProductDescription;

    private Button productInterfaceButton;

    private void Awake()
    {
        productInterfaceButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        productInterfaceButton.onClick.AddListener(InterfaceButtonClicked);
    }
    private void OnDisable()
    {
        productInterfaceButton.onClick.RemoveListener(InterfaceButtonClicked);
    }

    public void UpdateProductInterface(Product productData)
    {
        t_ProductName.text = productData.name;
        t_ProductPrice.text = $"${productData.price}";
        t_ProductDescription.text = productData.description;
    }

    public override string ToString()
    {
        return $"Name: {t_ProductName.text}, Price: {t_ProductPrice.text}, Description: {t_ProductDescription.text}";
    }

    private void InterfaceButtonClicked()
    {
        OnAnyProductButtonPressed?.Invoke($"{t_ProductName.text}, {t_ProductPrice.text}");
    }

}