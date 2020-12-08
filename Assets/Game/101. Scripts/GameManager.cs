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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public Image fadeImage;
    public bool pressCheck;
    public UnityEvent fadeIn;
    public UnityEvent fadeOut;
    public bool gameStart = false;
    public PlayerInput managerInput;

    // Start is called before the first frame update
    void Start()
    {
        //managerInput.actions
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(SceneLoad(sceneName));
    }

    private IEnumerator SceneLoad(string sceneName)
    {
        Debug.Log("Scene Load Start");
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        yield return SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Scene Load Complete");

        //managerInput.enabled = true;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
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
    private IEnumerator FadeOut()
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

    #region Input Action

    public void OnAnyKey(InputValue value)
    {
        if (value.isPressed && !gameStart)
        {
            StartCoroutine(FadeIn());
            gameStart = true;
            //managerInput.enabled = false;
        }
    }

    public void OnEscape(InputValue value)
    {
        Debug.Log("ESC");
    }
    #endregion
}
