using System.Collections.Generic;
using UnityEngine;

//Modify the name and price of displayed products
//Submit change and send display information update -> to UIManager
public class ShelfInterface : MonoBehaviour
{
    [SerializeField] private Transform container;

    private List<ProductData> activeProductList = new List<ProductData>();
    private static List<ProductInterface> productInterfaceList = new List<ProductInterface>();

    public static List<ProductInterface> GetProductInterfaceList() {  return productInterfaceList; }

    public void AddProductsToInterface(List<ProductData> products)
    {
        if (products.Count <= 0) { Debug.LogError("product list is empy"); return; }//Empty catch

        foreach (ProductData productData in products)
        {
            activeProductList.Add(productData);
            GameObject productInterface = ObjectPooler.GetProductInterface();//Pooling a prefab product interface
            productInterface.transform.parent = container;//Positioning interface prefab
            productInterface.transform.localPosition = Vector3.zero;

            ProductInterface newInterface = productInterface.GetComponent<ProductInterface>();
            newInterface.UpdateProductInterface(productData._product);

            productInterfaceList.Add(newInterface);
        }
    }
    public void ClearInterface()
    {
        //Clearing cached Data and returning to pool
        List<ProductData> toRemoveList = new List<ProductData>();
        List<ProductInterface> removeList = new List<ProductInterface>();

        foreach (ProductData product in activeProductList)
        {
            toRemoveList.Add(product);
        }
        foreach (ProductInterface productInterface in productInterfaceList)
        {
            removeList.Add(productInterface);
        }

        foreach (ProductData product in toRemoveList)
        {
            ObjectPooler.ReturnProductToPool(product._gameObject);
            activeProductList.Remove(product);
        }
        foreach (ProductInterface productInterface in removeList)
        {
            ObjectPooler.ReturnProductInterfaceToPool(productInterface.gameObject);
            productInterfaceList.Remove(productInterface);
        }

        toRemoveList.Clear();
        removeList.Clear();
    }

}