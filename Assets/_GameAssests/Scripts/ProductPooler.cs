using System.Collections.Generic;
using UnityEngine;

public class ProductPooler : MonoBehaviour
{
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private int poolSize = 10;

    private static Queue<GameObject> productPool = new Queue<GameObject>();
    private static Transform ProductPoolTransform;

    private void Start()
    {
        ProductPoolTransform = transform;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject productGO = Instantiate(productPrefab, transform);
            productPool.Enqueue(productGO);
        }
    }

    public static GameObject GetProduct()
    {
        return productPool.Dequeue();
    }
    public static void ReturnProductToPool(GameObject productGO)
    {
        productGO.transform.parent = ProductPoolTransform;
        productGO.transform.position = Vector3.zero;
        productPool.Enqueue(productGO);
    }

}