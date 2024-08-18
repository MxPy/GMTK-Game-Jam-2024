using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shini : MonoBehaviour
{
    public VariableTimer timer;
    int state = 0;
    public float freq = 2;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        timer = gameObject.AddComponent(typeof(VariableTimer)) as VariableTimer;
        timer.StartTimer(1/freq);
    }
    // Update is called once per frame
    void Update()
    {
        if(timer.finished && state == 0){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.7f, 1, 1);
            state = 1;
            timer.ResetTimer();
            timer.StartTimer(1/freq);
        }

        if(timer.finished && state == 1){
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            state = 0;
            timer.ResetTimer();
            timer.StartTimer(1/freq);
        }
    }
}
