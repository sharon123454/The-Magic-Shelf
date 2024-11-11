using UnityEngine.UI;
using UnityEngine;
using TMPro;

/// <summary>
/// Responsible for User input and game interaction
/// </summary>
[RequireComponent(typeof(ReadData))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private ShelfInterface shelfInterface;
    [SerializeField] private ProductEditScreen editScreen;

    [SerializeField] private Button tryRefreashDataButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button okayButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject confirmationScreen;

    private WaitForSeconds waitforXsec = new WaitForSeconds(1f);
    private ReadData readData;

    private void Awake() { readData = GetComponent<ReadData>(); }
    private void OnEnable()
    {
        tryRefreashDataButton.onClick.AddListener(TryRefreashData);//Links button click to getting data from URL
        confirmButton.onClick.AddListener(OnConfirmClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
        exitButton.onClick.AddListener(OnQuitClicked);
        okayButton.onClick.AddListener(OnOkayClicked);
        ProductInterface.OnAnyProductButtonPressed += ProductInterface_OnAnyProductButtonPressed;
        Shelf.OnShelfUpdated += Shelf_OnShelfUpdated;
    }
    private void OnDisable()
    {
        tryRefreashDataButton.onClick.RemoveListener(TryRefreashData);
        confirmButton.onClick.RemoveListener(OnConfirmClicked);
        cancelButton.onClick.RemoveListener(OnCancelClicked);
        exitButton.onClick.RemoveListener(OnQuitClicked);
        okayButton.onClick.RemoveListener(OnOkayClicked);
        ProductInterface.OnAnyProductButtonPressed -= ProductInterface_OnAnyProductButtonPressed;
        Shelf.OnShelfUpdated -= Shelf_OnShelfUpdated;
    }

    //Submit change and send display information update -> to UIManager
    #region Button functions
    /// <summary>
    /// Tries to access web data for new products
    /// </summary>
    private void TryRefreashData() { readData.TryGetData(); }
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
    /// <summary>
    /// Change from editing screen to confirmation
    /// </summary>
    private void OnOkayClicked()
    {
        confirmationScreen.SetActive(true);
        editScreen.gameObject.SetActive(false);
    }
    /// <summary>
    /// Exit confirmation screen, keep OLD data
    /// </summary>
    private void OnCancelClicked()
    {
        confirmationScreen.SetActive(false);
    }
    /// <summary>
    /// Exit confirmation screen, update NEW data
    /// </summary>
    private void OnConfirmClicked()
    {
        //editScreen.GetData();
        //Update DATA to shelf and UI
        //Shelf.GetCurrentActiveProducts();
        //ShelfInterface.GetProductInterfaceList();

        confirmationScreen.SetActive(false);
    }
    #endregion

    private void ProductInterface_OnAnyProductButtonPressed(string productNameAndPrice)
    {
        if (!editScreen) { return; }

        editScreen.gameObject.SetActive(true);
        editScreen.Setup(productNameAndPrice);
    }
    private void Shelf_OnShelfUpdated(ProductList newProductList)
    {
        shelfInterface.ClearInterface();
        shelfInterface.AddProductsToInterface(Shelf.GetCurrentActiveProducts());
    }

}