using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilDoman : MonoBehaviour
{
    public VariableTimer timerLive;
    public SpriteRenderer player;
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerLive.StartTimer(8.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerLive.started && state == 1){
            if(player.enabled == true){
                 SceneManager.LoadScene("gtmktest");
            }
            Debug.Log("quuutas");
        }
        if(timerLive.finished && state == 0){
            state = 1;
            timerLive.ResetTimer();
            timerLive.StartTimer(2.5f);

        }
        if(timerLive.finished && state == 1){
            state = 0;
            timerLive.ResetTimer();
            timerLive.StartTimer(10.5f);
        }
    }
}
