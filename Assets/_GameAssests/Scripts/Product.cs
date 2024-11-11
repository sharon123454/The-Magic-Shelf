using System;

/// <summary>
/// Product data matching JSON
/// </summary>
[Serializable]
public class Product
{
    public string name;
    public string description;
    public float price;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_price"></param>
    /// <param name="_description"></param>
    public Product(string _name, float _price, string _description)
    {
        name = _name;
        price = _price;
        description = _description;
    }

    public override string ToString() { return $"{name}, {price}, {description}"; }
}