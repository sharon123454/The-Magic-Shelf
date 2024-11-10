using System;

/// <summary>
/// Product array matching JSON data
/// </summary>
[Serializable]
public class ProductList
{
    public Product[] products;

    public override string ToString()
    {
        string toString = $"Product amount:{products.Length}\n";

        foreach (Product product in products)
            toString += $"{product.ToString()}\n";

        return toString;
    }

}