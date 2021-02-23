using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float timeRemaining = 10.0f;
    public bool timerIsRunning = false;

    public Timer(float time, bool startTimer)
    {
        this.timeRemaining = time;
        this.timerIsRunning = startTimer;
    }

    private void Start()
    {
        // Starts the timer automatically
        //timerIsRunning = true;
    }

    public void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public bool ResumeTimer()
    {
        if (timeRemaining > 0)
        {
            timerIsRunning = true;
            return true; //Timer was resumed successfully
        }
        return false; //The timer couldn't resume, because there is no more time remaining
    }

    public void SetTimerTimer(float time)
    {
        this.timeRemaining = time;
    }

    public bool isTimerDone()
    {
        if (timeRemaining > 0 )
        {
            return false;
        }
        return true;
    }
}
