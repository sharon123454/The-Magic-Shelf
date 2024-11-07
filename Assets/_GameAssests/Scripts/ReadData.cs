using System.Collections;
using UnityEngine;

public class ReadData : MonoBehaviour
{
    public bool isActive = false;
    private string url = "https://homework.mocart.io/api/products";

    private void Update()
    {
        if (isActive)
        {
            ReadDat();
            isActive = false;
        }
    }

    public void ReadDat()
    {
        StartCoroutine(ReadURL());
    }

    IEnumerator ReadURL()
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            Debug.Log(www.text);
        }
    }

}