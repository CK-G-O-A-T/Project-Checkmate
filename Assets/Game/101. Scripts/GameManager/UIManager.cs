﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public bool isPopup = false;
    private GameManager gameManager;

    public GameObject pauseCanvas;
    public GameObject victoryCanvas;
    public GameObject gameOverCanvas;

    public bool keyActive = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        keyActive = true;
    }

    private void Update()
    {
        PauseMenu();
        GameStart();
    }

    private void PauseMenu()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyActive && gameManager.gameStart && !isPopup && keyboard.escapeKey.isPressed)
        {
            Debug.Log("PauseMenuOpen");
            PauseMenuOpen();
        }

        else if (keyActive && gameManager.gameStart && isPopup && keyboard.escapeKey.isPressed)
        {
            Debug.Log("PauseMenuClose");
            PauseMenuClose();
        }

    }

    public void PauseMenuOpen()
    {
        TogglePlayerCamera(false);
        TogglePlayerInput(false);

        isPopup = true;

        TimeManager.Instance.IsPause = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseCanvas.SetActive(true);
        keyActive = false;

        StartCoroutine(CloseMenu());
    }

    public void PauseMenuClose()
    {
        TogglePlayerCamera(true);
        TogglePlayerInput(true);

        isPopup = false;

        TimeManager.Instance.IsPause = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pauseCanvas.SetActive(false);
        keyActive = false;

        StartCoroutine(CloseMenu());
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

    public void VictoryCanvasOpen()
    {

    }

    public void GameOverCanvasOpen()
    {

    }
}
