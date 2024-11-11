using UnityEngine;
using TMPro;

public class ProductEditScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentEditedProductData;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Setup(string stringToDisplay)
    {
        currentEditedProductData.text = stringToDisplay;
    }

}