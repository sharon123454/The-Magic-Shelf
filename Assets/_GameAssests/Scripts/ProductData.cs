using UnityEngine;

public struct ProductData
{
    public int _index;
    public GameObject _gameObject;
    public ProductUI _productUI;
    public Product _product;

    public ProductData(int index, GameObject gameObject, ProductUI productUI, Product product)
    {
        _index = index; _gameObject = gameObject;
        _productUI = productUI; _product = product;
    }
}