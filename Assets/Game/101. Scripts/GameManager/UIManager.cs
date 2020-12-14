using System.Collections;
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

    public float exitTimer = 0f;

    public GameObject exitPanel;
    [SerializeField] FMODUnity.StudioEventEmitter fmodAudio;

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
        ExitGame();
    }

    private void PauseMenu()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (!gameManager.gameEnd && keyActive && gameManager.gameStart && !isPopup && keyboard.escapeKey.isPressed)
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

    public void ExitGame()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }
        
        if (!gameManager.gameStart && keyboard.escapeKey.isPressed && exitPanel != null)
        {
            exitTimer += Time.deltaTime;
            exitPanel.SetActive(true);
#if UNITY_EDITOR
            if (exitTimer >= 1.5f)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif

#if UNITY_STANDALONE
            if (exitTimer >= 1.5f)
            {
                Application.Quit();
            }
#endif
        }

        else if (!gameManager.gameStart && exitPanel != null)
        {
            exitTimer = 0f;
            exitPanel.SetActive(false);
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
            fmodAudio.Play();
            gameManager.gameStart = true;
            gameManager.MainMenuToGameScene();
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
