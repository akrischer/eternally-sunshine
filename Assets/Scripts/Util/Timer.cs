using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    private bool isStarted = false;
    public bool IsStarted
    {
        get
        {
            return isStarted;
        }
    }
    private float startTime = -1; // system time in seconds when timer was started
    private float setTime;
    public float SetTime
    {
        get
        {
            return setTime;
        }
    }

	public Timer(float setTimeSeconds)
    {
        this.setTime = setTimeSeconds;
    }

    public bool StartTimer()
    {
        // if timer was already started but not reset
        if (!IsComplete())
        {
            return false;
        } else
        {
            startTime = Time.time;
            isStarted = true;
            return true;
        }
    }

    public bool IsComplete()
    {
        if (!IsStarted)
        {
            return true;
        } else
        {
            return Time.time - startTime > SetTime;
        }
    }

}
