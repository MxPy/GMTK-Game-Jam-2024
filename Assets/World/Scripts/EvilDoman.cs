using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilDoman : MonoBehaviour
{
    public VariableTimer timerLive;
    public SpriteRenderer player;
    public GameObject bg;
    public string restartSene = "gtmktest";
    public int typee = 0;
    public int state = 0;

    public float time1 = 7.25f;
    public float time2 = 2.25f;
    public float time3 = 7.75f;

    // Start is called before the first frame update
    void Start()
    {
        timerLive.StartTimer(time1);
        Debug.Log(typee);
        bg.GetComponent<Animator>().Play("bg2idle");
    }

    // Update is called once per frame
    void Update()
    {
        if(timerLive.started && state == 1){
            if(player.enabled == true){
                 SceneManager.LoadScene(restartSene);
            }
            Debug.Log("quuutas");
        }
        if(timerLive.finished && state == 0){
            if(typee == 0){
                bg.GetComponent<Animator>().Play("gut");
            }else if(typee == 1){
                bg.GetComponent<Animator>().Play("stary");
            }
            else if(typee == 2){
                bg.GetComponent<Animator>().Play("bg2");
            }
            
            state = 1;
            timerLive.ResetTimer();
            timerLive.StartTimer(time2);

        }
        if(timerLive.finished && state == 1){
            if(typee == 0){
                bg.GetComponent<Animator>().Play("idlebg");
            }else if(typee == 1){
                bg.GetComponent<Animator>().Play("idles3");
            }else if(typee == 2){
                
                bg.GetComponent<Animator>().Play("bg2idle");
            }
            
            state = 0;
            timerLive.ResetTimer();
            timerLive.StartTimer(time3);
        }
    }
}
