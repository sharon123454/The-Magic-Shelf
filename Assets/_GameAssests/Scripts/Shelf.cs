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

    private ProductList currentProductsData = new ProductList();
    private Queue<GameObject> currentProductVisualsQueue = new Queue<GameObject>();

    public void AddProductToShelf(Product product)
    {
        if (product == null) { Debug.LogError("product is null"); return; }//Null catch

        currentProductsData.AddProductToArray(product);//Caching current product data

        GameObject productGO = ProductPooler.GetProduct();//Drawing a product GO prefab and positioning it + caching
        productGO.transform.parent = productPositions[currentProductVisualsQueue.Count];
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