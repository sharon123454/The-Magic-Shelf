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

    private ProductList currentProductsData = new ProductList();
    private Queue<GameObject> currentProductVisualsQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)//If instance isn't null and isn't this object, Destroy yourself
        {
            Debug.Log($"{name} was destroyed.");
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void AddProductToShelf(Product product)
    {
        if (product == null) { Debug.LogError("product is null"); return; }//Null catch

        currentProductsData.AddProductToArray(product);//Caching current product data

        GameObject productGO = ProductPooler.GetProduct();//Drawing a product GO prefab
        
        ProductUI productUI = productGO.GetComponent<ProductUI>();//Updating product UI
        productUI.UpdateProductUI(product);

        productGO.transform.parent = productPositions[currentProductVisualsQueue.Count];//Positioning product GO prefab + caching
        productGO.transform.localPosition = Vector3.zero;
        currentProductVisualsQueue.Enqueue(productGO);
    }
    public void ClearShelfProducts()
    {
        currentProductsData.ClearArray();//Clearing cached product data

        while (currentProductVisualsQueue.Count > 0)//Clearing cached Game Objects and returning to pool
        {
            GameObject productGO = currentProductVisualsQueue.Dequeue();
            ProductPooler.ReturnProductToPool(productGO);
        }
    }

}