using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool keyActive = false;
    public UIManager uiManager;

    private void OnEnable()
    {
        keyActive = false;
        StartCoroutine(CloseMenu());
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyActive && keyboard.escapeKey.isPressed)
        {
            Resum();
        }
    }

    IEnumerator CloseMenu()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        keyActive = true;
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.ReturnToMainTitle();
    }

    public void Resum()
    {
        TimeManager.Instance.IsPause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        uiManager.CloseMenuCourutine();
        uiManager.isPopup = false;
        uiManager.TogglePlayerCamera(true);
        uiManager.TogglePlayerInput(true);
        gameObject.SetActive(false);
    }
}
