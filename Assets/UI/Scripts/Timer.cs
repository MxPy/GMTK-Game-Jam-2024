using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

     private float elapsedRunningTime = 0f;
     private float runningStartTime = 0f;
     private float pauseStartTime = 0f;
     private float elapsedPausedTime = 0f;
     private float totalElapsedPausedTime = 0f;
     private bool running = false;
     private bool paused = false;
     
     void Update()
     {
         if (running)
         {
             elapsedRunningTime = Time.time - runningStartTime - totalElapsedPausedTime;
            DisplayTime(elapsedRunningTime);
         }
         else if (paused)
         {
             elapsedPausedTime = Time.time - pauseStartTime;
         }
     }
 
     public void Begin()
     {
         if (!running && !paused)
         {
             runningStartTime = Time.time;
             running = true;
         }
     }
 
     public void Pause()
     {
         if (running && !paused)
         {
             running = false;
             pauseStartTime = Time.time;
             paused = true;
         }
     }
 
     public void Unpause()
     {
         if (!running && paused)
         {
             totalElapsedPausedTime += elapsedPausedTime;
             running = true;
             paused = false;
         }
     }
 
     public void Reset()
     {
         elapsedRunningTime = 0f;
         runningStartTime = 0f;
         pauseStartTime = 0f;
         elapsedPausedTime = 0f;
         totalElapsedPausedTime = 0f;
         running = false;
         paused = false;
     }
 
     public int GetMinutes()
     {
         return (int)(elapsedRunningTime / 60f);
     }
 
     public int GetSeconds()
     {
         return (int)(elapsedRunningTime);
     }
 
     public float GetMilliseconds()
     {
         return (float)(elapsedRunningTime - System.Math.Truncate(elapsedRunningTime));
     }

	 public float GetRawElapsedTime()
     {
         return elapsedRunningTime;
     }
 
    void DisplayTime(float timeToDisplay)
    {
        Debug.Log("jeeej");
        // int hours = Mathf.FloorToInt(timeToDisplay / 3600);
        int minutes = Mathf.FloorToInt((timeToDisplay % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt((timeToDisplay * 100) % 100);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
