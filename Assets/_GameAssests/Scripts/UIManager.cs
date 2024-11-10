using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Responsible for User input and game interaction
/// </summary>
[RequireComponent(typeof(ReadData))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private Button tryRefreashDataButton;

    private ReadData readData;

    private void Awake() { readData = GetComponent<ReadData>(); }
    private void OnEnable()
    {
        tryRefreashDataButton.onClick.AddListener(TryRefreashData);//Links button click to getting data from URL
    }

    private void TryRefreashData() { readData.TryGetData(); }

}