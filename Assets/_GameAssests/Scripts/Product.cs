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

    public override string ToString() { return $"{name}, {price}, {description}"; }
}