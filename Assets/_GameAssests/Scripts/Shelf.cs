using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages product visuals
/// </summary>
public class Shelf : MonoBehaviour
{
    /// <summary>
    /// World Positions for products on the shelf
    /// </summary>
    [SerializeField] internal Transform[] productPositions;

    private static List<ProductData> activeProductList = new List<ProductData>();

    private void OnEnable()
    {
        GameManager.OnDataParsed += GameManager_OnDataParsed;
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

        ProductData newData = new ProductData(activeProductList.Count, productGO, productUI, product);//Creating data struct for eaier handling
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

    private void GameManager_OnDataParsed(ProductList newProductList)
    {
        ClearShelfProducts();

        for (int i = 0; i < newProductList.products.Length; i++)
        {
            Product product = newProductList.products[i];
            AddProductToShelf(product);
        }
    }

}