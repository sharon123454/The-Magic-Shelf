using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class ProductEditScreen : MonoBehaviour
{
    public static Action<Product> OnAnyProductEdited;

    [SerializeField] private Button okayButton;
    [SerializeField] private TextMeshProUGUI currentEditedProductData;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField priceField;

    private Product currentlyEditedProduct;

    private void Awake()
    {
        okayButton.onClick.AddListener(OnOkayClicked);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        okayButton.onClick.RemoveListener(OnOkayClicked);
    }

    public void Setup(Product pressedProduct)
    {
        nameField.text = string.Empty;
        priceField.text = string.Empty;
        currentlyEditedProduct = pressedProduct;
        currentEditedProductData.text = $"{currentlyEditedProduct.name}, ${currentlyEditedProduct.price}";
    }

    /// <summary>
    /// Change from editing screen to confirmation
    /// </summary>
    private void OnOkayClicked()
    {
        Product newProductData = null;//Setting product data matching before changes
        string name = currentlyEditedProduct.name;
        float price = currentlyEditedProduct.price;
        string description = currentlyEditedProduct.description;

        //Checking for changes in the input fields
        if (nameField.text != string.Empty && nameField.text != null)
        {
            name = nameField.text;
            description = $"Description of {nameField.text}";
        }
        if (priceField.text != string.Empty && priceField.text != null)
        {
            price = float.Parse(priceField.text);
        }

        newProductData = new Product(name, price, description);//finish creating the product
        OnAnyProductEdited?.Invoke(newProductData);//send it off
        gameObject.SetActive(false);//close
    }

}