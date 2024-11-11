using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(Button))]
public class ProductInterface : MonoBehaviour
{
    public static Action<Product, int> OnAnyProductButtonPressed;

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
        string price = string.Empty;//Removing the $ at the start of the price
        for (int i = 1; i < t_ProductPrice.text.Length; i++)
        {
            price += t_ProductPrice.text[i];
        }

        Product myProductData = new Product(t_ProductName.text, float.Parse(price), t_ProductDescription.text);

        int myCount = 0;
        List<ProductInterface> currentProductList = ShelfInterface.GetProductInterfaceList();
        for (int i = 0; i < currentProductList.Count; i++)
        {
            if (currentProductList[i] == this) { break; }
            myCount++;
        }

        OnAnyProductButtonPressed?.Invoke(myProductData, myCount);
    }

}