using System;

[Serializable]
public class Product
{
    private string _name { get; set; }
    private string _description { get; set; }
    private float _price { get; set; }

    public Product(string name, string description, float price)
    {
        _name = name;
        _description = description;
        _price = price;
    }

    public override string ToString() { return $"{_name}, {_price}, {_description}"; }
}