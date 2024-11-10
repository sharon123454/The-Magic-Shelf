using System;

/// <summary>
/// Product array matching JSON data
/// </summary>
[Serializable]
public class ProductList
{
    private int listSize = 3;
    public Product[] products;

    /// <summary>
    /// Constructor
    /// </summary>
    public ProductList() { products = new Product[listSize]; }

    public void AddProductToArray(Product product)
    {
        for (int i = 0; i < listSize; i++)//Go over array
        {
            if (products[i] != null) { continue; }//If non null product in position, skip iteration

            products[i] = product;//If null add Product
            break;//Exit method
        }
    }
    public void ClearArray()
    {
        for (int i = 0; i < listSize; i++) { products[i] = null; }
    }

    public override string ToString()
    {
        string toString = $"Product amount:{products.Length}\n";

        foreach (Product product in products)
            toString += $"{product.ToString()}\n";

        return toString;
    }

}