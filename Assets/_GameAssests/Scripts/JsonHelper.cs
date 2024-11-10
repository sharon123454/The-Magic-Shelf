using UnityEngine;

/// <summary>
/// Converts JSON text from web request into class
/// </summary>
[System.Serializable]
public class JsonHelper
{
    // Given JSON input:
    // {"products":[{"name":"Product 3","description":"Description of Product 3","price":39.99}]}
    // this example will return a ProductList with an object object with
    // name == "Product 3", price == 39.99, and description == "Description of Product 3".

    /// <summary>
    /// Returns class using JSON data
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static ProductList CreateProductsFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ProductList>(jsonString);
    }

}