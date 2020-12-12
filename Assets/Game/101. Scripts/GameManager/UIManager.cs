using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public bool isPopup = false;
    private GameManager gameManager;
    public GameObject pauseCanvas;
    public bool keyActive = true;

    [SerializeField] FMODUnity.StudioEventEmitter fmodAudio;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
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
            TogglePlayerCamera(false);
            TogglePlayerInput(false);

            isPopup = true;

            TimeManager.Instance.IsPause = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseCanvas.SetActive(true);
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
            fmodAudio.Play();
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
