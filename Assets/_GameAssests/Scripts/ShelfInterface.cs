using System.Collections.Generic;
using UnityEngine;

//Modify the name and price of displayed products
//Submit change and send display information update -> to UIManager
public class ShelfInterface : MonoBehaviour
{
    [SerializeField] private Transform container;

    private List<ProductData> activeProductList = new List<ProductData>();
    private List<ProductInterface> productInterfaceList = new List<ProductInterface>();

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
        List<ProductData> toRemoveList = new List<ProductData>();//Clearing cached GameObjects and returning to pool
        List<ProductInterface> removeList = new List<ProductInterface>();//Clearing cached Interfaces and returning to pool

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