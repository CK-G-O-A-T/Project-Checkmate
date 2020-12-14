using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool keyActive = false;

    private void Start()
    {
        GameManager.Instance.uiManager.pauseCanvas = gameObject;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        keyActive = false;
        StartCoroutine(CloseMenu());
    }

    IEnumerator CloseMenu()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        keyActive = true;
    }

    public void ReturnToMainMenu()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ReturnToMainTitle();
    }

    public void Resume()
    {
        GameManager.Instance.uiManager.PauseMenuClose();
    }
}
