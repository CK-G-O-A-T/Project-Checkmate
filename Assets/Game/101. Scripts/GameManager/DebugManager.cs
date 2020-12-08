﻿using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(int.MinValue)]
public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance
    {
        get;
        private set;
    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
    public void PushDebugText(string text)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        debugTextBuilder.AppendLine(text);
#endif
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    StringBuilder debugTextBuilder = new StringBuilder();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Update()
    {
        debugTextBuilder.Clear();
    }

    private void OnGUI()
    {
        GUILayout.TextArea($"{debugTextBuilder}");
    }
#endif
}