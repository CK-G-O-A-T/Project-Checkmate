using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                Debug.LogError("GameManager NULL");
                return null;
            }
            else
            {
                return instance;
            }
        }
    }

    public Image fadeImage;
    public UnityEvent fadeIn;
    public UnityEvent fadeOut;
    public bool gameStart = false;
    public CinemachineBrain playerCamera;
    public PlayerInput player;
    public BossDamageHandler boss;
    public UIManager uiManager;

    private delegate void DeligateFunc();
    private DeligateFunc delimanjoo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        delimanjoo = new DeligateFunc(LoadMainGameData);
        StartCoroutine(SceneLoad(sceneName));
    }

    private IEnumerator SceneLoad(string sceneName)
    {
        Debug.Log("Scene Load Start");
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        yield return SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Scene Load Complete");
        StartCoroutine(FadeOut());
        delimanjoo();
    }

    private void LoadMainGameData()
    {
        playerCamera = Camera.main.GetComponent<CinemachineBrain>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossDamageHandler>();
    }

    private void InitMainGameData()
    {
        playerCamera = null;
        player = null;
        gameStart = false;
        boss = null;
    }

    public IEnumerator FadeIn()
    {
        Color imgColor = fadeImage.color;
        for (float i = 0f; i <= 1.1f; i += Time.deltaTime * 0.8f)
        {
            imgColor.a = i;
            fadeImage.color = imgColor;
            yield return null;
        }
        Debug.Log("Fade In Complete");
        fadeIn.Invoke();
    }
    public IEnumerator FadeOut()
    {
        Color imgColor = fadeImage.color;
        for (float i = 1f; i >= -0.1f; i -= Time.deltaTime * 0.8f)
        {
            imgColor.a = i;
            fadeImage.color = imgColor;
            yield return null;
        }
        Debug.Log("Fade Out Complete");
        fadeOut.Invoke();
    }

    public void ReturnToMainTitle()
    {
        delimanjoo = new DeligateFunc(InitMainGameData);
        StartCoroutine(SceneLoad("TitleScene"));
        TimeManager.Instance.IsPause = false;
    }

    #region Input Action

    public void OnAnyKey(InputValue value)
    {
        if (!gameStart && value.isPressed)
        {
            StartCoroutine(FadeIn());
            gameStart = true;
        }
    }

    public void OnEscape(InputValue value)
    {
        Debug.Log("ESC");
    }
    #endregion
}
