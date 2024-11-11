using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject confirmationScreen;

    private WaitForSeconds waitforXsec = new WaitForSeconds(1f);
    private int currentEditingProductIndex;
    private Product productInEditing;
    private ReadData readData;

    private void Awake() { readData = GetComponent<ReadData>(); }
    private void OnEnable()
    {
        tryRefreashDataButton.onClick.AddListener(TryRefreashData);//Links button click to getting data from URL
        confirmButton.onClick.AddListener(OnConfirmClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
        exitButton.onClick.AddListener(OnQuitClicked);
        ProductInterface.OnAnyProductButtonPressed += ProductInterface_OnAnyProductButtonPressed;
        ProductEditScreen.OnAnyProductEdited += ProductEditScreen_OnAnyProductEdited;
        Shelf.OnShelfUpdated += Shelf_OnShelfUpdated;
    }
    private void OnDisable()
    {
        tryRefreashDataButton.onClick.RemoveListener(TryRefreashData);
        confirmButton.onClick.RemoveListener(OnConfirmClicked);
        cancelButton.onClick.RemoveListener(OnCancelClicked);
        exitButton.onClick.RemoveListener(OnQuitClicked);
        ProductInterface.OnAnyProductButtonPressed -= ProductInterface_OnAnyProductButtonPressed;
        ProductEditScreen.OnAnyProductEdited -= ProductEditScreen_OnAnyProductEdited;
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
        List<ProductData> currentProductData = Shelf.GetCurrentActiveProducts();
        List<ProductInterface> currentProductInterface = ShelfInterface.GetProductInterfaceList();
        currentProductInterface[currentEditingProductIndex].UpdateProductInterface(productInEditing);
        currentProductData[currentEditingProductIndex]._productUI.UpdateProductUI(productInEditing);

        confirmationScreen.SetActive(false);
    }
    #endregion

    private void ProductInterface_OnAnyProductButtonPressed(Product pressedProduct, int productIndex)
    {
        if (!editScreen) { return; }

        currentEditingProductIndex = productIndex;
        editScreen.gameObject.SetActive(true);
        editScreen.Setup(pressedProduct);
    }
    private void ProductEditScreen_OnAnyProductEdited(Product editedProduct)
    {
        productInEditing = editedProduct;
        confirmationScreen.SetActive(true);
    }
    private void Shelf_OnShelfUpdated(ProductList newProductList)
    {
        shelfInterface.ClearInterface();
        shelfInterface.AddProductsToInterface(Shelf.GetCurrentActiveProducts());
    }

}