using UnityEngine;

/// <summary>
/// Manages game data, observes and reacts according to game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Shelf magicShelf;

    private void OnEnable()
    {
        ReadData.OnDataRecievedFromURL += ReadData_OnDataRecievedFromURL;
    }

    private void ReadData_OnDataRecievedFromURL(string uRLText)
    {
        //prase Json
        ProductList products = new ProductList();
        products = JsonHelper.CreateProductsFromJSON(uRLText);
        print(products.ToString());
        //for (int i = 0; i < products.products.Length; i++)
        //{
        //    print(products.products[i].ToString());
        //}
    }

    //Modify the name and price of displayed products
    //Submit change and send display information update -> to UIManager

}