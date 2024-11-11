using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private ProductInterface productInterfacePrefab;
    [SerializeField] private ProductUI productPrefab;
    [SerializeField] private int poolSize = 10;

    private static Queue<GameObject> productInterfacePool;
    private static Queue<GameObject> productPool;
    private static Transform ProductPoolTransform;

    private void Start()
    {
        ProductPoolTransform = transform;
        productPool = new Queue<GameObject>(poolSize);
        productInterfacePool = new Queue<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            if (productPrefab != null)
            {
                GameObject productGO = Instantiate(productPrefab.gameObject, transform);
                productPool.Enqueue(productGO);
            }

            if (productInterfacePrefab != null)
            {
                GameObject ProductInterface = Instantiate(productInterfacePrefab.gameObject, transform);
                productInterfacePool.Enqueue(ProductInterface);
            }
        }
    }

    public static GameObject GetProduct()
    {
        return productPool.Dequeue();
    }
    public static void ReturnProductToPool(GameObject productGO)
    {
        productGO.transform.parent = ProductPoolTransform;
        productGO.transform.localPosition = Vector3.zero;
        productPool.Enqueue(productGO);
    }

    public static GameObject GetProductInterface()
    {
        return productInterfacePool.Dequeue();
    }
    public static void ReturnProductInterfaceToPool(GameObject productInterface)
    {
        productInterface.transform.parent = ProductPoolTransform;
        productInterface.transform.localPosition = Vector3.zero;
        productInterfacePool.Enqueue(productInterface);
    }

}