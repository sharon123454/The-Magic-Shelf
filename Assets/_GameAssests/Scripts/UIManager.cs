using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Responsible for User input and game interaction
/// </summary>
[RequireComponent(typeof(ReadData))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private Button tryRefreashDataButton;
    [SerializeField] private Button exitButton;

    private ReadData readData;

    private void Awake() { readData = GetComponent<ReadData>(); }
    private void OnEnable()
    {
        tryRefreashDataButton.onClick.AddListener(TryRefreashData);//Links button click to getting data from URL
        exitButton.onClick.AddListener(OnQuitClicked);
    }

    /// <summary>
    /// Close / Exit game
    /// </summary>
    public void OnQuitClicked()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void TryRefreashData() { readData.TryGetData(); }

}