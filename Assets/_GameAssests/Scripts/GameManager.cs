using UnityEngine;
using System;

/// <summary>
/// Manages game data, observes and reacts according to game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static event Action<ProductList> OnDataParsed;

    private void OnEnable()
    {
        ReadData.OnDataRecievedFromURL += ReadData_OnDataRecievedFromURL;
    }

    private void ReadData_OnDataRecievedFromURL(string uRLText)
    {
        ProductList recievedProductList = JsonHelper.CreateProductsFromJSON(uRLText);//prasing JSoon

        int productsCount = recievedProductList.products.Length;
        if (productsCount <= 0) { Debug.LogError("No products recieved"); return; }
        
        OnDataParsed?.Invoke(recievedProductList);
    }

}