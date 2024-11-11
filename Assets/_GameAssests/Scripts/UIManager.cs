using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Responsible for User input and game interaction
/// </summary>
[RequireComponent(typeof(ReadData))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private ShelfInterface shelfInterface;
    [SerializeField] private Button tryRefreashDataButton;
    [SerializeField] private Button exitButton;

    private WaitForSeconds waitforXsec = new WaitForSeconds(2f);
    private ReadData readData;

    private void Awake() { readData = GetComponent<ReadData>(); }
    private void OnEnable()
    {
        tryRefreashDataButton.onClick.AddListener(TryRefreashData);//Links button click to getting data from URL
        exitButton.onClick.AddListener(OnQuitClicked);
        GameManager.OnDataParsed += GameManager_OnDataParsed;
    }

    /// <summary>
    /// Close / Exit game func
    /// </summary>
    private void OnQuitClicked()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private void TryRefreashData() { readData.TryGetData(); }

    private void GameManager_OnDataParsed(ProductList newProductList)//---------------------->UI connection
    {
        //shelfInterface.ClearInterface();
        //StartCoroutine(DelayedGetCurrentShelfProducts());
    }
    
    private IEnumerator DelayedGetCurrentShelfProducts()
    {
        yield return waitforXsec;
        shelfInterface.AddProductsToInterface(Shelf.GetCurrentActiveProducts());
    }

    //Modify the name and price of displayed products
    //Submit change and send display information update -> to UIManager
}