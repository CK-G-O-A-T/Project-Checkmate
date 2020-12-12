using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearTimer : MonoBehaviour
{
    private float timer;
    private string timeString;

    private bool timerChecker = false;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        timerChecker = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (timerChecker)
        {
            TimeFunction();
        }
    }

    private void TimeFunction()
    {
        timer += Time.deltaTime;
        TimeSpan ts = new TimeSpan(0, 0, (int)timer);
        string str = (timer - (int)timer).ToString();
        if (str.Length >= 5)
        {
            timeString = ts.ToString() + ":" + str.Substring(2, 3);
        }
        text.text = timeString;
    }

    public void TimerStartOrStop(bool value)
    {
        timerChecker = value;
    }
}
