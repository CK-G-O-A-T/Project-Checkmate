using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActivater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenCanvas()
    {
        gameObject.SetActive(true);
        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(7f);
        GameManager.Instance.ReturnToMainTitle();
    }
}
