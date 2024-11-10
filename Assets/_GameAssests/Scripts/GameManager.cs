using UnityEngine;

/// <summary>
/// Manages game data, observes and reacts according to game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Shelf magicShelf;

    private void OnEnable()
    {
        ReadData.OnDataRecievedFromURL += ReadData_OnDataRecievedFromURL;
    }

    private void ReadData_OnDataRecievedFromURL(string uRLText)
    {
        ProductList recievedProductList = JsonHelper.CreateProductsFromJSON(uRLText);//prasing JSoon

        int productsCount = recievedProductList.products.Length;
        if (productsCount <= 0) { Debug.LogError("No products recieved"); return; }

        magicShelf.ClearShelfProducts();//Clearing shelf

        for (int i = 0; i < productsCount; i++)
        {
            Product product = recievedProductList.products[i];
            magicShelf.AddProductToShelf(product);//Adding new products to shelf
        }
        print(recievedProductList.ToString());
    }

    //Modify the name and price of displayed products
    //Submit change and send display information update -> to UIManager

}