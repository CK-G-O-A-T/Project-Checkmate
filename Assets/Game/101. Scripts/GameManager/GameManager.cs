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
    public bool gameStart = false;
    public CinemachineBrain playerCamera;
    public PlayerInput player;
    public BossDamageHandler boss;
    public UIManager uiManager;

    private delegate void DeligateFunc();
    private DeligateFunc delimanjoo;

    public string targetScene;

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

        if (uiManager == null)
        {
            uiManager = GetComponent<UIManager>();
        }
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

    public IEnumerator LoadScene(string sceneName)
    {
        Color imgColor = fadeImage.color;
        for (float i = 0f; i <= 1.1f; i += Time.deltaTime * 0.8f)
        {
            imgColor.a = i;
            fadeImage.color = imgColor;
            yield return null;
        }
        Debug.Log("Fade In Complete");
        StartCoroutine(SceneLoad(sceneName));
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
    }

    public void ReturnToMainTitle()
    {
        delimanjoo = new DeligateFunc(InitMainGameData);
        uiManager.isPopup = false;
        TimeManager.Instance.IsPause = false;
        StartCoroutine(LoadScene("TitleScene"));
    }

    public void MainMenuToGameScene()
    {
        delimanjoo = new DeligateFunc(LoadMainGameData);
        StartCoroutine(LoadScene(targetScene));
    }

    #region Input Action
    #endregion
}
