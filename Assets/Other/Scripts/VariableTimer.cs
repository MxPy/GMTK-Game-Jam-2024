using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VariableTimer : MonoBehaviour
{
    public float actualTimeOfEvent;  // The actual time when the event should occur
    public float timerTime;           // The timer duration
    public bool started = false,  finished = false;     // Flag indicating whether the timer has finished

    private void Awake()
    {
        actualTimeOfEvent = float.PositiveInfinity;
    }

    public void StartTimer(float delay)
    {
        actualTimeOfEvent = Time.time + delay;
        started = true;
        StartCoroutine(RunTimer());
    }

    public void ResetTimer()
    {
        finished = false;
    }

    private IEnumerator RunTimer()
    {
        while (Time.time < actualTimeOfEvent)
            yield return null;
        finished = true;
        started = false;
        // Debug.Log("EVENT!");
        actualTimeOfEvent = float.PositiveInfinity;
    }
}