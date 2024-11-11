using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages product visuals (Singleton)
/// </summary>
public class Shelf : MonoBehaviour
{
    public static Shelf Instance;

    /// <summary>
    /// World Positions for products on the shelf
    /// </summary>
    [SerializeField] internal Transform[] productPositions;

    private List<ProductData> activeProductList = new List<ProductData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)//If instance isn't null and isn't this object, Destroy yourself
        {
            Debug.Log($"{name} was destroyed.");
            Destroy(gameObject);
        }
        Instance = this;
    }

    public List<ProductData> GetActiveProductData() { return activeProductList; }

    public void AddProductToShelf(Product product)
    {
        if (product == null) { Debug.LogError("product is null"); return; }//Null catch

        GameObject productGO = ProductPooler.GetProduct();//Pooling a prefab product GO

        ProductUI productUI = productGO.GetComponent<ProductUI>();//Updating product UI
        productUI.UpdateProductUI(product);

        productGO.transform.parent = productPositions[activeProductList.Count];//Positioning product GO prefab + caching
        productGO.transform.localPosition = Vector3.zero;

        ProductData newData = new ProductData(activeProductList.Count, productGO, productUI, product);//Creating data struct for eaier handling
        activeProductList.Add(newData);
    }
    public void ClearShelfProducts()
    {
        List<ProductData> toRemoveList = new List<ProductData>();//Clearing cached Game Objects and returning to pool
        foreach (ProductData product in activeProductList)
        {
            toRemoveList.Add(product);
        }
        foreach (ProductData product in toRemoveList)
        {
            ProductPooler.ReturnProductToPool(product._gameObject);
            activeProductList.Remove(product);
        }
        toRemoveList.Clear();
    }

}