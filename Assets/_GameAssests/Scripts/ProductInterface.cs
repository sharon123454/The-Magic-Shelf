using UnityEngine;
using TMPro;

public class ProductInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t_ProductName;
    [SerializeField] private TextMeshProUGUI t_ProductPrice;
    [SerializeField] private TextMeshProUGUI t_ProductDescription;

    public void UpdateProductInterface(Product productData)
    {
        t_ProductName.text = productData.name;
        t_ProductPrice.text = $"${productData.price}";
        t_ProductDescription.text = productData.description;
    }
    public void SetNameAndPrice(string name, string price)
    {
        t_ProductName.text = name;
        t_ProductPrice.text = price;
    }

    public override string ToString()
    {
        return $"Name: {t_ProductName.text}, Price: {t_ProductPrice.text}, Description: {t_ProductDescription.text}";
    }

}