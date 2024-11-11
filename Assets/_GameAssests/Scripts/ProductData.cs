using UnityEngine;

public struct ProductData
{
    public GameObject _gameObject;
    public ProductUI _productUI;
    public Product _product;

    public ProductData(GameObject gameObject, ProductUI productUI, Product product)
    {
        _gameObject = gameObject; _productUI = productUI; _product = product;
    }
}