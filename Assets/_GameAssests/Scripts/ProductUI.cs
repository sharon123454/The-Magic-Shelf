using UnityEngine;
using TMPro;

public class ProductUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t_Name;
    [SerializeField] private TextMeshProUGUI t_Price;
    [SerializeField] private TextMeshProUGUI t_Description;

    public void UpdateProductUI(Product productData)
    {
        t_Name.text = productData.name;
        t_Price.text = $"${productData.price}";
        t_Description.text = productData.description;
    }

    public override string ToString()
    {
        return $"Name: {t_Name.text}, Price: {t_Price.text}, Description: {t_Description.text}";
    }

}