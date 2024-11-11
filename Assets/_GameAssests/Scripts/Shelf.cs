using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// Manages product visuals
/// </summary>
public class Shelf : MonoBehaviour
{
    public static Action<ProductList> OnShelfUpdated;

    /// <summary>
    /// World Positions for products on the shelf
    /// </summary>
    [SerializeField] internal Transform[] productPositions;

    private static List<ProductData> activeProductList = new List<ProductData>();

    private void OnEnable()
    {
        ReadData.OnDataRecievedFromURL += ReadData_OnDataRecievedFromURL;
    }
    private void OnDisable()
    {
        ReadData.OnDataRecievedFromURL -= ReadData_OnDataRecievedFromURL;
    }

    public static List<ProductData> GetCurrentActiveProducts() { return activeProductList; }

    private void AddProductToShelf(Product product)
    {
        if (product == null) { Debug.LogError("product is null"); return; }//Null catch

        GameObject productGO = ObjectPooler.GetProduct();//Pooling a prefab product GO

        ProductUI productUI = productGO.GetComponent<ProductUI>();//Updating product UI
        productUI.UpdateProductUI(product);

        productGO.transform.parent = productPositions[activeProductList.Count];//Positioning product GO prefab + caching
        productGO.transform.localPosition = Vector3.zero;

        ProductData newData = new ProductData(productGO, productUI, product);//Creating data struct for eaier handling
        activeProductList.Add(newData);
    }
    private void ClearShelfProducts()
    {
        List<ProductData> toRemoveList = new List<ProductData>();//Clearing cached Game Objects and returning to pool
        foreach (ProductData product in activeProductList)
        {
            toRemoveList.Add(product);
        }
        foreach (ProductData product in toRemoveList)
        {
            ObjectPooler.ReturnProductToPool(product._gameObject);
            activeProductList.Remove(product);
        }
        toRemoveList.Clear();
    }

    private void ReadData_OnDataRecievedFromURL(string uRLText)
    {
        ProductList recievedProductList = JsonHelper.CreateProductsFromJSON(uRLText);//prasing JSoon

        int productsCount = recievedProductList.products.Length;
        if (productsCount <= 0) { Debug.LogError("No products recieved"); return; }

        ClearShelfProducts();

        for (int i = 0; i < recievedProductList.products.Length; i++)
        {
            Product product = recievedProductList.products[i];
            AddProductToShelf(product);
        }

        OnShelfUpdated?.Invoke(recievedProductList);
    }

}