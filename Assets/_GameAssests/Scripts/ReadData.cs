using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// Recieves JSON data from URL
/// </summary>
public class ReadData : MonoBehaviour
{
    /// <summary>
    /// Invoked when coroutine returned string from URL
    /// </summary>
    public event Action<string> OnDataRecievedFromURL;

    private string uRL = "https://homework.mocart.io/api/products";

    /// <summary>
    /// Activates web request coroutine
    /// </summary>
    public void TryGetData()
    {
        StartCoroutine(GetURLData());
    }

    private IEnumerator GetURLData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uRL))
        {
            yield return request.SendWebRequest();//Waiting web reply

            if (request.isHttpError || request.isNetworkError) { Debug.LogError(request.error); }//Error catch
            else
                OnDataRecievedFromURL?.Invoke(request.downloadHandler.text);//Notify of successfully obtaining web data + passing it on
        }
    }

}