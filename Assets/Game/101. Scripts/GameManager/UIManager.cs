using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public bool isPopup = false;
    public GameManager gameManager;
    public GameObject pauseCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
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

        if (gameManager.gameStart && !isPopup && keyboard.escapeKey.isPressed)
        {
            isPopup = true;
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
    }

    private void GameStart()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (!gameManager.gameStart && keyboard.anyKey.isPressed)
        {
            gameManager.gameStart = true;
            StartCoroutine(gameManager.FadeIn());
        }
    }
}
