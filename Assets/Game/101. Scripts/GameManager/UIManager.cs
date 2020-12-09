using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public bool isPopup = false;
    public GameManager gameManager;
    public GameObject pauseCanvas;
    public bool keyActive = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        keyActive = true;
    }

    private void FixedUpdate()
    {
        PopupPauseMenu();
        GameStart();
    }

    private void PopupPauseMenu()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyActive && gameManager.gameStart && !isPopup && keyboard.escapeKey.isPressed)
        {
            isPopup = true;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseCanvas.SetActive(true);

            TogglePlayerCamera(false);
            TogglePlayerInput(false);

            keyActive = false;
        }
    }

    private void GameStart()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (!gameManager.gameStart && (!keyboard.escapeKey.isPressed && keyboard.anyKey.isPressed))
        {
            gameManager.gameStart = true;
            StartCoroutine(gameManager.FadeIn());
        }
    }

    public void CloseMenuCourutine()
    {
        StartCoroutine(CloseMenu());
    }

    IEnumerator CloseMenu()
    {
        Debug.Log("Start UI Manager Courutine");
        yield return new WaitForSecondsRealtime(0.5f);
        keyActive = true;
        Debug.Log("End UI Manager Courutine");
    }

    public void TogglePlayerCamera(bool value)
    {
        gameManager.playerCamera.enabled = value;
    }

    public void TogglePlayerInput(bool value)
    {
        gameManager.player.enabled = value;
    }
}
