using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float fixedTimestep = 0.016f;
    [SerializeField] float globalTimeScale = 1;
    [SerializeField] float actionTimeScale = 1;
    [SerializeField] float debugTimeScale = 1;
    [SerializeField] bool isPause;

    //float actionTimeScaleSmooth;
    float actionTimeLerpDuration = 0;
    float currentActionTimeLerpTime = 0;
    float lastSetActionTimeScale;

    public static TimeManager Instance { get; private set; }

    public float GlobalTimeScale
    {
        get => this.globalTimeScale;
        set
        {
            this.globalTimeScale = value;
            UpdateTimeScale();
        }
    }
    public float DebugTimeScale
    {
        get => this.debugTimeScale;
        set
        {
            this.debugTimeScale = Mathf.Max(0, value);
            UpdateTimeScale();
        }
    }
    public float ActionTimeScale
    {
        get => this.actionTimeScale;
        set
        {
            this.actionTimeScale = value;
            UpdateTimeScale();
        }
    }
    public float FixedTimestep
    {
        get => this.fixedTimestep;
        set
        {
            this.fixedTimestep = value;
            UpdateTimeScale();
        }
    }

    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            UpdateTimeScale();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UpdateTimeScale();
        }
    }

    public void UpdateTimeScale()
    {
        if (!isPause)
        {
            var timeScale = GlobalTimeScale * ActionTimeScale * DebugTimeScale;

            Time.timeScale = timeScale;
            Time.fixedDeltaTime = FixedTimestep * Mathx.Clamp(timeScale, 0.0001f, 1f);
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void SetActionTimeScale(float value, float restoreTime = 0.25f)
    {
        lastSetActionTimeScale = value;
        ActionTimeScale = lastSetActionTimeScale;
        actionTimeLerpDuration = restoreTime;
        currentActionTimeLerpTime = 0;
    }

    private void Update()
    {
        currentActionTimeLerpTime += (Time.unscaledDeltaTime * GlobalTimeScale * DebugTimeScale) / actionTimeLerpDuration;
        actionTimeScale = Mathf.Lerp(lastSetActionTimeScale, 1f, currentActionTimeLerpTime);
        DebugManager.Instance.PushDebugText($"actionTimeScale: {(actionTimeScale)}");
        DebugManager.Instance.PushDebugText($"timeScale: {Time.timeScale}");

        //actionTimeScale = Mathf.SmoothDamp(actionTimeScale, 1f, ref actionTimeScaleSmooth, actionTimeSmoothTime, float.PositiveInfinity, Time.unscaledDeltaTime * GlobalTimeScale * DebugTimeScale);
        UpdateTimeScale();
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    bool numpadMultiplyPressed;
    bool numpadPlusPressed;
    bool numpadMinusPressed;
    private void LateUpdate()
    {
        if (Keyboard.current[Key.NumpadMultiply].IsCurrentState(ref numpadMultiplyPressed) == ButtonInputState.Pressed)
        {
            DebugTimeScale = 1;
        }
        else if (Keyboard.current[Key.NumpadPlus].IsCurrentState(ref numpadPlusPressed) == ButtonInputState.Pressed)
        {
            DebugTimeScale += 0.1f;
        }
        else if (Keyboard.current[Key.NumpadMinus].IsCurrentState(ref numpadMinusPressed) == ButtonInputState.Pressed)
        {
            DebugTimeScale -= 0.1f;
        }

        var debug = DebugManager.Instance;
        if (debug != null)
        {
            debug.PushDebugText($"debugTimeScale: {DebugTimeScale}");
        }
    }
#endif
}
